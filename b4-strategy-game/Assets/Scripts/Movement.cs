using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // List of tiles next to current tile
    [SerializeField] private List<GameObject> adjTiles = new List<GameObject>();
    // The current tile
    public GameObject currentTile;
    // Start is called before the first frame update
    void Start()
    {
        GetAdjacentTiles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateAdjacentTiles()
    {
        // For each adjacent tile
        foreach (var adjTile in adjTiles)
        {
            // If the tile is unoccupied
            if (!adjTile.GetComponent<Tile>().isOccupied)
            {
                // Activate the associated green tile
                adjTile.GetComponent<Tile>().greenTile.SetActive(true);
            }
        }
    }
    public void ActivateEnemyTiles()
    {
        // For each tile in attack range
        foreach (var tile in Physics.OverlapSphere(transform.position, transform.GetComponent<UnitStats>().attackRange))
        {
            if (tile.CompareTag("GridSquare"))
            {
                // If the tile is occupied by an enemy
                if (tile.GetComponent<Tile>().isOccupied && tile.GetComponent<Tile>().occupant.CompareTag("EnemyUnit"))
                {
                    // Activate the associated red tile
                    tile.GetComponent<Tile>().redTile.SetActive(true);
                }  
            }
        }
    }

    public void ClearTiles()
    {
        // Gets every green tile on the field
        foreach (var obj in GameObject.FindGameObjectsWithTag("GreenSquare"))
        {
            // Deactivates the green tile
            obj.gameObject.SetActive(false);
        }
        // Gets every red tile on the field
        foreach (var obj in GameObject.FindGameObjectsWithTag("RedSquare"))
        {
            // Deactivates the red tile
            obj.gameObject.SetActive(false);
        }
    }

    public void GetAdjacentTiles()
    {
        // Clears the list of adjacent tiles
        adjTiles.Clear();
        // Checks each object in a 0.5 radius
        foreach (var tile in Physics.OverlapSphere(transform.position, 0.7f))
        {
            // If the object has the GridSquare tag and is not the current tile
            if (tile.gameObject.CompareTag("GridSquare"))
            {
                // Add the tile to the adjTiles list
                adjTiles.Add(tile.gameObject);
            }
        }
    }

    private void OnMouseDown()
    {
        // When the unit is clicked, if the tile is occupied, the occupant is an ally, and the occupant has move points, activate the tiles
        ClearTiles();
        if (currentTile.GetComponent<Tile>().isOccupied && gameObject.CompareTag("AllyUnit") && gameObject.GetComponent<UnitStats>().currentMovepoints > 0)
        {
            ActivateAdjacentTiles();
            ActivateEnemyTiles();
        }
    }
    public void Death()
    {
        // Disconnects the unit from the tile
        currentTile.GetComponent<Tile>().occupant = null;
        currentTile.GetComponent<Tile>().isOccupied = false;
        currentTile.GetComponent<Tile>().redTile.SetActive(false);
        // Gets rid of the unit
        Destroy(gameObject);
    }
}