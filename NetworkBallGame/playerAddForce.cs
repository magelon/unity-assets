using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAddForce : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D collision)
    {

        var hit = collision.gameObject;
        Transform t = transform;

        var health = hit.GetComponent<ballForward>();
        if (health != null)
        {
            Debug.Log(t.position.y);
            health.m(t);
        }
    }
}
