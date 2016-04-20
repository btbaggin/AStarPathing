using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarPathing
{
    public interface IMap
    {
        /// <summary>
        /// Number of nodes in a row of the map
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Number of nodes in a column of the map
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Returns the node at the specified location
        /// </summary>
        /// <param name="pX">X value of the node to return</param>
        /// <param name="pY">Y value of the node to return</param>
        /// <returns>Node at the given X, Y coordinates</returns>
        Node GetNode(int pX, int pY);

        /// <summary>
        /// Sets the type of land of the node at the specified location
        /// </summary>
        /// <param name="pX">X value of the node to set the land type</param>
        /// <param name="pY">Y value of the node to set the land type</param>
        /// <param name="pLandType">Node to set</param>
        void SetNode(int pX, int pY, ref Node pNode);

        /// <summary>
        /// Returns all eligible neighbors of the given node
        /// </summary>
        /// <param name="pNode">Node of which to retrieve the neighbors</param>
        /// <returns>List of nodes that contain all eligible neighbors</returns>
        IEnumerable<Node> GetNeighbors(Node pNode);

        /// <summary>
        /// Calculates the FCost of moving to pCurrent from pPrevious
        /// </summary>
        /// <param name="pCurrent">Node we are calculating the FCost for</param>
        /// <param name="pPrevious">Node we are coming from</param>
        /// <param name="pEnd">End location of the path finding</param>
        void CalcFCost(Node pCurrent, Node pPrevious, Node pEnd);

        /// <summary>
        /// Adds pCurrent to the navigation path and calculates the FCost
        /// </summary>
        /// <param name="pCurrent">Node to Add to the path</param>
        /// <param name="pPrevious">Node we are coming from</param>
        /// <param name="pEnd">End location of the path finding</param>
        void AddToNavigationPath(Node pCurrent, Node pPrevious, Node pEnd);

        /// <summary>
        /// Resets the nodes to unevaluated and removes all parent nodes
        /// </summary>
        void ResetNodes();
    }
}
