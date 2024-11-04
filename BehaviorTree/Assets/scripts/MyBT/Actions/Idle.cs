using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT;
public class Idle : MyBTNode
{
    Animator an;
    public Idle(string name, Animator an) : base(name)
    {
        this.an = an;
    }

    protected override void OnFinish()
    {
        
    }

    protected override void OnStart()
    {
        an.Play("idle");
        
    }

    protected override NodeStatus OnUpdate()
    {
        return NodeStatus.RUNNING;
    }
}
