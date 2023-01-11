using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    #region FIELDS
    public Vector3 position;
    public int gCost=0;
    public int hCost=0;
    public int gridX;           // The x location of node in the grid
    public int gridY;           // The y location of node in the grid
    public Node parent;
    #endregion

    #region CONSTRUCTORS
    public Node(Vector3 _position, int _gridX, int _gridY)
    {
        position = _position;
        gridX = _gridX;
        gridY = _gridY;
    }
    #endregion


    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

}
