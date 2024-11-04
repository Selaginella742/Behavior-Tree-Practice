using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Skeleton implementation written by Joe Zachary for CS 3500, September 2013
// Version 1.1 - Joe Zachary
//   (Fixed error in comment for RemoveDependency)
// Version 1.2 - Daniel Kopta Fall 2018
//   (Clarified meaning of dependent and dependee)
//   (Clarified names in solution/project structure)
// Version 1.3 - H. James de St. Germain Fall 2024
// Version 1.4 - Junyang Huang 9/14/2024
// Version 1.5 - Junyang Huang 4/28/2024 used for behavior tree project

namespace PracticeBT
{
    public class BTDependencyGraph
    {
        private Dictionary<string, HashSet<string>> dependents;
        private Dictionary<string, HashSet<string>> dependees;

        private int size; // record the amount of ordered pairs in the graph

        /// <summary>
        ///   Initializes a new instance of the <see cref="DependencyGraph"/> class.
        ///   The initial DependencyGraph is empty.
        /// </summary>
        public BTDependencyGraph()
        {
            dependees = new Dictionary<string, HashSet<string>>();
            dependents = new Dictionary<string, HashSet<string>>();

            size = 0;
        }

        /// <summary>
        /// The number of ordered pairs in the DependencyGraph.
        /// </summary>
        public int Size
        {
            get { return size; }
        }

        /// <summary>
        ///   Reports whether the given node has dependents (i.e., other nodes depend on it).
        /// </summary>
        /// <param name="nodeName"> The name of the node.</param>
        /// <returns> true if the node has dependents. </returns>
        public bool HasDependents(string nodeName)
        {
            return dependents.ContainsKey(nodeName) && dependents[nodeName].Count != 0;
        }

        /// <summary>
        ///   Reports whether the given node has dependees (i.e., depends on one or more other nodes).
        /// </summary>
        /// <returns> true if the node has dependees.</returns>
        /// <param name="nodeName">The name of the node.</param>
        public bool HasDependees(string nodeName)
        {
            return dependees.ContainsKey(nodeName) && dependees[nodeName].Count != 0;
        }

        /// <summary>
        ///   <para>
        ///     Returns the dependents of the node with the given name.
        ///   </para>
        /// </summary>
        /// <param name="nodeName"> The node we are looking at.</param>
        /// <returns> The dependents of nodeName. </returns>
        public IEnumerable<string> GetDependents(string nodeName)
        {
            List<string> result = new List<string>();
            if (HasDependents(nodeName))
                result.AddRange(dependents[nodeName]);

            return result;
        }

        /// <summary>
        ///   <para>
        ///     Returns the dependees of the node with the given name.
        ///   </para>
        /// </summary>
        /// <param name="nodeName"> The node we are looking at.</param>
        /// <returns> The dependees of nodeName. </returns>
        public IEnumerable<string> GetDependees(string nodeName)
        {
            List<string> result = new List<string>();
            if (HasDependees(nodeName))
                result.AddRange(dependees[nodeName]);

            return result;
        }

        /// <summary>
        /// <para>Adds the ordered pair (dependee, dependent), if it doesn't exist.</para>
        ///
        /// <para>
        ///   This can be thought of as: dependee must be evaluated before dependent
        /// </para>
        /// </summary>
        /// <param name="dependee"> the name of the node that must be evaluated first</param>
        /// <param name="dependent"> the name of the node that cannot be evaluated until after dependee</param>
        public void AddDependency(string dependee, string dependent)
        {
            if (AddDependentForDependee(dependee, dependent) &&
            AddDependeeForDependent(dependee, dependent))
                size++;
        }

        /// <summary>
        ///   <para>
        ///     Removes the ordered pair (dependee, dependent), if it exists.
        ///   </para>
        /// </summary>
        /// <param name="dependee"> The name of the node that must be evaluated first</param>
        /// <param name="dependent"> The name of the node that cannot be evaluated until after dependee</param>
        public void RemoveDependency(string dependee, string dependent)
        {
            if (RemoveDependentFromList(dependee, dependent) &&
            RemoveDependeeFromList(dependee, dependent))
                size--;
        }

        /// <summary>
        ///   Removes all existing ordered pairs of the form (nodeName, *).  Then, for each
        ///   t in newDependents, adds the ordered pair (nodeName, t).
        /// </summary>
        /// <param name="nodeName"> The name of the node who's dependents are being replaced </param>
        /// <param name="newDependents"> The new dependents for nodeName</param>
        public void ReplaceDependents(string nodeName, IEnumerable<string> newDependents)
        {
            var old = (List<string>)GetDependents(nodeName);

            foreach (var oldDependent in old)
            {
                RemoveDependency(nodeName, oldDependent);
            }


            foreach (var newDepndent in newDependents)
            {
                AddDependency(nodeName, newDepndent);
            }
        }

        /// <summary>
        ///   <para>
        ///     Removes all existing ordered pairs of the form (*, nodeName).  Then, for each
        ///     t in newDependees, adds the ordered pair (t, nodeName).
        ///   </para>
        /// </summary>
        /// <param name="nodeName"> The name of the node who's dependees are being replaced</param>
        /// <param name="newDependees"> The new dependees for nodeName</param>
        public void ReplaceDependees(string nodeName, IEnumerable<string> newDependees)
        {
            var old = GetDependees(nodeName);

            foreach (var oldDependee in old)
            {
                RemoveDependency(oldDependee, nodeName);
            }


            foreach (var newDepndee in newDependees)
            {
                AddDependency(newDepndee, nodeName);
            }
        }


        /// <summary>
        /// add the dependent on the dependents dictionary and dependee as the node key.
        /// </summary>
        /// <param name="dependee"> The name of the node that must be evaluated first</param>
        /// <param name="dependent"> The name of the node that cannot be evaluated until after dependee</param>
        private bool AddDependentForDependee(string dependee, string dependent)
        {
            dependents.TryAdd(dependee, new HashSet<string>());

            return dependents[dependee].Add(dependent);
        }

        /// <summary>
        /// add the dependee on the dependees dictionary and dependent as the node key.
        /// </summary>
        /// <param name="dependee"> The name of the node that must be evaluated first</param>
        /// <param name="dependent"> The name of the node that cannot be evaluated until after dependee</param>
        private bool AddDependeeForDependent(string dependee, string dependent)
        {
            dependees.TryAdd(dependent, new HashSet<string>());

            return dependees[dependent].Add(dependee);
        }


        /// <summary>
        /// remove the odered pair from the dependents list, and clear out the record of dependee that has no dependents
        /// </summary>
        /// <param name="dependee"> The name of the node that must be evaluated first</param>
        /// <param name="dependent"> The name of the node that cannot be evaluated until after dependee</param>
        private bool RemoveDependentFromList(string dependee, string dependent)
        {
            if (!dependents.ContainsKey(dependee))
            {
                return false;
            }

            dependents[dependee].Remove(dependent);

            if (dependents[dependee].Count <= 0)
            {
                dependents.Remove(dependee);
            }

            return true;
        }

        /// <summary>
        /// remove the odered pair from the dependees list, and clear out the record of dependent that has no dependees
        /// </summary>
        /// <param name="dependee"> The name of the node that must be evaluated first</param>
        /// <param name="dependent"> The name of the node that cannot be evaluated until after dependee</param>
        private bool RemoveDependeeFromList(string dependee, string dependent)
        {
            if (!dependees.ContainsKey(dependent))
            {
                return false;
            }

            dependees[dependent].Remove(dependee);

            if (dependees[dependent].Count <= 0)
            {
                dependees.Remove(dependent);
            }

            return true;
        }
    }
}
