using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PracticeBT
{
    public class BehaviorTree: IBTNode
    {
        public BehaviorTree(string name) : base(name) { }

        public override NodeState OnUpdate()
        {
            while (currentChild < Children.Count)
            {
                var status = Children[currentChild].OnUpdate();
                if (status != NodeState.SUCCESSS)
                {
                    return status;
                }
                currentChild++;
            }

            return NodeState.SUCCESSS;
        }
    }
}
