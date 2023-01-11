using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public ObstacleScriptableObject obstacleData;
    public GameObject obstacle;                     //Prefab to be instantiated as obstacle

    private void Start()
    {
        UpdateObstacle();
    }


    // Checks the Obstacle Scriptable Object and places the obstacle prefab accordingly
    public void UpdateObstacle()
    {
        int i, j;
        for (i = 0; i < 10; i++)
        {
            for (j = 0; j < 10; j++)
            {
                if (obstacleData.IsObstacle(i, j))
                {
                    Instantiate(obstacle, new Vector3(i, 1, j), Quaternion.identity);
                }    
            }
        }
    }
}
