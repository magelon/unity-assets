using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enime : MonoBehaviour
{
    public int health=10;
    public float speed;
    public float dazedTime;
    public float startDazed;

    private void Update()
    {
        if (dazedTime <= 0)
        {
            speed = 1;
        }
        else
        {
            speed = 0;
            dazedTime -= Time.deltaTime;
        }
        if (health <= 0)
        {
            //recycle enime
        }
    }

    public void damage(int dam) {
        dazedTime = startDazed;
        //blood split
        health -= dam;
    }
}
