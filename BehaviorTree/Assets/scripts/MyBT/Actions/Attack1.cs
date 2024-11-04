using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT;
using static UnityEngine.GraphicsBuffer;
public class Attack1 : MyBTNode
{
    protected Animator animator;
    protected Transform subject;
    protected Transform target;
    protected float rotateSpeed;
    public Attack1(string name, Animator an, Transform subject, Transform target, float rotateSpeed) : base(name)
    {
        animator = an;
        this.subject = subject;
        this.target = target;
        this.rotateSpeed = rotateSpeed;
    }

    protected override void OnFinish()
    {
        
    }

    protected override void OnStart()
    {
        animator.SetTrigger("attack1");
    }

    protected override NodeStatus OnUpdate()
    {

        var pos = new Vector3(target.position.x, subject.position.y, target.position.z) - subject.position;
        subject.rotation = Quaternion.Slerp(subject.rotation, Quaternion.LookRotation(pos), rotateSpeed * Time.deltaTime);

        if (animator.GetAnimatorTransitionInfo(0).IsName("attack1_3 -> moving_guard"))
            return NodeStatus.SUCCESS;

        return NodeStatus.RUNNING;
    }
}
