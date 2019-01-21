using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//attach on preview obj
public class Preview_Obj : MonoBehaviour
{
    public GameObject realThing;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<Rigidbody>();
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Place() {
        GameObject go = Instantiate(realThing, transform.position, transform.rotation);
        go.GetComponent<Actual_Obj>().TurnOnSnaps();
        Destroy(gameObject);
    }
}
