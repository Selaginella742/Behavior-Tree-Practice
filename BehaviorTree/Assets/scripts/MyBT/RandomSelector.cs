using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class RandomSelector : Composite
    {
        protected int currentChildCount;
        public RandomSelector(string name) : base(name)
        {
            currentChildCount = 0;
        }

        protected override void OnFinish()
        {
            
        }

        protected override void OnStart()
        {
            
        }

        protected override NodeStatus OnUpdate()
        {
            if (children.Count == 0)
                return NodeStatus.FAIL;

            var res = children[currentChildCount].Tick();

            if (res != NodeStatus.SUCCESS)
            {
                return res;
            }

            
            var roll = Random.Range(0, children.Count);
            while (roll == currentChildCount)
            {
                roll = Random.Range(0, children.Count);
            }
            currentChildCount = roll;
            //Debug.Log($"get node: {children[currentChildCount].name}");

            return res;
        }
    }
}
