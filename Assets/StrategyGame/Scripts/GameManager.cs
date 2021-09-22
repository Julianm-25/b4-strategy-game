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
    private Vector3 currentMousePos;

    /// <summary>
    /// restart level and game
    /// </summary>
    public void startNewGame()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        initGame();
    }

    private void initGame()
    {
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
}