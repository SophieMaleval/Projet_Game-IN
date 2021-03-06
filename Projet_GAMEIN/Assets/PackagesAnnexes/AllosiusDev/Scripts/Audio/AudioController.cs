using AllosiusDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace AllosiusDev
{
    
    namespace Audio {

        public class AudioController : Singleton<AudioController>
        {
            #region Fields

            private List<AudioSource> audioSources = new List<AudioSource>();

            private Hashtable m_JobTable;   // relationship between audio types (key) and jobs (value)

            private enum AudioAction 
            {
                START,
                STOP,
                RESTART
            }

            #endregion

            #region UnityInspector

            [SerializeField] private bool debug;

            [SerializeField] private AudioMixerGroup outputSfx, outputMusics, outputAmbients;

            [SerializeField] private SamplableLibrary audioLibrary;

            #endregion

            #region Class

            private class AudioJob {
                public AudioAction action;
                public AudioData data;
                public bool fade;
                public WaitForSeconds delay;
                public Vector3 positionOffset;
                public Transform transformToAttach;

                public AudioSource source;

                public AudioJob(AudioAction _action, AudioData _data, bool _fade, float _delay, Vector3 _positionOffset, Transform _transformToAttach = null) 
                {
                    action = _action;
                    data = _data;
                    fade = _fade;
                    delay = _delay > 0f ? new WaitForSeconds(_delay) : null;
                    positionOffset = _positionOffset;
                    transformToAttach = _transformToAttach;
                }

                public void SetSource(AudioSource _source)
                {
                    source = _source;
                }
            }

            #endregion

            #region Unity Functions
            protected override void Awake() 
            {
                base.Awake();
                Configure();
            }

            private void OnDisable() 
            {
                Dispose();
            }

            #endregion

            #region Public Functions

            public void PlayAudio(AudioData _data, Transform _transformToAttach = null, bool _fade=false, float _delay=0.0F) {
                AddJob(new AudioJob(AudioAction.START, _data, _fade, _delay, Vector3.zero, _transformToAttach));
            }

            public void PlayAudio(AudioData _data, Vector3 _positionOffset, Transform _transformToAttach = null, bool _fade = false, float _delay = 0.0F)
            {
                AddJob(new AudioJob(AudioAction.START, _data, _fade, _delay, _positionOffset, _transformToAttach));
            }


            public void PlayRandomAudio(AudioData[] _datas, Transform _transformToAttach = null, bool _fade = false, float _delay = 0.0F)
            {
                AudioData _data = _datas[Random.Range(0, _datas.Length)];
                AddJob(new AudioJob(AudioAction.START, _data, _fade, _delay, Vector3.zero, _transformToAttach));
            }

            public void PlayRandomAudio(AudioData[] _datas, Vector3 _positionOffset, Transform _transformToAttach = null, bool _fade = false, float _delay = 0.0F)
            {
                AudioData _data = _datas[Random.Range(0, _datas.Length)];
                AddJob(new AudioJob(AudioAction.START, _data, _fade, _delay, _positionOffset, _transformToAttach));
            }


            public void StopAudio(AudioData _data, Transform _transformToAttach = null, bool _fade=false, float _delay=0.0F) {
                AddJob(new AudioJob(AudioAction.STOP, _data, _fade, _delay, Vector3.zero, _transformToAttach));
            }

            public void StopAudio(AudioData _data, Vector3 _positionOffset, Transform _transformToAttach = null, bool _fade = false, float _delay = 0.0F)
            {
                AddJob(new AudioJob(AudioAction.STOP, _data, _fade, _delay, _positionOffset, _transformToAttach));
            }


            public void RestartAudio(AudioData _data, Transform _transformToAttach = null, bool _fade=false, float _delay=0.0F) {
                AddJob(new AudioJob(AudioAction.RESTART, _data, _fade, _delay, Vector3.zero, _transformToAttach));
            }

            public void RestartAudio(AudioData _data, Vector3 _positionOffset, Transform _transformToAttach = null, bool _fade = false, float _delay = 0.0F)
            {
                AddJob(new AudioJob(AudioAction.RESTART, _data, _fade, _delay, _positionOffset, _transformToAttach));
            }


            public void StopAllMusics()
            {
                foreach (AudioSource audioSource in Instance.audioSources)
                {
                    if (audioSource.outputAudioMixerGroup == Instance.outputMusics)
                    {
                        audioSource.Stop();
                    }
                }
            }

            public void StopAllSfx()
            {
                foreach (AudioSource audioSource in Instance.audioSources)
                {
                    if (audioSource.outputAudioMixerGroup == Instance.outputSfx)
                    {
                        audioSource.Stop();
                    }
                }
            }

            public void StopAllAmbients()
            {
                foreach (AudioSource audioSource in Instance.audioSources)
                {
                    if (audioSource.outputAudioMixerGroup == Instance.outputAmbients)
                    {
                        audioSource.Stop();
                    }
                }
            }

            #endregion

            #region Private Functions
            private void Configure() {
                m_JobTable = new Hashtable();
            }

            private void Dispose() {
                // cancel all jobs in progress
                foreach(DictionaryEntry _kvp in m_JobTable) {
                    Coroutine _job = (Coroutine)_kvp.Value;
                    if(_job != null)
                        StopCoroutine(_job);
                }
            }

            private void AddJob(AudioJob _job) 
            {
                Coroutine _jobRunner = StartCoroutine(RunAudioJob(_job));
                m_JobTable.Add(_job, _jobRunner);
                Log("Starting job on ["+_job.data+"] with operation: "+_job.action);
            }

            private IEnumerator RunAudioJob(AudioJob _job) {
                if (_job.delay != null) yield return _job.delay;

                float _initial = 0f;
                float _target = _job.data.SoundIs3D();
                switch (_job.action) 
                {
                    case AudioAction.START:
                        PlaySound(_job);
                    break;
                    case AudioAction.STOP when !_job.fade:
                        StopSound(_job);
                    break;
                    case AudioAction.STOP:
                        _initial = _job.data.SoundIs3D();

                        _target = 0f;
                    break;
                    case AudioAction.RESTART:
                        StopSound(_job);
                        PlaySound(_job);
                    break;
                }

                AudioSource source = null;
                // fade volume
                if (_job.fade) 
                {
                    Debug.Log("fade");

                    float _duration = 1.0f;
                    float _timer = 0.0f;

                    

                    foreach (AudioSource audioSource in Instance.audioSources)
                    {
                        if (audioSource.clip == _job.data.Clip)
                        {
                            source = audioSource;
                            break;
                        }
                    }

                    if(source != null)
                    {
                        while (_timer <= _duration)
                        {
                            source.volume = Mathf.Lerp(_initial, _target, _timer / _duration);
                            _timer += Time.deltaTime;
                            yield return null;
                        }

                        // if _timer was 0.9999 and Time.deltaTime was 0.01 we would not have reached the target
                        // make sure the volume is set to the value we want
                        source.volume = _target;

                        if (_job.action == AudioAction.STOP)
                        {
                            source.Stop();
                        }
                    }
                    
                }

                m_JobTable.Remove(_job);
                Log("Job count: "+m_JobTable.Count);
            }

            private void PlaySound(AudioJob _job)
            {
                AudioSource source = FindFreeAudiosource(_job, Instance.audioSources);
                if (_job.transformToAttach == null)
                {
                    source.transform.parent = Instance.transform;
                }
                else
                {
                    source.transform.parent = _job.transformToAttach;
                }
                source.transform.localPosition = _job.positionOffset;
                source.clip = _job.data.Clip;

                if (_job.data.typeSound == AudioData.TypeSound.Music)
                {
                    source.outputAudioMixerGroup = Instance.outputMusics;
                }
                else if (_job.data.typeSound == AudioData.TypeSound.Sfx)
                {
                    source.outputAudioMixerGroup = Instance.outputSfx;
                }
                else if (_job.data.typeSound == AudioData.TypeSound.Ambients)
                {
                    source.outputAudioMixerGroup = Instance.outputAmbients;
                }

                _job.SetSource(source);

                source.SetSoundToSource(_job.data);
                source.Play();
            }

            private void StopSound(AudioJob _job)
            {
                Debug.Log("Stop Sound " + _job.data.name);

                foreach (AudioSource audioSource in Instance.audioSources)
                {
                    if (audioSource != null && audioSource.clip == _job.data.Clip)
                    {
                        audioSource.Stop();
                        //Destroy(audioSource.gameObject);
                    }
                }
            }

            private AudioSource FindFreeAudiosource(AudioJob _job, List<AudioSource> _audioSources)
            {
                foreach (AudioSource audioSource in _audioSources)
                {
                    if (audioSource != null && !audioSource.isPlaying) return audioSource;
                }
                return Instance.CreateNewAudioSource(_job);
            }

            private AudioSource CreateNewAudioSource(AudioJob job)
            {
                GameObject go = new GameObject("Audio Source " + job.data.name);
                go.transform.parent = transform;
                AudioSource newAudioSource = go.AddComponent<AudioSource>();
                newAudioSource.playOnAwake = false;
                audioSources.Add(newAudioSource);
                return newAudioSource;
            }

            private void Log(string _msg) {
                if (!debug) return;
                Debug.Log("[Audio Controller]: "+_msg);
            }
            
            private void LogWarning(string _msg) {
                if (!debug) return;
                Debug.LogWarning("[Audio Controller]: "+_msg);
            }
            #endregion
        }
    }
}