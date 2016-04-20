using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarPathing
{
    public abstract class Node : IComparable
    {
        #region "Node Properties"
        /// <summary>
        /// Returns the multiplier for moving across this land type
        /// </summary>
        public abstract float TraversalMultiplier { get; }

        /// <summary>
        /// Returns if this node is able to be traversed
        /// </summary>
        public abstract bool Traversable { get; }

        public float FCost { get; set; }
        public float GCost { get; set; }

        public int X { get; protected set; }
        public int Y { get; protected set; }

        public bool Evaluated { get; internal set; }
        public Node ParentNode { get; set; }
        #endregion

        public void Reset()
        {
            ParentNode = null;
            Evaluated = false;
            FCost = 0;
        }
        
        public int CompareTo(object obj)
        {
            Node Node2 = obj as Node;
            if (Node2 == null)
            {
                throw new Exception();
            }

            return FCost.CompareTo(Node2.FCost);
        }
    }
}
