using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (UIManager.Instance.isMenu()) return;
        //button is held down and not at the same spot
        if (Input.GetMouseButton(1) && (GameManager.Instance.currentMousePos != Input.mousePosition))
        {
            GameManager.Instance.currentMousePos = Input.mousePosition;
            Debug.Log($"cameramove mouse down at {Input.mousePosition}");
        }

        //button is released
        if (Input.GetMouseButtonUp(1))
        {
            Debug.Log($"cameramove mouse up at {Input.mousePosition}");
        }
    }
}
