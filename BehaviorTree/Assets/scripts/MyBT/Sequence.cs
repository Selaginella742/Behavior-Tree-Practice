using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class Sequence : Composite
    {
        int currentChildCount;

        public Sequence(string name) : base(name) { }


        protected override void OnFinish()
        {
            
        }

        protected override void OnStart()
        {
            //Debug.Log("sequence start");
            currentChildCount = 0;
        }

        protected override NodeStatus OnUpdate()
        {
            while (currentChildCount < children.Count)
            {
                var status = children[currentChildCount].Tick();

                if (status != NodeStatus.SUCCESS)
                    return status;

                currentChildCount++;

            }

            return NodeStatus.SUCCESS;
        }
    }
}
