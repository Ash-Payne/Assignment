using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ObstacleScriptableObject", order = 1)]
public class ObstacleScriptableObject : ScriptableObject
{
    // Stores obstacle data in 1D array of bools of size 100
    bool[] values = new bool[100];

    // Function to modify scriptable object value at an index
    public void SetObstacleAt(int i, int j, bool value)
    {
        int index = (10 * i) + j;
        values[index] = value;
    }

    // Function that returns true if an obstacle is at given X,Y index
    public bool IsObstacle(int i, int j)
    {
        int index = (10 * i) + j;
        if (values[index])
            return true;
        else
            return false;
    }
}

// Data for grid[0,0] is stored at values[0]
// Data for grid[0,1] is stored at values[1]
// Data for grid[0,2] is stored at values[2]
//.
//.
//.
// Data for grid[1,0] is stored at values[10]
// Data for grid[1,1] is stored at values[11]
//.
//.
// Data for grid[i,j] is stored at values[10i+j]
