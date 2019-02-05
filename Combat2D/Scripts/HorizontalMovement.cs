using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement : MonoBehaviour
{
    public float speed=1f;
    PlayerStates ps;

    void Start()
    {
        ps = GetComponent<PlayerStates>();
    }

   
    void FixedUpdate()
    {
        float translation = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        transform.Translate(translation, 0, 0);
        if (translation != 0)
        {
            ps.run = true;
        }
        else
        {
            ps.run = false;
        }

    }
}
