using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT;

public class Guard : MyBTNode
{
    Transform target;
    Transform enemy;

    float range;
    Animator an;
    float cooldown;
    float currentCooldown;
    float rotateSpeed;

    public Guard(string name, Transform enemy, Transform player, float range, Animator an, float cooldown, float rotateSpeed) : base(name)
    {
        this.enemy = enemy;
        target = player;
        this.range = range;
        this.an = an;
        this.cooldown = cooldown;
        this.rotateSpeed = rotateSpeed;
    }

    protected override void OnFinish()
    {
        an.SetFloat("directionX", 0);
        an.SetFloat("directionZ", 0);
        an.SetBool("guard", false);
    }

    protected override void OnStart()
    {
        Debug.Log("guard start");
        an.SetFloat("directionX", Random.Range(-0.5f, 0.5f));
        an.SetFloat("directionZ", Random.Range(-0.5f, 0.5f));

        an.SetBool("guard", true);

        currentCooldown = cooldown;
        
    }

    protected override NodeStatus OnUpdate()
    {
        
        currentCooldown -= Time.deltaTime;

        var pos = new Vector3(target.position.x, enemy.position.y, target.position.z) - enemy.position;
        enemy.rotation = Quaternion.Slerp(enemy.rotation, Quaternion.LookRotation(pos), rotateSpeed*Time.deltaTime);

        if (Vector3.Distance(enemy.position, target.position) <= 1)
        {
            var toward = an.GetFloat("directionZ");
            an.SetFloat("directionZ", -Mathf.Abs(toward));
        }

        if (currentCooldown <= 0)
        {
            an.SetFloat("directionX", Random.Range(-0.5f, 0.5f));
            an.SetFloat("directionZ", Random.Range(-0.5f, 0.5f));
            currentCooldown = cooldown;
        }

            

        return NodeStatus.RUNNING;
    }
}
