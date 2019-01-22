using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class ballForward : NetworkBehaviour
{

    public void m(Transform t)
    {
        //gameObject.GetComponent<Rigidbody2D>().AddForce(-transform.up);

        //Debug.Log(t.position.y + "," + gameObject.GetComponent<Transform>().position.y);
        if (t.position.y > transform.position.y||transform.position.y>=(float)9)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(-transform.up*20);
        }
        if(t.position.y < transform.position.y || transform.position.y<= (float)-9)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up*20);
        }
        if (t.position.x > transform.position.x || transform.position.x >= (float)12)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(-transform.right*20);
        }
        if (t.position.x < transform.position.x || transform.position.x <= -(float)9)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right*20);
        }


    }

    
}
