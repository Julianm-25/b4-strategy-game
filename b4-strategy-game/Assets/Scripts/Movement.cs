using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private List<GameObject> adjTiles = new List<GameObject>();
    public GameObject greenTile;
    private bool isOccupied;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var tile in Physics.OverlapSphere(transform.position, 0.5f))
        {
            if (tile.gameObject.CompareTag("GridSquare") && tile.gameObject.transform != transform)
            {
                adjTiles.Add(tile.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ActivateAdjacentTiles()
    {
        // For each adjacent tile
        foreach (var adjTile in adjTiles)
        {
            // If the tile is unoccupied
            if (!adjTile.GetComponent<Movement>().isOccupied)
            {
                // Activate the associated green tile
                adjTile.GetComponent<Movement>().greenTile.SetActive(true);
            }
        }
    }

    private void ClearGreenTiles()
    {
        foreach (var obj in GameObject.FindGameObjectsWithTag("GreenSquare"))
        {
            obj.gameObject.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        ClearGreenTiles();
        ActivateAdjacentTiles();
    }
}
