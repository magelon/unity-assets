using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public float speed = 2f;
    public float jumpForce = 100f;
    float translation;
    float straffe;
    int maxVelocity = 20;

    public static Transform blockBeneath;
    public static bool grounded = false;
    bool jumpCoolDown = false;

    Rigidbody RB;
    private void Start()
    {
        RB = GetComponent<Rigidbody>();
        Physics.IgnoreLayerCollision(1,6);
    }

    void FixedUpdate () {

        translation = Input.GetAxis("Vertical") * speed;
        straffe = Input.GetAxis("Horizontal") * speed;

        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;

        transform.Translate(straffe, 0, translation);
        
        if (grounded && Input.GetButtonDown("Jump") && !jumpCoolDown)
        {
            RB.AddForce(transform.up * jumpForce, ForceMode.VelocityChange);
            StartCoroutine(JumpCool());
        }
    }
    void OnTriggerStay(Collider other)
    {
        blockBeneath = other.transform;
        grounded = true;
        if (other.tag == "Respawn")
        {
            transform.position = new Vector3(0, 30, 0);
        }
    }

    void OnTriggerExit(Collider other)
    {
        grounded = false;
    }
    IEnumerator JumpCool()
    {
        jumpCoolDown = true;
        yield return new WaitForSecondsRealtime(0.2f);
        jumpCoolDown = false;
    }
}
