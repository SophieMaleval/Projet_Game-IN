using AllosiusDev.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    #region Fields

    [Header("Inputs")]
    private PlayerActionControls PlayerActionControllers;

    private float InitialMoveSpeed;

    Scene scene;

    private Vector2 LastMoveDirection;
    private Vector2 MoveDirection;

    private float AmplitudeToSwitchScoot = 0.125f;
    bool OnSlope = false;
    float ValueSlopeAdd;

    bool SlopeStartLeft;
    int ElevationValue;

    #endregion

    #region UnityInspector



    [Header ("Movement")]
    [SerializeField] private float MoveSpeed ;
        
        [SerializeField] private float ScooterSpeed ;

        //public AudioSource ScooterStop;
        //public AudioSource ScooterMoving;

        public bool PlayOneShotClip = true;

    [HideInInspector] public bool PlayerChangeScene ;
    [HideInInspector] public bool MakePlayerInGoodSens = false ; 

        [HideInInspector] public bool PlayerNeedInitialePosition = false ;
        [HideInInspector] public bool PlayerNeedLookUp = false ;
        [HideInInspector] public bool PlayerNeedLookRight = false ;
        [HideInInspector] public bool PlayerNeedLookLeft = false ;




    public List<SpriteRenderer> PlayerRenderers ;
    public Rigidbody2D RbPlayer ;
    

    public List<Animator> Animators ;

    

    [Header ("Avatar Display")]
    public List<float> ValueColorDisplay ;
    public List<Color> ColorsDisplay ;
    public List<RuntimeAnimatorController> SpriteDisplay ;

    public bool OnScooter = false;
    public bool InExterior = false ;
    

    [Header("Sounds")]

    [SerializeField] private AudioData sfxScooterStop;
    [SerializeField] private AudioData sfxScooterMoving;

    #endregion

    #region Behaviour

    //public void LunchPlayerGame() {PlayerActionControllers.PlayerInLand.Enable() ;  }

    private void OnEnable() {   PlayerActionControllers.Enable(); }
    private void OnDisable() {   PlayerActionControllers.Disable();   }

    public void StartActivity() {   PlayerActionControllers.Disable();   }
    public void EndActivity() {   PlayerActionControllers.Enable(); }

    private void Awake() 
    {  
        PlayerActionControllers = new PlayerActionControls();
        PlayerActionControllers.PlayerInLand.EnterScoot.performed += OnEnterScoot;
        PlayerActionControllers.PlayerInScoot.ExitScoot.performed += OnExitScoot;

        InitialMoveSpeed = MoveSpeed ;
    }

    private void Start() 
    {
        PlayerActionControllers.PlayerInScoot.Disable() ; 
        //PlayerActionControllers.PlayerInLand.Disable() ;   
    }

    void Update()
    {
        scene = SceneManager.GetActiveScene();

        ProcessInputs();
        Animate(); 
    }

    public void OnEnterScoot (InputAction.CallbackContext ctx )
    {      
        if(ctx.performed && (MoveDirection.magnitude >= -AmplitudeToSwitchScoot) && (MoveDirection.magnitude <= AmplitudeToSwitchScoot) && InExterior)
        {
            PlayerActionControllers.PlayerInLand.Disable();
            PlayerActionControllers.PlayerInScoot.Enable();
            switchScootState(true);
            //ScooterStop.Play();
            AudioController.Instance.PlayAudio(sfxScooterStop);
        }    
    }
    public void OnExitScoot (InputAction.CallbackContext ctx )
    {
        if(ctx.performed && (MoveDirection.magnitude >= -AmplitudeToSwitchScoot) && (MoveDirection.magnitude <= AmplitudeToSwitchScoot) && InExterior)
        {        
            if(OnScooter)
            {
                PlayerActionControllers.PlayerInScoot.Disable() ;                
                PlayerActionControllers.PlayerInLand.Enable() ;
                switchScootState(false);  
                //ScooterStop.Stop();
                AudioController.Instance.StopAudio(sfxScooterStop);
                //ScooterMoving.Stop();
                AudioController.Instance.StopAudio(sfxScooterMoving);
            }
        }
    }


    public void switchScootState(bool state)
    {
        OnScooter = state;    
        if(Animators[0].runtimeAnimatorController != null)
            Animators[0].SetBool("InScoot", state); 

        if(!OnScooter) Animators[0].GetComponent<SpriteRenderer>().color = ColorsDisplay[0];
        else Animators[0].GetComponent<SpriteRenderer>().color = Color.white;

        for (int i = 1; i < Animators.Count; i++)
        {    Animators[i].gameObject.GetComponent<SpriteRenderer>().enabled = !OnScooter ;  }

        if(!state) RebindAnimation();
    }


    private void FixedUpdate() 
    {    
        Move();
        if(OnSlope == true)
        {
            if(!OnScooter) Slopes(1f);
            else Slopes(1.5f);            
        }    
    }

    public void SlopeParameter (bool EnterSlope, float valueSlope, bool BottomAsLeft, int PositionElevation)
    {
        OnSlope = EnterSlope;
        ValueSlopeAdd =  valueSlope; 
        SlopeStartLeft = BottomAsLeft;
        ElevationValue = PositionElevation;
    }

    void Slopes(float StateDeplacementValue)
    {
        if(MoveDirection.x != 0)
        {
            if(!SlopeStartLeft)
            {
                if(MoveDirection.x < 0)
                    RbPlayer.velocity += new Vector2 (0, (ValueSlopeAdd * -ElevationValue * StateDeplacementValue));

                if(MoveDirection.x > 0)        
                    RbPlayer.velocity += new Vector2 (0, (-1 * ValueSlopeAdd * -ElevationValue * StateDeplacementValue)); 
            } else {
                if(MoveDirection.x < 0)
                    RbPlayer.velocity += new Vector2 (0, (-1 * ValueSlopeAdd * ElevationValue * StateDeplacementValue));

                if(MoveDirection.x > 0)        
                    RbPlayer.velocity += new Vector2 (0, (ValueSlopeAdd * ElevationValue * StateDeplacementValue)); 
            } 
        }   
    }

    void ProcessInputs()
    {
        Vector2 Move = Vector2.zero ;

        if(!PlayerChangeScene)
        {
            if(!OnScooter)
                Move = PlayerActionControllers.PlayerInLand.Move.ReadValue<Vector2>();
            else
                Move = PlayerActionControllers.PlayerInScoot.MoveScoot.ReadValue<Vector2>();            
        }/* else {*/
            if(MakePlayerInGoodSens) 
            {


                if(PlayerNeedInitialePosition)
                {
                    PlayerNeedInitialePosition = false ;                    
                    Move = new Vector2(0, -1f);
                } 

                if(PlayerNeedLookUp)
                {
                    PlayerNeedLookUp = false ;   
                    Move = new Vector2(0, 1f);
                } 

                if(PlayerNeedLookLeft)
                {
                    PlayerNeedLookLeft = false ;   
                    Move = new Vector2(-1f, 0f);
                } 

                if(PlayerNeedLookRight)
                {
                    PlayerNeedLookRight = false ;   
                    Move = new Vector2(1f, 0f);
                } 
                MakePlayerInGoodSens = false ;  
                PlayerChangeScene = false ;                 
            }
      //  }



        if((Move.x == 0 && Move.y == 0) && MoveDirection.x != 0 || MoveDirection.y != 0)
            LastMoveDirection = MoveDirection ;    


        if((Move.x != 0 || Move.y != 0) && OnScooter == true && PlayOneShotClip == false)
        {
            ScootMovingForward();
        }  
        if((Move.x == 0 && Move.y == 0) && OnScooter == true && PlayOneShotClip == true)
        {
            ScootNotMoving();
        }


            //MoveDirection = Move.normalized ;
        if((Move.x != 0 && Move.y != 0) || (Move.x == 0 && Move.y == 0)) MoveDirection = new Vector2 (0, 0).normalized ;
        if(Move.x != 0) MoveDirection = new Vector2 (Move.x, 0).normalized ;
        if(Move.y != 0) MoveDirection = new Vector2 (0, Move.y).normalized ;



        for (int i = 0; i < PlayerRenderers.Count; i++)
        {
            // Flip et DéFlip X en fonction de Vector2 Move 
            if(MoveDirection.x < 0)              
                PlayerRenderers[i].flipX = true ; 
            if(MoveDirection.x > 0 || MoveDirection.y != 0)        
                PlayerRenderers[i].flipX = false ;     
        }
    }

    void Move()
    {   
        if (!OnScooter)  
            RbPlayer.velocity = new Vector2(MoveDirection.x * MoveSpeed * Time.deltaTime, MoveDirection.y * MoveSpeed * Time.deltaTime); 
        else
            RbPlayer.velocity = new Vector2(MoveDirection.x * (MoveSpeed*ScooterSpeed) * Time.deltaTime, MoveDirection.y * (MoveSpeed*ScooterSpeed) * Time.deltaTime); 
    }

    public void ChangePlayerSpeed(bool PlayerIsInside)
    {
        if(PlayerIsInside) MoveSpeed = InitialMoveSpeed -1f ;
        else MoveSpeed = InitialMoveSpeed ;
    }



    void ScootNotMoving()
    {
        PlayOneShotClip = false;
        //ScooterStop.Play();
        AudioController.Instance.PlayAudio(sfxScooterStop);
        //ScooterMoving.Stop();
        AudioController.Instance.StopAudio(sfxScooterMoving);
    }

    void ScootMovingForward() {
        PlayOneShotClip = true; 

        //ScooterMoving.Play();
        //ScooterStop.Stop();
        AudioController.Instance.PlayAudio(sfxScooterMoving);
        AudioController.Instance.StopAudio(sfxScooterStop);
    }
    

    public void ResetVelocity()
    {
        RbPlayer.velocity = Vector2.zero ;
    }

    void Animate() 
    {
        // Applique la même valeur à tout les Animator
        for (int i = 0; i < Animators.Count; i++)
        {
            if(Animators[i].runtimeAnimatorController != null)
            {
                // Set Up la direction du déplacement
                Animators[i].SetFloat("AnimMoveX", MoveDirection.x) ;
                Animators[i].SetFloat("AnimMoveY", MoveDirection.y) ;
                // Garde la denière direction du dernier déplacement 
                Animators[i].SetFloat("AnimLastMoveX", LastMoveDirection.x) ;
                Animators[i].SetFloat("AnimLastMoveY", LastMoveDirection.y) ;
                // Passe de Idle à Run
                Animators[i].SetFloat("AnimMoveMagnitude",MoveDirection.magnitude);
            }
        }
    }

    public Vector2 GetLastMovePlayer()
    {
        return LastMoveDirection ;
    }

    public void GiveGoodAnimation(Vector2 OldLastMove)
    {
        if(OldLastMove.x == -1f) PlayerNeedLookLeft = true ;
        if(OldLastMove.x == 1f) PlayerNeedLookRight = true ;
        if(OldLastMove.y == -1f) PlayerNeedInitialePosition = true ;
        if(OldLastMove.y == 1f) PlayerNeedLookUp = true ;

        MakePlayerInGoodSens = true ;
    }

  

       

    void RebindAnimation()
    {
        for (int i = 0; i < Animators.Count; i++)
        {    Animators[i].Rebind();  }
    }

    #endregion
}
