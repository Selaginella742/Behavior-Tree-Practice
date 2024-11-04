using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PracticeBT
{
    public class BTSequence: IBTNode
    {
        public BTSequence(string name) : base(name) { }

        public override NodeState OnUpdate()
        {
            if (currentChild < Children.Count)
            {
                switch (Children[currentChild].OnUpdate())
                {
                    case NodeState.RUNNING:
                        return NodeState.RUNNING;
                    case NodeState.FAIL:
                        return NodeState.FAIL;
                    case NodeState.SUCCESSS:
                        currentChild++;
                        return currentChild == Children.Count ? NodeState.SUCCESSS : NodeState.RUNNING;
                }
            }
            Reset();
            return NodeState.SUCCESSS; 
        }
    }
}
