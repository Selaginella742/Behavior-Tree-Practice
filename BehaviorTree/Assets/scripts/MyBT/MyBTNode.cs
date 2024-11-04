using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    /// <summary>
    /// the BT node contains 5 state 
    /// </summary>
    public enum NodeStatus
    {
        RUNNING,
        SUCCESS,
        FAIL,
        UNINITIAL,
        STOP
    }

    /// <summary>
    /// MyBTNode is an abstract base class representing a node in a behavior tree structure.
    /// 
    /// Key functionalities include:
    /// <list type="bullet">
    ///   <item>
    ///     OnStart: Defines the behavior when the node starts execution.
    ///   </item>
    ///   <item>
    ///     OnUpdate: Describes the behavior while the node is running.
    ///   </item>
    ///   <item>
    ///     OnFinish: Handles any clean-up or finishing actions when the node's execution ends.
    ///   </item>
    ///   <item>
    ///     Tick: Controls the node's behavior for each tick, executing start/update/finish behavior based on the node's current status.
    ///   </item>
    ///   
    /// </list>
    /// </summary>
    public abstract class MyBTNode
    {
        /// <summary>
        /// the node's name, the Behavior Tree will check this name when searching certain node
        /// </summary>
        public readonly string name;

        public NodeStatus Status { get { return currentStatus; } }

        protected NodeStatus currentStatus;

        /// <summary>
        /// initialize the node and provide a name
        /// </summary>
        /// <param name="name"></param>
        public MyBTNode(string name)
        {
            this.name = name;
            currentStatus = NodeStatus.UNINITIAL;
        }


        /// <summary>
        /// node's start method, execute when enter this node first time
        /// </summary>
        protected abstract void OnStart();
        /// <summary>
        /// node's finish method, execute when this node's running status end
        /// </summary>
        protected abstract void OnFinish();
        /// <summary>
        /// behavior when the node is running
        /// </summary>
        protected abstract NodeStatus OnUpdate();

        /// <summary>
        /// node's behavior for each tick in the game.  
        /// If the node is not currently running, will execute start behavior; if this node is not running after update behavior, will execute end behavior
        /// </summary>
        /// <returns></returns>
        public NodeStatus Tick()
        {
            if (currentStatus != NodeStatus.RUNNING)
                OnStart();

            currentStatus = OnUpdate();

            if (currentStatus != NodeStatus.RUNNING)
                OnFinish();

            return currentStatus;
        }

        /// <summary>
        /// abstract method for the node
        /// </summary>
        /// <param name="node"></param>
        public virtual void AddChild(MyBTNode node) { }
        public virtual IEnumerable<MyBTNode> GetChildren()
        {
            return new List<MyBTNode>();
        }

        /// <summary>
        /// reset this node into uninitial status
        /// </summary>
        public virtual void Reset()
        {
            currentStatus = NodeStatus.UNINITIAL;
        }

        public void StopNode()
        {
            OnFinish();
            currentStatus = NodeStatus.STOP;
        }

    }


    /// <summary>
    /// composite nodes like sequencer or selector
    /// </summary>
    public abstract class Composite : MyBTNode
    {
        protected List<MyBTNode> children;
        public Composite(string name) : base(name) 
        {
            children = new List<MyBTNode>();
        }

        public override void AddChild(MyBTNode node)
        {
            children.Add(node);
        }

        public override IEnumerable<MyBTNode> GetChildren()
        {
            var children = new List<MyBTNode>();

            foreach (var child in this.children)
            {
                children.Add(child);
            }

            return children;
        }

        public void ClearChild()
        {
            children.Clear();
        }

        public override void Reset()
        {
            base.Reset();
            foreach (var child in children)
            {
                child.Reset();
            }
        }
    }


    /// <summary>
    /// a base class of decorator
    /// </summary>
    public abstract class Decorator : MyBTNode
    {
        protected MyBTNode child = null;

        protected Decorator(string name) : base(name)
        {
        }

        public override void AddChild(MyBTNode node)
        {
            child = node;
        }

        public override IEnumerable<MyBTNode> GetChildren()
        {
            var children = new List<MyBTNode>();

            if (child != null)
                children.Add(child);

            return children;
        }

        public override void Reset()
        {
            base.Reset();
            child.Reset();
        }
    }
}

