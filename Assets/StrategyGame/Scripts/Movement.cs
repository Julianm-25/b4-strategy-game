using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
            //skip tiles with no tiles script
            if (!adjTile.GetComponent<Tile>()) continue;
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
        foreach (var tileCollider in Physics.OverlapSphere(transform.position, transform.GetComponent<Character>().attackRange - 0.4f)) // the 0.4f is to avoid issues with diagonals
        {
            if (tileCollider.CompareTag("GridSquare"))
            {
                // If the tile is occupied by an enemy
                var tile = tileCollider.GetComponent<Tile>();
                //skip tiles with no tiles script
                if (!tile) continue;
                
                if (tile.isOccupied && !GameManager.Instance.isAlly(tile.occupant))
                {
                    // Activate the associated red tile
                    tileCollider.GetComponent<Tile>().redTile.SetActive(true);
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
        var tile = currentTile.GetComponent<Tile>();
        if (GameManager.Instance.nextAction == GameManager.NextAction.Select
            && tile.isOccupied
            && gameObject.CompareTag("Player")
            && GameManager.Instance.isAlly(tile.occupant)
            && gameObject.GetComponent<Character>().currentMovepoints > 0)
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
