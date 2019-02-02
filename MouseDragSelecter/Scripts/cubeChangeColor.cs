using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeChangeColor : MonoBehaviour
{
    Renderer rend;
    void Start()
    {
         rend = gameObject.GetComponent<Renderer>();
    }

    public void turnGreen() {
        rend.material.color = Color.green;
    }
    public void turnRed() {
        rend.material.color = Color.red;
    }

}
