using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BT
{
    /// <summary>
    /// the behavior tree evaluate root's tick behavior and contains the feature to add child node to certain node in the BT
    /// </summary>
    public class BehaviorTree 
    {
        /// <summary>
        /// the root node of BT
        /// </summary>
        MyBTNode root;


        public BehaviorTree(MyBTNode root)
        {
            this.root = root;
        }

        /// <summary>
        /// perform the root's tick and return the result, root will tick their children until it reach the avaliable leaf node
        /// </summary>
        /// <returns></returns>
        public NodeStatus Tick()
        {
            return root.Tick();
        }

        /// <summary>
        /// find the node based on the name and add child under the node
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        public void AddChildForNode(string parent, MyBTNode child)
        {
            var target = FindNode(parent);

            target?.AddChild(child);
        }

        /// <summary>
        /// set the BT's root node
        /// </summary>
        /// <param name="node"></param>
        public void SetRoot(MyBTNode node)
        {
            root = node;
        }

        /// <summary>
        /// reset the whole tree
        /// </summary>
        public void ResetTree()
        {
            root.Reset();
        }


        /// <summary>
        /// find the node in the BT that matches the name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private MyBTNode FindNode(string name)
        {
            Stack<MyBTNode> nodesToView = new Stack<MyBTNode>();
            nodesToView.Push(root);

            while (nodesToView.Count > 0) //BFS search
            {
                var target = nodesToView.Pop();

                if (target.name == name)
                    return target;

                var children = target.GetChildren();

                foreach (var node in children)
                {
                    nodesToView.Push(node);
                }
            }

            return null;
        }
    }
}
