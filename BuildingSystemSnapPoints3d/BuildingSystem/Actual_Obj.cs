using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//attach on actual build obj
public class Actual_Obj : MonoBehaviour
{
    public void TurnOnSnaps() {
        foreach(Transform child in transform)
        {
            if (child.tag == "SnapPoint")
            {
                child.GetComponent<SnapPoint>().TurnOnSnaps();
            }
        }
    }
}
