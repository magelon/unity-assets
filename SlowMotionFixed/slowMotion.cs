using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slowMotion : MonoBehaviour
{
    private float slow = 0.1f;
    private float normTime = 1.0f;
    private bool doslow = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Time.timeScale = normTime;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Time.timeScale = slow;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
    }
}
