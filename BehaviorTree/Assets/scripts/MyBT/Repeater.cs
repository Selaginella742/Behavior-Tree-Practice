using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class Repeater : Decorator
    {
        bool infinite;
        int repeatAmount;

        public Repeater(string name, bool infiniteLoop, int repeatAmount) : base(name)
        {
            infinite = infiniteLoop;
            this.repeatAmount = repeatAmount;
        }

        protected override void OnFinish()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnStart()
        {
            throw new System.NotImplementedException();
        }

        protected override NodeStatus OnUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}
