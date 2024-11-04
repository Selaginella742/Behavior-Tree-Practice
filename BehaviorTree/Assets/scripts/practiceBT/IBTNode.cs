using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PracticeBT
{
    public enum NodeState
    {
        SUCCESSS,
        RUNNING,
        FAIL
    }

    /// <summary>
    /// base class of behavior tree node
    /// </summary>
    public class IBTNode
    {
        /// <summary>
        /// node's name
        /// </summary>
        public readonly string name;

        /// <summary>
        /// node's children nodes
        /// </summary>
        List<IBTNode> children;

        /// <summary>
        /// property to get and check children
        /// </summary>
        public List<IBTNode> Children { get { return children; } }

        protected int currentChild = 0; 
        public IBTNode(string name) 
        {
            children = new List<IBTNode>();
            this.name = name;

        }

        /// <summary>
        /// each node's action
        /// </summary>
        /// <returns></returns>
        public virtual NodeState OnUpdate()
        {

            return children[currentChild].OnUpdate();
        }

        public void AddChild(IBTNode node) 
        {
            children.Add(node);
        }

        public virtual void Reset()
        {
            currentChild = 0;
            foreach (var child in children)
            {
                child.Reset();
            }
        }
    }

    /// <summary>
    /// leaf node of behavior tree
    /// </summary>
    public class BTLeaf : IBTNode
    {

        IBTLeafStrategy strategy;

        public BTLeaf(string name, IBTLeafStrategy strategy) : base(name)
        {
            
            this.strategy = strategy;
        }

        public override NodeState OnUpdate()
        {
            return strategy.OnUpdate();
        }

        public override void Reset()
        {
            strategy.Reset();
        }

    }


    /// <summary>
    /// the actions that the leaf node make
    /// </summary>
    public interface IBTLeafStrategy
    {
        NodeState OnUpdate();

        void Reset();
    }
}

