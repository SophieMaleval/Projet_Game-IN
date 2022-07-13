using AllosiusDev;
using Core.Session;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class BaseGameController : MonoBehaviour
    {
        #region Properties

        public SceneData LoadingScreenScene => loadingScreenScene;
        public SceneData MenuScene => menuScene;
        public SceneData CharacterCustomerScene => characterCustomerScene;

        public SceneData StartLevelScene => startLevelScene;

        #endregion

        #region UnityInspector

        [SerializeField] private SceneData loadingScreenScene;
        [SerializeField] private SceneData menuScene;
        [SerializeField] private SceneData characterCustomerScene;

        [SerializeField] private SceneData startLevelScene;

        #endregion

        #region Unity Functions

        protected void Awake()
        {

            SessionController.Instance.InitializeGame(this);
        }

        #endregion

        #region Public Functions

        public virtual void OnInit()
        {

        }

        public virtual void OnUpdate()
        {

        }

        #endregion
    }
}

