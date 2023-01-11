using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public ObstacleScriptableObject obstacleData;
    Grid grid;

    private void Awake()
    {
        grid = GetComponent<Grid>();
    }

   public void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Node startNode = grid.NodeFromCoordinates(startPos);
        Node targetNode = grid.NodeFromCoordinates(targetPos);

        if(startNode==targetNode)
            return;

        List<Node> openSet = new List<Node>();              // Contains all nodes that are to be evaluated
        List<Node> closedSet = new List<Node>();            // Contains all the evaluated nodes
        openSet.Add(startNode);

        while (openSet.Count>0)
        {
            Node currentNode = openSet[0];

            for(int i=0; i < openSet.Count; i++)            // Traverse the elements of open set and set the node with the  
            {                                               // lowest f cost as current Node
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);                 // Add the current node to closed set

            if (currentNode == targetNode)              // call RetracePath function if current node is the target node
            {                                           // i.e. the algorithm has reached the end, and now we retrace the path
                RetracePath(startNode, targetNode);
                return;
            }

            foreach (Node neighbour in grid.GetNeighbours(currentNode))     // get all neighbours of current node
            {
                if(obstacleData.IsObstacle(neighbour.gridX,neighbour.gridY) || closedSet.Contains(neighbour))
                    continue;       // ignore all the neighbours which are already in closed set or contain obstacles

                // calculate new g cost for current node
                int updatedCost = currentNode.gCost + GetDistance(currentNode, neighbour);
                if (updatedCost < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = updatedCost;
                    neighbour.hCost = GetDistance(neighbour,targetNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
    }

    // starts from endNode,adds it to path list, checks its parent node, adds it to path list, continues similarly
    // and retraces back to the startNode.
    // The path list is then reversed and a list of nodes is obtained which is the shortest path from startNode to endNode
    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;
        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        grid.path = path;
    }

    int GetDistance (Node nodeA, Node nodeB)
    {
          int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
          int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);
          return 10 * (dstX + dstY);
    }
}

