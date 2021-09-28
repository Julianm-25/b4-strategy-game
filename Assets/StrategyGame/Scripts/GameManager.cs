using System;
using System.Collections;
using System.Collections.Generic;
using TeddyToolKit.Core;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// The Manager of the game, in charge of starting, stopping and score keeping
/// </summary>
public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private Camera mainCamera;
    public Vector3 currentMousePos;

    //team id starts from 0
    public int activeTeamId;
    public string nextAction = "Move"; //or "Attack"
    //list of list of charachers, usage: teams[activeTeam][character]
    private List<List<Character>> teams;
    
    /// <summary>
    /// get the total AP for this turn for the a team
    /// </summary>
    /// <returns></returns>
    public int getApThisTurn(int teamID)
    {
        int moves = 0;
        foreach (var unit in teams[teamID])
        {
            moves += unit.actionPoints;
        }

        return moves;
    }
    /// <summary>
    /// get the total AP for this turn for the activeteam
    /// </summary>
    /// <returns></returns>
    public int getApThisTurn()
    {
        return getApThisTurn(activeTeamId);
    }

    /// <summary>
    /// end turn and give control to the next team
    /// </summary>
    public void endTurn()
    {
        // go from 0 to 1 and loop back to 0 using modulus
        activeTeamId = (activeTeamId + 1) % 2;
    }
    
    /// <summary>
    /// restart level and game
    /// </summary>
    public void startNewGame()
    {
        activeTeamId = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        initGame();
    }

    private void initGame()
    {
        Debug.Log("init game called();");
        throw new NotImplementedException();
    }

    /// <summary>
    /// get the mouse clicked object
    /// </summary>
    /// <returns>GameObject</returns>
    public GameObject getObjectOnMousePosition()
    {
        //3D
        RaycastHit rayHit = new RaycastHit();
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        bool isHit = Physics.Raycast(ray, out rayHit);
        Debug.DrawRay(ray.origin, ray.direction * 50, Color.magenta, 10f);
        // Debug.Log($"mouse position: {Input.mousePosition} is 3d hit: {isHit}");
        if (isHit)
        {
            return rayHit.collider.gameObject;
        }

        //2D
        RaycastHit2D rayHit2D = Physics2D.GetRayIntersection(ray);
        //if (rayHit2D != null) //don't compare to null because it is never null, resharper hint.
        if (rayHit2D) //if there is no value then this will not be true, don't use null
        {
            // Debug.Log($"mouse position: {Input.mousePosition} is 2d hit: {rayHit2D.collider.gameObject.name}");
            return rayHit2D.collider.gameObject;
        }

        return null;
    }

    // Update is called once per frame
    void Update()
    {
        if (UIManager.Instance.isMenu()) return;
        //button is held down and not at the same spot
        if (Input.GetMouseButton(0) && (currentMousePos != Input.mousePosition))
        {
            currentMousePos = Input.mousePosition;
            Debug.Log($"mouse down at {Input.mousePosition}");
        }

        //button is released
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log($"mouse up at {Input.mousePosition}");
        }
    }
    
    /// <summary>
    /// Is this occupant of the tile Ally or not?
    /// Not Ally means Enemy
    /// </summary>
    /// <param name="tileOccupant"></param>
    /// <returns>true for ally, false for enemy</returns>
    public bool isAlly(GameObject tileOccupant)
    {
        Debug.Log($"isAlly({tileOccupant.name}), occupant {tileOccupant.GetComponent<Character>().teamID}, active {activeTeamId}");
        return tileOccupant.GetComponent<Character>().teamID == GameManager.Instance.activeTeamId;
    }
}