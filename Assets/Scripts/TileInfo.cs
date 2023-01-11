using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour
{
    public int x, y;

    public void SetXY(int _x, int _y)
    {
        x = _x;
        y = _y;
        return;
    }
}
