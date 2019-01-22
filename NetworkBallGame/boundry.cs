using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boundry : MonoBehaviour {
	
	// Update is called once per frame
	void Update ()
    {
        if (transform.position.x > 12)
        {
            transform.position = new Vector2(-12, transform.position.y);
        }
        if (transform.position.x < -12)
        {
            transform.position = new Vector2(12, transform.position.y);
        }
        if (transform.position.y > 9) 
        {
            transform.position = new Vector2(transform.position.x,-9);
        }
        if (transform.position.y < -9)
        {
            transform.position = new Vector2(transform.position.x, 9);
        }
    }
}
