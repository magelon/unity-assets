using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//set camera to orthographic
public class GridMovement : MonoBehaviour
{
    float moveSpeed = 1;
    Vector3 mousePosition;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = new Vector2(Mathf.Round(mousePosition.x)
            ,Mathf.Round(mousePosition.y));
    }
}
