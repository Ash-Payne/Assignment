using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int i;
    public float speed = 10f;

    // Coroutine takes a list of Nodes and moves the Player unit
    // through each node in succession till the path is reached
    public IEnumerator PlayerMove(List<Node> path)
    {
       GameManager.Instance.moving = true;
        for (i = 0; i < path.Count; i++)
        {
            while (transform.position != path[i].position + Vector3.up)
            {
                transform.position = Vector3.MoveTowards(transform.position, path[i].position + Vector3.up, speed* Time.deltaTime);
                yield return new WaitForSeconds(0.01f);
            }
        }
        GameManager.Instance.ClearPathOnGrid();
        yield return new WaitForSeconds(0.5f);          // Wait for 0.5 seconds before the enemy moves
        GameManager.Instance.moving = false;
        GameManager.Instance.UpdateGameState(GameManager.GameState.EnemyTurn);
        yield return null;
    }

}



