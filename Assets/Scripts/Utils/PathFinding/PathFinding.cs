using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terrain;
using UnityEngine;

namespace Utils.PathFinding
{
    /// <summary>
    /// pathfinding A*
    /// source : https://www.youtube.com/watch?v=alU04hvz6L4 / https://fr.wikipedia.org/wiki/Algorithme_A*
    /// </summary>
    public class PathFinding
    {
        private List<PathNode> _openList = new List<PathNode>();
        private List<PathNode> _closeList = new List<PathNode>();

        public List<HexagoneGenerator> FindPath(HexagoneGenerator hexStart, HexagoneGenerator hexEnd)
        {
            PathNode startNode = new PathNode(hexStart);            
            _openList = new List<PathNode>{startNode};
            _closeList = new List<PathNode>();

            startNode.gCost = 0;
            startNode.hCost = Vector3.Distance(hexStart.transform.position, hexEnd.transform.position);
            startNode.CalculateFCost();

            while (_openList.Count > 0)
            {
                var currenNode = GetLowestFCostNode(_openList);
                if (currenNode.Hex == hexEnd)
                {
                    //reach final node
                    return CalculatePath(currenNode);
                }

                _openList.Remove(currenNode);
                _closeList.Add(currenNode);

                foreach (var neighbourNode in GetNeighbourList(currenNode))
                {
                    if (_closeList.Contains(neighbourNode)) continue;
                    var tentativeGCost = currenNode.gCost + CalculateDistanceCost(currenNode, neighbourNode);
                    if (tentativeGCost < neighbourNode.gCost)
                    {
                        neighbourNode.cameFromNode = currenNode;
                        neighbourNode.gCost = tentativeGCost;
                        neighbourNode.hCost = CalculateDistanceCost(neighbourNode.Hex, hexEnd);
                        neighbourNode.CalculateFCost();
                        if (!_openList.Contains(neighbourNode))
                        {
                            _openList.Add(neighbourNode);                            
                        }
                    }
                }
            }
            
            //Out of nodes on the openlist
            return null;
        }

        private float CalculateDistanceCost(PathNode pathNodeA, PathNode pathNodeB)
        {
            return Vector3.Distance(pathNodeA.Hex.transform.position, pathNodeB.Hex.transform.position);
        }
        
        private float CalculateDistanceCost(HexagoneGenerator hexA, HexagoneGenerator hexB)
        {
            return Vector3.Distance(hexA.transform.position, hexB.transform.position);
        }

        private List<PathNode> GetNeighbourList(PathNode node)
        {
            var result = new List<PathNode>();

            if (CanAddNeighbour(node.Hex.E))
            {
                result.Add(new PathNode(node.Hex.E));
            }
            if (CanAddNeighbour(node.Hex.SE))
            {
                result.Add(new PathNode(node.Hex.SE));
            }
            if (CanAddNeighbour(node.Hex.NE))
            {
                result.Add(new PathNode(node.Hex.NE));
            }
            if (CanAddNeighbour(node.Hex.O))
            {
                result.Add(new PathNode(node.Hex.O));
            }
            if (CanAddNeighbour(node.Hex.NO))
            {
                result.Add(new PathNode(node.Hex.NO));
            }
            if (CanAddNeighbour(node.Hex.SO))
            {
                result.Add(new PathNode(node.Hex.SO));
            }
            
            return result;
        }

        private bool CanAddNeighbour(HexagoneGenerator hex)
        {
            return hex && hex.gameObject.activeInHierarchy;
        }
        
        private List<HexagoneGenerator> CalculatePath(PathNode endNode)
        {
            var result = new List<HexagoneGenerator>();
            result.Add(endNode.Hex);
            PathNode currentNode = endNode;

            while (currentNode.cameFromNode != null)
            {
                result.Add(currentNode.Hex);
                currentNode = currentNode.cameFromNode;
            }

            result.Reverse();
            return result;
        }

        private PathNode GetLowestFCostNode(List<PathNode> pathNodes)
        {
            var result = pathNodes[0];

            for (var i = 1; i < pathNodes.Count; i++)
            {
                var item = pathNodes[i];
                if (result.fCost > item.fCost)
                {
                    result = item;
                }
            }

            return result;
        }

    }
}