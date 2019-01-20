using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllorSimple : MonoBehaviour {

    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    //animator from the cube which your leg will be imitating
    public Animator a;
    //public GameObject cube;
    bool walking = false;
    // Use this for initialization
    void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetAxis("Vertical")!=0 && walking!=true) {
            walking = true;
            a.SetBool("walking", true);
            
        }
        if(Input.GetAxis("Vertical")==0 && walking!=false)
        {
            walking = false;
            a.SetBool("walking", false);
            
        }
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        // Move translation along the object's y-axis
        transform.Translate(0, 0, translation);

        // Rotate around our y-axis
        transform.Rotate(-rotation, 0, 0);
    }
}
