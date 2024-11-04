using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PracticeBT
{
    public class BTSelector : IBTNode
    {
        readonly Func<IEnumerable<IBTNode>, int> SelectChild;

        public BTSelector (string name, Func<IEnumerable<IBTNode>, int> SelectChild) : base (name)
        {
            this.SelectChild = SelectChild;
        }

        public override NodeState OnUpdate()
        {
            currentChild = SelectChild(Children);

            return Children[currentChild].OnUpdate();
        }
    }
}
