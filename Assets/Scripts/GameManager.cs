using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public GameState state;
    public enum GameState
    {
        PlayerTurn,
        EnemyTurn,
    }

    public TMP_Text tileInfoText;
    public GameObject gridCursorHover;
    public GameObject pathOverlay;
    public ObstacleScriptableObject obstacleData;

    public Vector3 targetLocation = new Vector3(5, 0, 5);
    public GameObject player;
    public GameObject enemy;
    Ray ray;
    public bool moving = false;
    public Grid grid;
    public Pathfinding pathfinding;
  
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;
        switch (newState)
        {
            case GameState.PlayerTurn:
                break;
            case GameState.EnemyTurn:
                EnemyTurnFunction();
                break;
            default:
                break;
        }
    }

    private void EnemyTurnFunction()
    {
       pathfinding.FindPath(enemy.transform.position, player.transform.position);
        StartCoroutine(enemy.GetComponent<IAI>().Move(grid.path));
    }

    private void Awake()
    {
        grid = GetComponent<Grid>();
        pathfinding = GetComponent<Pathfinding>();
        _instance = this;
    }
    private void Start()
    {
        UpdateGameState(GameState.PlayerTurn);
    }

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {
            ShowTileInfo(hit);
        }
            
        DrawCursorOnHover(ray);

        if (Input.GetMouseButtonDown(0) && state==GameState.PlayerTurn)
        {
            Vector2 mousePos = Input.mousePosition;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Tile" || hit.transform.tag == "UI")
                {
                    targetLocation = hit.transform.position;
                    pathfinding.FindPath(player.transform.position, targetLocation);
                        if (grid.path != null && !moving)
                         {
                              DrawPath();
                               StartCoroutine(player.GetComponent<Player>().PlayerMove(grid.path));
                         }
                }
            }
        }
    }


    void ShowTileInfo(RaycastHit hit)
    {
        if (hit.transform.tag == "Tile")
        {
            tileInfoText.text = hit.transform.GetComponent<TileInfo>().x + " " + hit.transform.GetComponent<TileInfo>().y;
        }
        else if (hit.transform.tag == "UI")
        {
            return;
        }
        else
            tileInfoText.text = " ";
    }

    void DrawCursorOnHover(Ray ray)
    {
        RaycastHit hit;
        if (state == GameState.PlayerTurn && moving==false)
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "UI")
                {
                    return;
                }
                if (hit.transform.tag == "Tile")
                {
                    gridCursorHover.transform.GetComponent<MeshRenderer>().enabled = true;
                    Vector3 cursorPos = new Vector3(hit.transform.GetComponent<TileInfo>().x, 0.51f, hit.transform.GetComponent<TileInfo>().y);
                    gridCursorHover.transform.position = cursorPos;
                }
            }
            else
                gridCursorHover.transform.GetComponent<MeshRenderer>().enabled = false;
        }
        else
            gridCursorHover.transform.GetComponent<MeshRenderer>().enabled = false;
    }
    

    public void DrawPath()
    {
        if (grid.path != null)
        {
            ClearPathOnGrid();

            foreach (Node n in grid.path)
            {
                GameObject pathTile = Instantiate(pathOverlay, n.position + Vector3.up * 0.51f, Quaternion.identity);
                pathTile.transform.parent = this.transform;
            }
        }
   
    }

    public void ClearPathOnGrid()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

}

