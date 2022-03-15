using System.Collections.Generic;
using Terrain;

namespace Utils.PathFinding
{
    public class PathNode
    {
        private readonly HexagoneGenerator _hex;

        public HexagoneGenerator Hex => _hex;

        public float gCost;
        public float hCost;
        public float fCost;

        public PathNode cameFromNode;
        
        public PathNode(HexagoneGenerator hex)
        {
            _hex = hex;
            gCost = float.MaxValue;
        }

        public void CalculateFCost()
        {
            fCost = gCost + hCost;
        }
    }
}