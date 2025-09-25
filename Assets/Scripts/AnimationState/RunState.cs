using UnityEngine;

public class RunState : State
{
    private float speed;

    public RunState(Animator animator, float speed) : base(animator) 
    {
        this.speed = speed;
    } 

   
    public override void Enter() 
    {
       animator.SetBool("IsRunning", true); 
    }
    public override void Update() 
    {
       animator.SetFloat("IsRunning", speed); 
    }
    public override void Exit() 
    {
       animator.SetBool("IsRunning", false);
    }

}