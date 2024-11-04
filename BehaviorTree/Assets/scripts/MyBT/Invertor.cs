using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class Invertor : Decorator
    {
        public Invertor(string name) : base(name)
        {
        }

        protected override void OnFinish() { }

        protected override void OnStart() { }

        protected override NodeStatus OnUpdate()
        {
            var status = child.Tick();

            if (status == NodeStatus.SUCCESS)
                status = NodeStatus.FAIL;
            else if (status == NodeStatus.FAIL)
                status = NodeStatus.SUCCESS;

            return status;
        }
    }
}
