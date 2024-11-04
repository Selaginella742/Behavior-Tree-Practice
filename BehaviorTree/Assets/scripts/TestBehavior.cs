using BT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBehavior : MonoBehaviour
{
    
    BehaviorTree testBT;

    void Start()
    {
        testBT = new BehaviorTree(new Selector("root"));

        testBT.AddChildForNode("root", new TestPrint("print 1"));
        testBT.AddChildForNode("root", new TestPrint("print 2"));
        testBT.AddChildForNode("root", new TestPrint("print 3"));

        InvokeRepeating("TestLoop", 2f, 2f);
    }

    void Update()
    {
        testBT.Tick();
    }

    void TestLoop()
    {
        testBT.ResetTree();
    }
}
