using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// one enemy's behavior, the enemy will do following:
/// <list type="bullet">
/// <item>
///     normal case: idle
/// </item>
/// <item>
///     when the player get close: begin guarding
/// </item>
/// <item>
///     when the player get into attack range: meele attack
/// </item>
/// <item>
///     when the player can't be meele attack but in guard range: gun attack
/// </item>
/// </list>
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyBehavior : MonoBehaviour
{
    BehaviorTree enemyBehavior;
    NavMeshAgent agent;
    Animator animator;
    float currentCoolDown = 0;

    [Range(0,20)]
    [SerializeField]float actionCoolDown;

    [Range(0, 50)]
    [SerializeField] float guardRange;

    [Range(0, 50)]
    [SerializeField] float meeleRange;

    [Range(0, 15)]
    [SerializeField] float rotateSpeed;

    [SerializeField] GameObject player;

    void Start()
    {
        currentCoolDown = actionCoolDown;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        enemyBehavior = new BehaviorTree(new PrioritySelector("root"));

        enemyBehavior.AddChildForNode("root", new BTConditionFilter("attacks condition", 
                                                new Condition("can attack", 
                                                () =>
                                                {
                                                    if (Vector3.Distance(transform.position, player.transform.position) <= meeleRange && currentCoolDown <= 0)
                                                    {
                                                        currentCoolDown = actionCoolDown;
                                                        return true;
                                                    }

                                                    return false;
                                                })));
        
        enemyBehavior.AddChildForNode("attacks condition", new RandomSelector("random attacks"));
        enemyBehavior.AddChildForNode("random attacks", new Attack1("attack 1", animator, transform, player.transform, rotateSpeed));
        enemyBehavior.AddChildForNode("random attacks", new Attack2("attack 2", animator, transform, player.transform, rotateSpeed));
        enemyBehavior.AddChildForNode("random attacks", new HeavyAttack("heavy attack", animator, transform, player.transform, rotateSpeed));


        enemyBehavior.AddChildForNode("root", new BTConditionFilter("guard condition",
                                                new Condition("player in range",
                                                ()=> Vector3.Distance(transform.position, player.transform.position) <= guardRange)));

        enemyBehavior.AddChildForNode("guard condition", new Guard("guard", this.transform, player.transform, guardRange, animator, actionCoolDown, rotateSpeed));

        enemyBehavior.AddChildForNode("root", new Idle("idle", animator));

    }

    void Update()
    {
        if ( currentCoolDown >= 0)
        {
            currentCoolDown -= Time.deltaTime;
        }

        enemyBehavior.Tick();
    }

    private void OnAnimatorMove()
    {
        transform.position += animator.deltaPosition;
    }
}
