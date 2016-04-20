using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarPathing
{
    //Basic rectangular 
    class TileMap : IMap
    {
        Node[,] _map;

        #region "Properties"
        /// <summary>
        /// Number of nodes in a row of the TileMap
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Number of nodes in a column of the TileMap
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Allow moving to adjacent diagonal nodes
        /// </summary>
        public bool AllowDiagonal { get; set; }
        #endregion

        #region "PathingMap Constructor"
        /// <summary>
        /// PathingMap constructor
        /// </summary>
        /// <param name="pWidth">Number of tiles in width</param>
        /// <param name="pHeight">Number of tiles in height</param>
        /// <remarks>Width and height must be divisible by their respective NumOfTiles</remarks>
        public TileMap(int pWidth, int pHeight, Func<int, int, Node> pInitialize)
        {
            Width = pWidth;
            Height = pHeight;

            _map = new Node[Width, Height];

            //Initialize each node in the map
            for (Int16 x = 0; x < Width; x++)
            {
                for (Int16 y = 0; y < Height; y++)
                {
                    _map[x, y] = pInitialize.Invoke(x, y);
                }
            }
        }
        #endregion

        #region "PathingMap Public Functions"
        /// <summary>
        /// Returns the node at the specified location
        /// </summary>
        /// <param name="pX">X value of the node to return</param>
        /// <param name="pY">Y value of the node to return</param>
        /// <returns>Node at the given X, Y coordinates</returns>
        public Node GetNode(int pX, int pY)
        {
            return _map[pX, pY];
        }

        /// <summary>
        /// Sets the value of the node at the given position
        /// </summary>
        /// <param name="pX">X value of the node to set</param>
        /// <param name="pY">Y value of the node to set</param>
        /// <param name="pNode">Value to set the node to</param>
        public void SetNode(int pX, int pY, ref Node pNode)
        {
            _map[pX, pY] = pNode;
        }

        /// <summary>
        /// Resets the nodes to unevaluated and removes all parent nodes
        /// </summary>
        public void ResetNodes()
        {
            //For every node, remove parent nodes and mark it not evaluated
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    _map[x, y].Reset();
                }
            }
        }
        #endregion

        #region "PathingMap Private Functions"
        /// <summary>
        /// Returns whether or not the node is a valid neighbor
        /// </summary>
        /// <param name="pX">X value of the node to evaluate</param>
        /// <param name="pY">Y value of the node to evaluate</param>
        private bool ValidNeighbor(int pX, int pY)
        {
            //If the X or Y is too big or too small return false
            if (pX < 0 || pX >= Width || pY < 0 || pY >= Height)
            {
                return false;
            }

            //Else if it is walkable and not evaluated
            return _map[pX, pY].Traversable && !_map[pX, pY].Evaluated;
        }
        #endregion

        public void AddToNavigationPath(Node pCurrent, Node pPrevious, Node pEnd)
        {
            pCurrent.ParentNode = pPrevious;
            CalcFCost(pCurrent, pPrevious, pEnd);
        }

        public void CalcFCost(Node pCurrent, Node pPrevious, Node pEnd)
        {
            int hCost = 0;
            if (pPrevious == null)
            {
                pCurrent.FCost = 0;
                return;
            }

            //Calculate G cost
            if (Math.Abs(pPrevious.X - pCurrent.X) > 1 || Math.Abs(pPrevious.Y - pCurrent.Y) > 1)
            {
                throw new Exception("You can only compare neighbor nodes");
            }

            //If it is only a horizonal / verical move
            if (pPrevious.X == pCurrent.X || pPrevious.Y == pCurrent.Y)
            {
                pCurrent.GCost = (float)(pPrevious.GCost + (10 * pCurrent.TraversalMultiplier));
            }
            else //If it is a diagonal move
            {
                pCurrent.GCost = (float)(pPrevious.GCost + (14 * pCurrent.TraversalMultiplier));
            }

            //Calculate H cost
            //Move horizontal till we reach our end node
            if (pCurrent.X < pEnd.X)
            {
                for (int i = pCurrent.X; i <= pEnd.X; i++)
                {
                    hCost += 10;
                }
            }
            else if (pCurrent.X > pEnd.X)
            {
                for (int i = pCurrent.X; i >= pEnd.X; i--)
                {
                    hCost += 10;
                }
            }

            //Move vertical until we reach our end node
            if (pCurrent.Y < pEnd.Y)
            {
                for (int i = pCurrent.Y; i <= pEnd.Y; i++)
                {
                    hCost += 10;
                }
            }
            else if (pCurrent.Y > pEnd.Y)
            {
                for (int i = pCurrent.Y; i >= pEnd.Y; i--)
                {
                    hCost += 10;
                }
            }

            pCurrent.FCost = pCurrent.GCost + hCost;
        }

        IEnumerable<Node> IMap.GetNeighbors(Node pNode)
        {
            List<Node> returnList = new List<Node>();
            int x = pNode.X;
            int y = pNode.Y;

            //*** LEFT CENTER
            if (ValidNeighbor(x - 1, y))
            {
                returnList.Add(_map[x - 1, y]);
            }

            //*** TOP CENTER
            if (ValidNeighbor(x, y - 1))
            {
                returnList.Add(_map[x, y - 1]);
            }

            //*** RIGHT CENTER
            if (ValidNeighbor(x + 1, y))
            {
                returnList.Add(_map[x + 1, y]);
            }

            //*** BOTTOM CENTER
            if (ValidNeighbor(x, y + 1))
            {
                returnList.Add(_map[x, y + 1]);
            }

            //*** TOP LEFT
            if (ValidNeighbor(x - 1, y - 1) && AllowDiagonal)
            {
                returnList.Add(_map[x - 1, y - 1]);
            }

            //*** TOP RIGHT
            if (ValidNeighbor(x + 1, y - 1) && AllowDiagonal)
            {
               returnList.Add(_map[x + 1, y - 1]);            
            }

            //*** BOTTOM LEFT
            if (ValidNeighbor(x - 1, y + 1) && AllowDiagonal)
            {
                returnList.Add(_map[x - 1, y + 1]);
            }

            //*** BOTTOM RIGHT
            if (ValidNeighbor(x + 1, y + 1) && AllowDiagonal)
            {
                returnList.Add(_map[x + 1, y + 1]);
            }

            return returnList;
        }
    }
}
