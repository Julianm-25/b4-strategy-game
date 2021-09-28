using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    private GameObject selectedUnit;
    public TMP_Text teamText;
    public TMP_Text unitText;
    public TMP_Text hpText;
    public TMP_Text actionPointText;
    public TMP_Text movementPointText;
    public GameObject UICanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If the left mouse button is clicked and no menu is displayed
        if (Input.GetMouseButtonDown(0) && !UIManager.Instance.isMenu())
        {
            // This raycast goes towards the mouse
            Ray clickCheck;

            clickCheck = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;

            if (Physics.Raycast(clickCheck, out hitInfo))
            {
                Debug.Log(hitInfo.collider.tag);
                // If clicked on Character Unit
                if (hitInfo.collider.CompareTag("AllyUnit"))
                {
                    // If the raycast hits an ally unit, set selectedUnit to the target
                    selectedUnit = hitInfo.collider.gameObject;
                    UICanvas.SetActive(true);
                    UpdateText();
                }
                // If the raycast hits a grid square and selectedUnit has been set
                else if (hitInfo.collider.CompareTag("GridSquare") && selectedUnit != null)
                {
                    // Set unitMovement to the Movement script attached to selectedUnit
                    Movement unitMovement = selectedUnit.GetComponent<Movement>();
                    // Set unitStats to the Character script attached to selectedUnit
                    Character unitStats = selectedUnit.GetComponent<Character>();
                    
                    // If the grid square's green tile is active
                    if (hitInfo.collider.GetComponent<Tile>().greenTile.activeInHierarchy && unitStats.currentMovepoints > 0)
                    {
                        // Un-occupy the current tile
                        unitMovement.currentTile.GetComponent<Tile>().isOccupied = false;
                        unitMovement.currentTile.GetComponent<Tile>().occupant = null;
                        // Move the selectedUnit to the new tile and set it as occupied
                        selectedUnit.transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.transform.position.y + 0.3f, hitInfo.transform.position.z);
                        hitInfo.collider.GetComponent<Tile>().isOccupied = true;
                        hitInfo.collider.GetComponent<Tile>().occupant = selectedUnit;
                        unitMovement.currentTile = hitInfo.collider.gameObject;
                        // Reset the process of showing the adjacent tiles
                        unitMovement.ClearTiles();
                        unitMovement.GetAdjacentTiles();
                        // Stops the green tiles from activating if you go from 1 to 0 movement points
                        if (unitStats.currentMovepoints > 1)
                        {
                            unitMovement.ActivateAdjacentTiles();
                        }

                        if (unitStats.actionPoints > 0)
                        {
                            unitMovement.ActivateEnemyTiles();
                        }
                        
                        // Reduces movement points by 1
                        unitStats.currentMovepoints -= 1;
                        // Updates the UI Text
                        UpdateText();

                    }
                    // If an inactive tile is selected
                    else
                    {
                        // Reset everything
                        unitMovement.ClearTiles();
                        selectedUnit = null;
                        UICanvas.SetActive(false);
                    }
                }
                // If the raycast hits an enemy and you have more than 0 AP
                else if (hitInfo.collider.CompareTag("EnemyUnit") && selectedUnit != null && selectedUnit.GetComponent<Character>().actionPoints > 0)
                {
                    // If the raycast hits an enemy within the selected unit's attack range
                    if(Vector3.Distance(hitInfo.collider.transform.position, selectedUnit.transform.position) <= selectedUnit.GetComponent<Character>().attackRange)
                    {
                        // Set enemy as the occupant of the selected tile
                        GameObject enemy = hitInfo.collider.gameObject; 
                        // Reduce the enemy's health by the current unit's attack
                        enemy.GetComponent<Character>().currentHealth -= selectedUnit.GetComponent<Character>().attack; 
                        // If the enemy's health is 0 or lower
                        if (enemy.GetComponent<Character>().currentHealth <= 0) 
                        { 
                            // Destroy the enemy
                            enemy.GetComponent<Movement>().Death(); 
                        } 
                        selectedUnit.GetComponent<Character>().actionPoints -= 1;
                        UpdateText();
                    }
                }
            }
        }
    }
    // Allows the player to spend more AP to move
    // Connected to a UI button
    public void IncreaseMovePoints()
    {
        // If the last AP is used up
        // Get rid of the red tiles
        if (selectedUnit.GetComponent<Character>().actionPoints == 1)
        {
            // Clear the tiles
            selectedUnit.GetComponent<Movement>().ClearTiles();
            // Activate the adjacent green tiles
            selectedUnit.GetComponent<Movement>().ActivateAdjacentTiles();
        }
        // If the unit has AP
        if (selectedUnit.GetComponent<Character>().actionPoints > 0)
        {
            // Take away 1 AP and add some movepoints
            selectedUnit.GetComponent<Character>().actionPoints -= 1;
            selectedUnit.GetComponent<Character>().currentMovepoints += selectedUnit.GetComponent<Character>().maxMovepoints;
            UpdateText();
        }
    }

    public void UpdateText()
    {
        teamText.text = selectedUnit.GetComponent<Character>().teamID.ToString();
        unitText.text = selectedUnit.GetComponent<Character>().GetType().Name;
        hpText.text = selectedUnit.GetComponent<Character>().currentHealth.ToString();
        actionPointText.text = selectedUnit.GetComponent<Character>().actionPoints.ToString();
        movementPointText.text = selectedUnit.GetComponent<Character>().currentMovepoints.ToString();
    }
}
