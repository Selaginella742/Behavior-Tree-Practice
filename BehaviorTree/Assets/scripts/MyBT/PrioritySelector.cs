using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class PrioritySelector : Selector
    {
        public PrioritySelector(string name) : base(name)
        {
        }

        protected override NodeStatus OnUpdate()
        {
            var prev = currentChildCount;
            base.OnStart();

            var res = base.OnUpdate();

            if (prev != children.Count - 1 && prev != currentChildCount)
                children[prev].StopNode();

            return res;
        }
    }
}
