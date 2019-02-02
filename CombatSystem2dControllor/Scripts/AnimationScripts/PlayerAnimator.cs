using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator an;
    private float moveInput;
    // Start is called before the first frame update
    void Start()
    {
        an = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
        if (moveInput != 0) { 
        an.SetBool("run", true);
        }
        else
        {
            an.SetBool("run", false);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            an.SetTrigger("attack1");
        }
    }
}
