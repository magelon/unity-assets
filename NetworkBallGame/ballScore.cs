using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballScore : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject hit = collision.gameObject;
        teamScore s = hit.GetComponent<teamScore>();
        if (s != null)
        {
            s.goal();
            Destroy(gameObject);
        }


    }

}
