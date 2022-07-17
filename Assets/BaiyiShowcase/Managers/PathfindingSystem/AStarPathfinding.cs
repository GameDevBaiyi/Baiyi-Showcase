using System.Collections.Generic;
using System.Linq;
using BaiyiShowcase.Managers.GridManager;
using BaiyiUtilities.Singleton;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.PathfindingSystem
{
    public class AStarPathfinding : Singleton<AStarPathfinding>
    {
        [Required]
        [SerializeField] private PathfindingGrid _pathfindingGrid;
        [Required]
        [SerializeField] private GridSystem _gridSystem;


        public List<Vector2> GetPath(Vector2 startPosition, Vector2 targetPosition)
        {
            PathfindingNode targetNode = _pathfindingGrid.WorldPositionToNode(targetPosition);
            List<Vector2> waypointsPath = new List<Vector2>();
            if (!targetNode.walkable) return waypointsPath;

            PathfindingNode startNode = _pathfindingGrid.WorldPositionToNode(startPosition);

            List<PathfindingNode> openSet = new List<PathfindingNode>();
            List<PathfindingNode> closedSet = new List<PathfindingNode>();

            openSet.Add(startNode);
            InitializeStartNode();


            int iterations = 0;
            while (openSet.Count > 0)
            {
                iterations++;
                if (iterations > 1000)
                {
                    Debug.Log("导航点太远了, A* Pathfinding算法只做了基本实现作为基本功展示, 太远可能会卡");
                    PathfindingNode maxGCost = openSet[0];
                    foreach (PathfindingNode pathfindingNode in openSet)
                    {
                        if (pathfindingNode.gCost > maxGCost.gCost)
                        {
                            maxGCost = pathfindingNode;
                        }
                    }


                    targetNode = maxGCost;

                    CalculatePath();
                    return waypointsPath;
                }

                PathfindingNode current = openSet[0];
                foreach (PathfindingNode node in openSet)
                {
                    if (node.FCost < current.FCost || (node.FCost == current.FCost && node.hCost < current.hCost))
                    {
                        current = node;
                    }
                }

                if (current == targetNode)
                {
                    CalculatePath();
                    return waypointsPath;
                }

                openSet.Remove(current);
                closedSet.Add(current);

                foreach (PathfindingNode neighbor in _pathfindingGrid.Get8DNeighbors(current.coord)
                             .Where(t => t.walkable && !closedSet.Contains(t)))
                {
                    int newGCost = current.gCost + _pathfindingGrid.CalculateDistance(current, neighbor);
                    if (openSet.Contains(neighbor))
                    {
                        if (newGCost < neighbor.gCost)
                        {
                            neighbor.gCost = newGCost;
                            neighbor.parent = current;
                        }
                    }
                    else
                    {
                        neighbor.gCost = newGCost;
                        neighbor.hCost = _pathfindingGrid.CalculateDistance(neighbor, targetNode);
                        neighbor.parent = current;
                        openSet.Add(neighbor);
                    }
                }

                void CalculatePath()
                {
                    List<PathfindingNode> pathNodes = new List<PathfindingNode>();
                    pathNodes.Add(targetNode);
                    PathfindingNode traceNode = targetNode;
                    while (traceNode != startNode)
                    {
                        traceNode = traceNode.parent;
                        pathNodes.Add(traceNode);
                    }

                    pathNodes.Reverse();

                    int previousDistance = 0;
                    for (int i = 0; i < pathNodes.Count(); i++)
                    {
                        if (pathNodes[i] == targetNode)
                        {
                            waypointsPath.Add(_gridSystem.CoordToWorldPosition(pathNodes[i].coord));
                            break;
                        }

                        int nextDistance = _pathfindingGrid.CalculateDistance(pathNodes[i], pathNodes[i + 1]);


                        if (nextDistance != previousDistance && i != 0)
                        {
                            waypointsPath.Add(_gridSystem.CoordToWorldPosition(pathNodes[i].coord));
                        }

                        previousDistance = nextDistance;
                    }
                }
            }

            return waypointsPath;

            void InitializeStartNode()
            {
                startNode.gCost = 0;
                startNode.hCost = _pathfindingGrid.CalculateDistance(startNode, targetNode);
                startNode.parent = null;
            }
        }

        public List<Vector2> GetRestrictedPath(Vector2 startPosition, Vector2 targetPosition, int maxIterations)
        {
            PathfindingNode targetNode = _pathfindingGrid.WorldPositionToNode(targetPosition);
            List<Vector2> waypointsPath = new List<Vector2>();
            if (!targetNode.walkable) return waypointsPath;

            PathfindingNode startNode = _pathfindingGrid.WorldPositionToNode(startPosition);
            if (startNode == targetNode) return waypointsPath;

            List<PathfindingNode> openSet = new List<PathfindingNode>();
            List<PathfindingNode> closedSet = new List<PathfindingNode>();

            openSet.Add(startNode);
            InitializeStartNode();


            int iterations = 0;
            while (openSet.Count > 0)
            {
                PathfindingNode current = openSet[0];
                foreach (PathfindingNode node in openSet)
                {
                    if (node.FCost < current.FCost || (node.FCost == current.FCost && node.hCost < current.hCost))
                    {
                        current = node;
                    }
                }

                if (iterations > maxIterations)
                {
                    PathfindingNode maxGCost = openSet[0];
                    foreach (PathfindingNode pathfindingNode in openSet)
                    {
                        if (pathfindingNode.gCost > maxGCost.gCost)
                        {
                            maxGCost = pathfindingNode;
                        }
                    }


                    targetNode = maxGCost;

                    CalculatePath();
                    return waypointsPath;
                }

                if (current == targetNode)
                {
                    CalculatePath();
                    return waypointsPath;
                }


                openSet.Remove(current);
                closedSet.Add(current);

                foreach (PathfindingNode neighbor in _pathfindingGrid.Get8DNeighbors(current.coord)
                             .Where(t => t.walkable && !closedSet.Contains(t)))
                {
                    int newGCost = current.gCost + _pathfindingGrid.CalculateDistance(current, neighbor);
                    if (openSet.Contains(neighbor))
                    {
                        if (newGCost < neighbor.gCost)
                        {
                            neighbor.gCost = newGCost;
                            neighbor.parent = current;
                        }
                    }
                    else
                    {
                        neighbor.gCost = newGCost;
                        neighbor.hCost = _pathfindingGrid.CalculateDistance(neighbor, targetNode);
                        neighbor.parent = current;
                        openSet.Add(neighbor);
                    }
                }

                iterations++;
            }

            return waypointsPath;


            void InitializeStartNode()
            {
                startNode.gCost = 0;
                startNode.hCost = _pathfindingGrid.CalculateDistance(startNode, targetNode);
                startNode.parent = null;
            }

            void CalculatePath()
            {
                List<PathfindingNode> pathNodes = new List<PathfindingNode>();
                pathNodes.Add(targetNode);
                PathfindingNode traceNode = targetNode;
                while (traceNode != startNode)
                {
                    traceNode = traceNode.parent;
                    pathNodes.Add(traceNode);
                }

                pathNodes.Reverse();

                int previousDistance = 0;
                for (int i = 0; i < pathNodes.Count(); i++)
                {
                    if (pathNodes[i] == targetNode)
                    {
                        waypointsPath.Add(_gridSystem.CoordToWorldPosition(pathNodes[i].coord));
                        break;
                    }

                    int nextDistance = _pathfindingGrid.CalculateDistance(pathNodes[i], pathNodes[i + 1]);


                    if (nextDistance != previousDistance && i != 0)
                    {
                        waypointsPath.Add(_gridSystem.CoordToWorldPosition(pathNodes[i].coord));
                    }

                    previousDistance = nextDistance;
                }
            }
        }

        public List<Vector2> GetRandomPath(Vector2 currentPosition, int pathLength)
        {
            Vector2 randomTarget = currentPosition + (Vector2)Random.onUnitSphere * pathLength;
            return GetRestrictedPath(currentPosition, randomTarget, pathLength);
        }
    }
}