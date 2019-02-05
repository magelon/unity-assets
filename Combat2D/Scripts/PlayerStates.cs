using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script for player states
public class PlayerStates:MonoBehaviour
{
    Animator an;
    [HideInInspector]
    public bool jump = false;
    [HideInInspector]
    public bool attack = false;
    [HideInInspector]
    public bool run = false;

    private void Start()
    {
        an = GetComponent<Animator>();
    }

    private void Update()
    {
        if (jump)
        {
            an.SetBool("jump", true);
        }else if (attack) {
            an.SetTrigger("attack1");
        }else if (run)
        {
            an.SetBool("run", true);
        }
        else {
            an.SetBool("jump", false);
            an.SetBool("run", false);
            attack = false;
        }

    }

}
