using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImitateRag : MonoBehaviour
{
 
    public Transform objetivo;
    public bool invertido;


    //imitating the cube swaying animation to perform a walking animation
    void Update()
    {

       transform.eulerAngles= objetivo.localEulerAngles;
       if (invertido)
        {
            transform.eulerAngles = -objetivo.localEulerAngles;
        }

    }
}
