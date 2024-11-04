using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT;
public class TestPrint : MyBTNode
{
    float second = Time.deltaTime * 2;

    public TestPrint(string name) : base(name)
    {


    }

    protected override void OnFinish()
    {
        Debug.Log($"{name} end \n");
    }

    protected override void OnStart()
    {
        Debug.Log($"{name} start \n");
        second = Time.deltaTime * 2;
    }

    protected override NodeStatus OnUpdate()
    {
        second -= Time.deltaTime;
        Debug.Log($"{name} execute \n");

        if (second >= 0)
            return NodeStatus.RUNNING;
        else
            return NodeStatus.FAIL;
    }

    private IEnumerator TestFail()
    {
        yield return new WaitForSeconds(1);


    }

}
