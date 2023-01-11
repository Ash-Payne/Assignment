using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IAI
{
    public float speed = 10f;
    int i;

    // Coroutine takes a list of Nodes and moves the enemy unit(The gameobject on which this script is attached) 
    // through each node in succession till the path is reached
    public IEnumerator Move(List<Node> path)
    {
        for (i = 0; i < path.Count-1; i++)                           // count-1 because the final node in this path is the player node,
        {                                                            // the enemy unit should stop at the second last node in list
            while (transform.position !=  path[i].position + Vector3.up)        //Vector3.up to move the target position up by 1 unit
            {                                                                   // Grid lies at height 0, the unit must move above it, at height 1
                transform.position = Vector3.MoveTowards(transform.position, path[i].position + Vector3.up, speed*Time.deltaTime);
                yield return new WaitForSeconds(0.01f);
            }
        }
        GameManager.Instance.ClearPathOnGrid();
        GameManager.Instance.UpdateGameState(GameManager.GameState.PlayerTurn);     // Updates game state to indicate Player turn
        yield return null;
    }
}
