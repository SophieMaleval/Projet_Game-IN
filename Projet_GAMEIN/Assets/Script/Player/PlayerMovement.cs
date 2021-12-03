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


    

    private void OnEnable() {   PlayerActionControllers.Enable();   }
    private void OnDisable() {   PlayerActionControllers.Disable();   }

    public void StartDialog() {   PlayerActionControllers.Disable();   }
    private void EndDialog() {   PlayerActionControllers.Enable();   }

    private void Awake() 

    {  
          PlayerActionControllers = new PlayerActionControls();
          PlayerActionControllers.PlayerInLand.EnterScoot.performed+= OnEnterScoot;
          PlayerActionControllers.PlayerInScoot.ExitScoot.performed+= OnEnterScoot;}

    void Update()
    {
        ProcessInputs();
        Animate();
    }

    public void OnEnterScoot (InputAction.CallbackContext ctx ){

        if (ctx.performed)
        {
            switchScootState(true);
           
        }
    }
    public void OnExitScoot (InputAction.CallbackContext ctx ){

        if (ctx.performed)
        {
            switchScootState(false);
           
        }
    }

    public void switchScootState(bool state){
         OnScooter = state;
              for (int i = 0; i < Animators.Count; i++)
        {
            if(Animators[i].runtimeAnimatorController != null)
            
               
                Animators[i].SetBool("InScoot", state);   
        }
    }



    private void FixedUpdate() 
    {    
        Move();
    
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
}
