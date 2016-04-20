using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarPathing
{
    class WaterNode : Node
    {
        public override bool Traversable
        {
            get { return true; }
        }

        public override float TraversalMultiplier
        {
            get { return 2f; }
        }

        public WaterNode(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    class LandNode : Node
    {
        public override bool Traversable
        {
            get { return true; }
        }

        public override float TraversalMultiplier
        {
            get { return 1f; }
        }

        public LandNode(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    class MountainNode : Node
    {
        public override bool Traversable
        {
            get { return false; }
        }

        public override float TraversalMultiplier
        {
            get { return 0f; }
        }

        public MountainNode(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
