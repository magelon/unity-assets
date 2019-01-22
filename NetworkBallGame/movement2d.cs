using System.Collections;
using System.Collections.Generic;

using UnityEngine.Networking;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class movement2d : NetworkBehaviour{


    public float speed=2;
   

    // Update is called once per frame
    void Update () {

        if (!isLocalPlayer)
        {
            return;
        }

        transform.Translate(Input.acceleration.x, Input.acceleration.y, 0);

        float transactionV = Input.GetAxis("Vertical") * speed;
        float transactionH = Input.GetAxis("Horizontal") * speed;
        transactionV *= Time.deltaTime;
        transactionH *= Time.deltaTime;
        transform.Translate(transactionH, transactionV, 0);

        //float transactionVC = CrossPlatformInputManager.GetAxis("Vertical") * speed;
        //float transactionHC = CrossPlatformInputManager.GetAxis("Horizontal") * speed;
        //transactionVC *= Time.deltaTime;
        //transactionHC *= Time.deltaTime;
        //transform.Translate(transactionHC, transactionVC, 0);

    }

    public override void OnStartLocalPlayer()
    {
        GetComponent<SpriteRenderer>().material.color = Color.red;
    }
}
