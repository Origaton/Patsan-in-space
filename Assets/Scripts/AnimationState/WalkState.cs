using UnityEngine;

public class WalkingState : State
{
    private float speed;

    public WalkingState(Animator animator, float speed) : base(animator) 
    {
        this.speed = speed;
    } 

   
    public override void Enter() 
    {
       animator.SetBool("IsWalking", true);  
    }
    public override void Update() 
    {
       animator.SetFloat("IsWalking", speed);
    }
    public override void Exit() 
    {
       animator.SetBool("IsWalking", false); 
    }


}