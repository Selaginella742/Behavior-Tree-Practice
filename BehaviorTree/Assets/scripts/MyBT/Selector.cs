using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class Selector : Composite
    {
        protected int currentChildCount;
        public Selector(string name) : base(name)
        {
            currentChildCount = 0;
        }

        protected override void OnFinish()
        {
            
        }

        protected override void OnStart()
        {
            currentChildCount = 0;
        }

        protected override NodeStatus OnUpdate()
        {
            while (currentChildCount < children.Count)
            {
                var status = children[currentChildCount].Tick();
                if (status != NodeStatus.FAIL)
                    return status;

                currentChildCount++;
            }

            return NodeStatus.FAIL;
        }
    }
}
