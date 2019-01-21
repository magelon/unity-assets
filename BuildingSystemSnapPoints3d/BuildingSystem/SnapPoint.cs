using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//snappoint is on actual build block
public class SnapPoint : MonoBehaviour
{
    SphereCollider myCollider;
    BuildSystem buildSystem;

    private bool canSnap = false;
    public string TagImLookingFor;
    // Start is called before the first frame update
    void Start()
    {
        buildSystem = GameObject.FindObjectOfType<BuildSystem>();
        myCollider = GetComponent<SphereCollider>();
    }

    public void TurnOnSnaps() {
        canSnap = true;
    }
   
    void OnTriggerEnter(Collider other)
    {
        if (canSnap)
        {
            if (other.tag == TagImLookingFor)
            {
                other.transform.position = transform.position;
                other.transform.rotation = transform.rotation;
                canSnap = false;
                buildSystem.PauseBuild(true);
            }
        }
    }
    void OnTriggerExit(Collider other) {
        canSnap = true;
    }
}
