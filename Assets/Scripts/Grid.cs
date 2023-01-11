using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject tile;
    public Node[,] grid;
    public List<Node> path;

    public void Start()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[10, 10];                    // Grid is a 2D array of node objects

        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                Vector3 gridPos = Vector3.zero + Vector3.right * (x) + Vector3.forward * (y); //The position at which new nodes are to be placed
                grid[x, y] = new Node(gridPos, x, y);                          // Using Node constructor, create node at x,y index in grid object
                GameObject instBlock = Instantiate(tile, new Vector3(x, 0, y), Quaternion.identity);    // Instantiate cube prefabs in world to represent nodes in grid
                instBlock.GetComponent<TileInfo>().SetXY(x, y);             // store the x and y index of node in TileInfo script
            }
        }
    }

    // Used to get neighbours of a node, returned as a list of nodes
    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0 || Mathf.Abs(x) == 1 && Mathf.Abs(y) == 1)  // to exclude the diagonal nodes, and the node itself from neighbours list
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;
                // For cases when the node is on a corner edge of the grid, we check whether the neighbour node is in the grid bounds
                if (checkX >= 0 && checkX < 10 && checkY >= 0 && checkY < 10)   
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }
        return neighbours;
    }


    // takes in a vector3, and returns the node which contains this position
    public Node NodeFromCoordinates(Vector3 Coordinates)
    {
        int nodeX = Mathf.FloorToInt(Coordinates.x + 0.5f);
        int nodeY = Mathf.FloorToInt(Coordinates.z + 0.5f);
        return grid[nodeX, nodeY];

    }

}
