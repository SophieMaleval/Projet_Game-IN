using AllosiusDev.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    #region Fields

    private PlayerScript playerScript;

    [Header("Inputs")]
    private PlayerActionControls PlayerActionControllers;
    private bool canMove;

    private float InitialMoveSpeed;

    Scene scene;

    private Vector2 LastMoveDirection;
    private Vector2 MoveDirection;

    bool OnSlope = false;
    float ValueSlopeAdd;

    bool SlopeStartLeft;
    int ElevationValue;

    private enum EDirection
    {
        NONE = 0,
        HORIZONTAL = 1,
        VERTICAL = 2
    }

    private EDirection m_lastDirection = EDirection.NONE;

    private bool m_isMovingVertically = false;
    private bool m_isMovingHorizontally = false;

    private Vector2 move;

    public ParticleSystem smokeExplosion;
    public ParticleSystem scooterSmoke01;
    public ParticleSystem scooterSmoke02;
    

    #endregion

    #region Properties

    public bool PlayerChangeScene { get; set; }
    public bool MakePlayerInGoodSens { get; set; }

    public bool MakeSmokeInGoodSens { get; set; }

    public bool PlayerNeedInitialePosition { get; set; }
    public bool PlayerNeedLookUp { get; set; }
    public bool PlayerNeedLookRight { get; set; }
    public bool PlayerNeedLookLeft { get; set; }


    public bool CanSwitchState { get; set; }

    #endregion

    #region UnityInspector



    [Header ("Movement")]
    [SerializeField] private float MoveSpeed ;
        
        [SerializeField] private float ScooterSpeed ;
    //[SerializeField] private float AmplitudeToSwitchScoot = 0.125f;

        //public AudioSource ScooterStop;
        //public AudioSource ScooterMoving;

        public bool PlayOneShotClip = true;




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

    public void StartActivity() 
    {   
        PlayerActionControllers.Disable();
        canMove = false;
    }
    public void EndActivity() 
    {   
        PlayerActionControllers.Enable();
        canMove = true;
    }

    private void Awake() 
    {
        playerScript = GetComponent<PlayerScript>();

        PlayerActionControllers = new PlayerActionControls();
        PlayerActionControllers.PlayerInLand.EnterScoot.performed += OnEnterScoot;
        PlayerActionControllers.PlayerInScoot.ExitScoot.performed += OnExitScoot;

        InitialMoveSpeed = MoveSpeed ;

        canMove = true;
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
        CheckMoveValue();
        Animate(); 
    }

    public void OnEnterScoot (InputAction.CallbackContext ctx )
    {      
        if(ctx.performed)
        {
            ChangeScootState(true);
            
            scooterSmoke01.Play();
            scooterSmoke02.Play();
            
                
        }
        
    }
    public void OnExitScoot (InputAction.CallbackContext ctx )
    {
        if(ctx.performed)
        {
            ChangeScootState(false);
            scooterSmoke01.Stop();
            scooterSmoke02.Stop();
        }
    }

    public void ChangeScootState(bool value)
    {
        if(InExterior && playerScript.CanSwitchState)
        {
            if (value)
            {
                PlayerActionControllers.PlayerInLand.Disable();
                PlayerActionControllers.PlayerInScoot.Enable();
                switchScootState(true);
            }
            else
            {
                if (OnScooter)
                {
                    PlayerActionControllers.PlayerInScoot.Disable();
                    PlayerActionControllers.PlayerInLand.Enable();
                    switchScootState(false);
                    scooterSmoke01.Stop();
                    scooterSmoke02.Stop();
                }
            }
        }   
    }


    public void switchScootState(bool state)
    {
        Debug.Log("Switch Scoot State");

        OnScooter = state;
        if (Animators[0].runtimeAnimatorController != null)
        {
            if(state == true)
            {
                Animators[0].SetTrigger("InScoot");
                AudioController.Instance.PlayAudio(sfxScooterStop);
                Explosion();
            }
            else
            {
                Animators[0].SetTrigger("OnWalk");
                AudioController.Instance.StopAudio(sfxScooterStop);
                AudioController.Instance.StopAudio(sfxScooterMoving);
            }
            
        }

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
        //Debug.Log("Process Inputs");
        move = Vector2.zero;

        //Vector2 moveLand = PlayerActionControllers.PlayerInLand.Move.ReadValue<Vector2>();

        //Vector2 moveScoot = PlayerActionControllers.PlayerInScoot.MoveScoot.ReadValue<Vector2>();

        //Vector2 moveOldSyst = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //Debug.Log(moveOldSyst);

        //Debug.Log(moveLand + " " + moveScoot + " " + moveOldSyst);

        if (!PlayerChangeScene && canMove)
        {
            float vertical = Input.GetAxisRaw("Vertical");
            float horizontal = Input.GetAxisRaw("Horizontal");
            if (vertical == 0f)
            {
                m_isMovingVertically = false;
            }
            if (horizontal == 0f)
            {
                m_isMovingHorizontally = false;
            }
            if (vertical == 0f && horizontal == 0f)
            {
                m_lastDirection = EDirection.NONE;
                if ((move.x == 0 && move.y == 0) && MoveDirection.x != 0 || MoveDirection.y != 0)
                    LastMoveDirection = MoveDirection;
                MoveDirection = Vector2.zero;
                return;
            }

            if (!m_isMovingVertically && vertical != 0f)
            {
                // The user has just pressed a vertical key
                m_lastDirection = EDirection.VERTICAL;
                //anim.SetFloat("hSpeed", 0);
                m_isMovingVertically = true;
            }
            else if (!m_isMovingHorizontally && horizontal != 0f)
            {
                // The user has just pressed an horizontal key
                m_lastDirection = EDirection.HORIZONTAL;
                //anim.SetFloat("vSpeed", 0);
                m_isMovingHorizontally = true;
            }

            if(Input.GetButtonUp("Vertical"))
            {
                if(m_isMovingHorizontally)
                    m_lastDirection = EDirection.HORIZONTAL;
            }
            if (Input.GetButtonUp("Horizontal"))
            {
                if(m_isMovingVertically)
                    m_lastDirection = EDirection.VERTICAL;
            }



            move = Vector2.zero;
            switch (m_lastDirection)
            {
                case EDirection.VERTICAL:
                    move = (vertical * Vector2.up).normalized;
                    break;
                case EDirection.HORIZONTAL:
                    move = (horizontal * Vector2.right).normalized;
                    break;
                default:
                    break;
            }

            

            //Move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            //Debug.Log(Move);
            /*if (!OnScooter)
            {
                Move = PlayerActionControllers.PlayerInLand.Move.ReadValue<Vector2>();

            }
            else
            {
                Move = PlayerActionControllers.PlayerInScoot.MoveScoot.ReadValue<Vector2>();

            }*/
        }/* else {*/
        //Debug.Log(Move);    
        if(MakePlayerInGoodSens) 
            {
                if(PlayerNeedInitialePosition)
                {
                    PlayerNeedInitialePosition = false ;             
                    move = new Vector2(0, -1f);
                } 

                if(PlayerNeedLookUp)
                {
                    PlayerNeedLookUp = false ;   
                    move = new Vector2(0, 1f);
                } 

                if(PlayerNeedLookLeft)
                {
                    PlayerNeedLookLeft = false ;   
                    move = new Vector2(-1f, 0f);
                } 

                if(PlayerNeedLookRight)
                {
                    PlayerNeedLookRight = false ;   
                    move = new Vector2(1f, 0f);
                } 
                MakePlayerInGoodSens = false ;  
                PlayerChangeScene = false ;                 
            }
      //  }



        if((move.x == 0 && move.y == 0) && MoveDirection.x != 0 || MoveDirection.y != 0)
            LastMoveDirection = MoveDirection ;

        

        MoveDirection = move;

        //MoveDirection = Move.normalized ;
        //if((Move.x != 0 && Move.y != 0) || (Move.x == 0 && Move.y == 0)) MoveDirection = new Vector2 (0, 0).normalized ;
        //if(Move.x != 0) MoveDirection = new Vector2 (Move.x, 0).normalized ;
        //if(Move.y != 0) MoveDirection = new Vector2 (0, Move.y).normalized ;



        for (int i = 0; i < PlayerRenderers.Count; i++)
        {
            // Flip et DéFlip X en fonction de Vector2 Move 
            if(MoveDirection.x < 0)              
                PlayerRenderers[i].flipX = true ; 
            if(MoveDirection.x > 0 || MoveDirection.y != 0)        
                PlayerRenderers[i].flipX = false ;     
        }
    }

    private void CheckMoveValue()
    {
        Debug.Log(move + " " + MoveDirection);

        if ((move.x != 0 || move.y != 0) && OnScooter == true && PlayOneShotClip == false)
        {
            ScootMovingForward();
        }
        if ((move.x == 0 && move.y == 0) && OnScooter == true && PlayOneShotClip == true)
        {
            ScootNotMoving();
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
        var smoke01 = scooterSmoke01.emission;
        var smoke02 = scooterSmoke02.emission;
        smoke01.rateOverTime = 2f;
        smoke02.rateOverTime = 1f;
    }

    void ScootMovingForward() {
        PlayOneShotClip = true;

        //ScooterMoving.Play();
        //ScooterStop.Stop();
        Debug.Log("Scoot Moving Forward");
        AudioController.Instance.PlayAudio(sfxScooterMoving);
        AudioController.Instance.StopAudio(sfxScooterStop);
        var smoke01 = scooterSmoke01.emission;
        var smoke02 = scooterSmoke02.emission;
        smoke01.rateOverTime = 6.75f;
        smoke02.rateOverTime = 4f;
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

    public void Explosion()
    {
        smokeExplosion.Play();
    }

    #endregion
}
