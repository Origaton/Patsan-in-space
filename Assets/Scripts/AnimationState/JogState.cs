using UnityEngine;

public class JogState : State
{
    private float speed;

    public JogState(Animator animator, float speed) : base(animator) 
    {
        this.speed = speed;
    } 

    public override void Enter() 
    {
       animator.SetBool("IsJoging", true);
    }
    public override void Update() 
    {
       animator.SetFloat("IsJoging", speed);
    }
    public override void Exit() 
    {
       animator.SetBool("IsJoging", false);
    }
}
