using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBackGround : MonoBehaviour
{
    //down 1.8 -1.5 -11
    //1.8 -.25 -11
    public float speed=.3f;
    float BetweenTime=1f;
    float currentTime;
    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time < currentTime + BetweenTime&&currentTime!=0) {
            transform.Translate((new Vector3(0, -1, 0)) * speed * Time.deltaTime);
            if (transform.position.y < -0.695)
            {
                transform.position = startPos;
            }
        }
       
    }

    public void moveBackGround() {
        currentTime = Time.time; 
    }
}
