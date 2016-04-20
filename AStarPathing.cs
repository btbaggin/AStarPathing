using Microsoft.VisualBasic;
using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace AStarPathing
{
    public class AStarPathing
    {
        IMap _map;
        //SkipList<Node> _openList; //* List of nodes we can evaluate
        SortedNodeList<Node> _openList;

        #region "AStarPathfinding Constuctor"
        public AStarPathing(IMap pMap)
        {
            if (pMap == null)
            {
                throw new ArgumentNullException("pMap");
            }
            _map = pMap;
            _openList = new SortedNodeList<Node>();// _openList = new SkipList<Node>();
        }
        #endregion

        #region "AStarPathfinding Public Functions"
        /// <summary>
        /// Finds the most efficient path from the start node to end node using the A* pathfinding algorithm.
        /// </summary>
        /// <param name="pStart">Start node of the path</param>
        /// <param name="pEnd">End node of the path</param>
        /// <returns>Path of rectangles that represent the most efficient path starting with end node at index 0 and start node at last index.</returns>
        /// <remarks>Map, start node, and end node must all be instiated before calling this functionv</remarks>
        public IEnumerable<Node> FindPath(Node pStart, Node pEnd)
	    {
            if (pStart == null)
            {
                throw new ArgumentNullException("pStart");
            }
            if (pEnd == null)
            {
                throw new ArgumentNullException("pEnd");
            }
            Node currentNode = pStart;

		    //Make sure our lists are clear before we start
		    _openList.Clear();
		    _map.ResetNodes();

		    //We are at the start node
            _openList.Push(pStart);//_openList.Add(pStart);

		    //while we are not at the goal
		    while (_openList.Count > 0)
            {
			    //Stop searching if we are at the goal
                if (currentNode == pEnd)
                {
                    break; 
                }

			    //Set the parent nodes and move them into the open list
                foreach (Node n in _map.GetNeighbors(currentNode)) 
                {
                    if (n.ParentNode == null)
                    {
                        _map.AddToNavigationPath(n, currentNode, pEnd);
                    }
                    if (!_openList.Contains(n))
                    {
                        _openList.Push(n);// _openList.Add(n);
                    }
			    }

			    //move current node from open to closed list
			    currentNode.Evaluated = true;

			    //current node is the one with the lowest F cost
                currentNode = _openList.Pop();
                //currentNode = _openList.First();
                //_openList.Remove(currentNode);
		    }

		    return CreateNavList(currentNode);
	    }
        #endregion

        #region "AStarPathfinding Private Functions"
        /// <summary>
        /// Returns the a list of nodes representing the most efficent path from the start node to the end node
        /// </summary>
        /// <param name="pNode">Last node in the path</param>
        private List<Node> CreateNavList(Node pNode)
        {
            List<Node> retval = new List<Node>();
            Node tempNode = pNode;

            if (tempNode.ParentNode == null)
            {
                retval.Add(tempNode);
                return retval;
            }

            //While we have not reached the start node
            while (tempNode.ParentNode != null)
            {
                //Add this node to our list
                retval.Add(tempNode);
                tempNode = tempNode.ParentNode;
            }

            return retval;
        }
        #endregion
    }
}
