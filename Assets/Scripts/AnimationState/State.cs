using UnityEngine;

public abstract class State
{
    protected Animator animator;

    public State(Animator animator)
    {
        this.animator = animator;
    } 

    public abstract void Enter();

    public abstract void Update();

    public abstract void Exit();
}

