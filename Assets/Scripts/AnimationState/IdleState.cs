using UnityEngine;

public class IdleState : State
{
    private float speed;

    public IdleState(Animator animator, float speed) : base(animator) 
    {
        this.speed = speed;
    } 

   public override void Enter() 
    {
       
    }
    public override void Update() 
    {
       animator.SetFloat("Idle", speed); 
    }
    public override void Exit() 
    {
       
    }
}
