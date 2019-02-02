using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faceing : MonoBehaviour
{
    private bool faceing = true;
    private float moveInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");
        if (faceing == false && moveInput > 0)
        {
            Flip();
        }else if (faceing == true && moveInput < 0)
        {
            Flip();
        }
    }

    void Flip() {
        faceing = !faceing;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
