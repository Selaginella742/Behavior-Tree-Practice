using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BT
{
    /// <summary>
    /// a node with condition.  If the condition meets then execute later nodes
    /// </summary>
    public class BTConditionFilter : Sequence
    {
        public BTConditionFilter(string name, Condition condition) : base(name)
        {
            AddChild(condition);
        }
    }
}
