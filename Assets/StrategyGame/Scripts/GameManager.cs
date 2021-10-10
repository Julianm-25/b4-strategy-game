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

    /// <summary>
    /// to make the button choice easier to read and code
    /// </summary>
    public enum NextAction
    {
        Select,
        Move,
        Attack,
    }
    
    [SerializeField] private Camera mainCamera;
    public Vector3 currentMousePos;

    //team id starts from 0
    public int activeTeamId;
    public NextAction nextAction; // "Move" or "Attack"
    public GameObject mapGrid;
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
            //Debug.Log($"getapthis turn team {teamID}, unit team {unit.teamID} unit {unit.name} ap {unit.actionPoints}");
        }
        //Debug.Log($"team {teamID} moves {moves}");
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
    /// called at the end of every action to forcefully end turn if there is no more actions left to do
    /// </summary>
    public void checkEndTurn()
    {
        if (getApThisTurn() == 0)
        {
            endTurn();
        }
    }

    /// <summary>
    /// end turn and give control to the next team
    /// </summary>
    public void endTurn()
    {
        // go from 0 to 1 and loop back to 0 using modulus
        activeTeamId = (activeTeamId + 1) % 2;
        UIManager.Instance.setTurnText(activeTeamId + 1);
        UIManager.Instance.toggleCommandMenu(false);
        resetAPMP();
        AudioManager.Instance.playEndTurn(true);

    }

    /// <summary>
    /// reset the AP and MP for all characters after end of turn
    /// </summary>
    public void resetAPMP()
    {
        foreach (var team in teams)
        {
            foreach (var character in team)
            {
                character.actionPoints = character.maxAP;
                character.currentMovepoints = character.maxMovepoints;
            }
        }
    }
    
    /// <summary>
    /// restart level and game
    /// </summary>
    public void startNewGame()
    {
        activeTeamId = 0;
        UIManager.Instance.setTurnText(activeTeamId + 1);
    }

    // Start is called before the first frame update
    void Start()
    {
        initGame();
    }

    public void initGame()
    {
        // Debug.Log("init game called();");
        //create the teams list
        teams = new List<List<Character>>(2);
        //add the two team 0 and 1
        teams.Add(new List<Character>(3));
        teams.Add(new List<Character>(3));
        var grids = mapGrid.GetComponentsInChildren<Tile>();
        //find characters in the map
        foreach (var tile in grids)
        {
            //Debug.Log($"tile {{tile.name}} isoccupied {tile.isOccupied}");
            if (!tile.isOccupied) continue; //skip if not occupied
            //Debug.Log($"tile {tile.name} occupant {tile.occupant.name}");
            var character = tile.occupant.GetComponent<Character>(); //try to get the character
            if (character == null) continue; //skip non character objects
            teams[character.teamID].Add(character); //add the character
            //Debug.Log($"tile {tile.name} character {character.name} team {character.teamID} AP {character.actionPoints} grids {grids.Length}");
        }
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
            //Debug.Log($"mouse down at {Input.mousePosition}");
        }

        //button is released
        if (Input.GetMouseButtonUp(0))
        {
            //Debug.Log($"mouse up at {Input.mousePosition}");
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
        //Debug.Log($"isAlly({tileOccupant.name}), occupant {tileOccupant.GetComponent<Character>().teamID}, active {activeTeamId}");
        return tileOccupant.GetComponent<Character>().teamID == GameManager.Instance.activeTeamId;
    }

    /// <summary>
    /// check if all in enemy team are dead
    /// </summary>
    public void checkGameOver()
    {
        var alive = false;
        // go from 0 to 1 and loop back to 0 using modulus
        var enemyTeamID = (activeTeamId + 1) % 2;
        var grids = mapGrid.GetComponentsInChildren<Tile>();
        //find characters in the map
        foreach (var tile in grids)
        {
            //Debug.Log($"tile {{tile.name}} isoccupied {tile.isOccupied}");
            if (!tile.isOccupied) continue; //skip if not occupied
            //Debug.Log($"tile {tile.name} occupant {tile.occupant.name}");
            var character = tile.occupant.GetComponent<Character>(); //try to get the character
            if (character == null) continue; //skip non character objects
            if (character.teamID == enemyTeamID)
            {
                alive = true; //at least one enemy char alive
                break; //exit the loop
            }
            // Debug.Log($"tile {tile.name} character {character.name} team {character.teamID} grids {grids.Length}");
        }

        if (!alive)
        {
            UIManager.Instance.showGameOver(activeTeamId); 
        }
    }
}