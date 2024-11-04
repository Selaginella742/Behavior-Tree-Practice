using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class Condition : MyBTNode
    {

        Func<bool> condition;

        public Condition(string name, Func<bool> condition) : base(name)
        {
            this.condition = condition;
        }

        protected override void OnFinish()
        {

        }

        protected override void OnStart()
        {

        }

        protected override NodeStatus OnUpdate()
        {
            if (condition())
                return NodeStatus.SUCCESS;
            else
                return NodeStatus.FAIL;
        }
    }
}
