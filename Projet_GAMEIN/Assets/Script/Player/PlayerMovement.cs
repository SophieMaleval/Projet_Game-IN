using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header ("Inputs")]
    private PlayerActionControls PlayerActionControllers ;

    [Header ("Movement")]
    [SerializeField] private float MoveSpeed ;
        [SerializeField] private float ScooterSpeed ;



    public List<SpriteRenderer> PlayerRenderers ;
    public Rigidbody2D RbPlayer ;
    public List<Animator> Animators ;

    private Vector2 LastMoveDirection ;
    private Vector2 MoveDirection ;

    [Header ("Avatar Display")]
    public List<float> ValueColorDisplay ;
    public List<Color> ColorsDisplay ;
    public List<RuntimeAnimatorController> SpriteDisplay ;

    public bool OnScooter = false;
     bool OnSlope = false;
     float ValueSlopeAdd;

     bool SlopeStartLeft;
     int ElevationValue ;
    


    

    private void OnEnable() {   PlayerActionControllers.Enable();   }
    private void OnDisable() {   PlayerActionControllers.Disable();   }

    public void StartDialog() {   PlayerActionControllers.Disable();   }
    public void EndDialog() {   PlayerActionControllers.Enable();   }

    private void Awake() 
    {  
        PlayerActionControllers = new PlayerActionControls();
        PlayerActionControllers.PlayerInLand.EnterScoot.performed += OnEnterScoot;
        PlayerActionControllers.PlayerInScoot.ExitScoot.performed += OnExitScoot;}

    void Update()
    {
        ProcessInputs();
        Animate();
    }

    public void OnEnterScoot (InputAction.CallbackContext ctx )
    {      
        if(ctx.performed && MoveDirection == Vector2.zero)
        {
            PlayerActionControllers.PlayerInLand.Disable() ;
            PlayerActionControllers.PlayerInScoot.Enable() ;
            switchScootState(true);                
        }
    }
    public void OnExitScoot (InputAction.CallbackContext ctx )
    {
        if(ctx.performed && MoveDirection == Vector2.zero)
        {        
            if(OnScooter)
            {
                PlayerActionControllers.PlayerInScoot.Disable() ;                
                PlayerActionControllers.PlayerInLand.Enable() ;
                switchScootState(false);                
            }
        }
    }


    public void switchScootState(bool state)
    {
        OnScooter = state;
    
        if(Animators[0].runtimeAnimatorController != null)
            Animators[0].SetBool("InScoot", state); 

        if(!OnScooter) Animators[0].GetComponent<SpriteRenderer>().color = ColorsDisplay[0];
        else Animators[0].GetComponent<SpriteRenderer>().color = Color.white ;

        for (int i = 1; i < Animators.Count; i++)
        {    Animators[i].gameObject.GetComponent<SpriteRenderer>().enabled = !OnScooter ;  }
    }



    private void FixedUpdate() 
    {    
        Move();
        if(OnSlope == true)
        Slopes();
    
    }
    public void SlopeParameter (bool EnterSlope, float valueSlope, bool BottomAsLeft, int PositionElevation)
    {
        OnSlope = EnterSlope;
        ValueSlopeAdd =  valueSlope; 
        SlopeStartLeft = BottomAsLeft;
        ElevationValue = PositionElevation;
    }

     void Slopes ()
    {
        if(MoveDirection.x != 0){
            if(!SlopeStartLeft)
            {
                if(MoveDirection.x < 0)
                    RbPlayer.velocity += new Vector2 (0, (ValueSlopeAdd * -ElevationValue));

                if(MoveDirection.x > 0)        
                    RbPlayer.velocity += new Vector2 (0, (-1 * ValueSlopeAdd * -ElevationValue)); 
            } else {
                if(MoveDirection.x < 0)
                    RbPlayer.velocity += new Vector2 (0, (-1 * ValueSlopeAdd * ElevationValue));

                if(MoveDirection.x > 0)        
                    RbPlayer.velocity += new Vector2 (0, (ValueSlopeAdd * ElevationValue)); 
            }

            
        }
            
    }

    void ProcessInputs()
    {
        Vector2 Move = Vector2.zero ;
        if(!OnScooter)
            Move = PlayerActionControllers.PlayerInLand.Move.ReadValue<Vector2>();
        else
            Move = PlayerActionControllers.PlayerInScoot.MoveScoot.ReadValue<Vector2>();


        if((Move.x == 0 && Move.y == 0) && MoveDirection.x != 0 || MoveDirection.y != 0)
            LastMoveDirection = MoveDirection ;      


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
        
            RbPlayer.velocity = new Vector2(MoveDirection.x * MoveSpeed, MoveDirection.y * MoveSpeed); 
        else
            RbPlayer.velocity = new Vector2(MoveDirection.x * (MoveSpeed*ScooterSpeed), MoveDirection.y * (MoveSpeed*ScooterSpeed)); 


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

    void RebindAnimation()
    {
        for (int i = 0; i < Animators.Count; i++)
        {    Animators[i].Rebind();  }
    }
}
