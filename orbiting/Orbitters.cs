using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//combain object pooling to find the prefect orbitter
public class Orbitters : MonoBehaviour
{
    public int sphereCount = 100;
    public int maxRadius = 100;
    public GameObject[] spheres;
    public Material[] mats;
    public Material trailMat;

    void Awake() {
        spheres = new GameObject[sphereCount];
    }
    // Start is called before the first frame update
    void Start()
    {
        spheres = CreateSpheres(sphereCount, maxRadius);
        
    }

    public GameObject[] CreateSpheres(int count,int radius)
    {
        var sphs = new GameObject[count];
        var sphereToCopy = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Rigidbody rb = sphereToCopy.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.angularDrag = 0;
        for(int i = 0; i < count; i++)
        {
            var sp = GameObject.Instantiate(sphereToCopy);
            sp.transform.position = this.transform.position +
                new Vector3(Random.Range(-maxRadius, maxRadius),
                Random.Range(-10, 10),
                Random.Range(-maxRadius, maxRadius));
            sp.transform.localScale *= Random.Range(0.5f, 1);
           // sp.GetComponent<Collider>().enabled = false;
            sp.GetComponent<Renderer>().material = mats[Random.Range(0, mats.Length)];
            TrailRenderer tr = sp.AddComponent<TrailRenderer>();
            tr.time = 1.0f;
            tr.startWidth = 0.1f;
            tr.endWidth = 0;
            tr.material = trailMat;
            tr.startColor = new Color(1, 1, 0, 0.1f);
            tr.endColor = new Color(0, 0, 0, 0);
            
            spheres[i] = sp;
        }
        GameObject.Destroy(sphereToCopy);
        return spheres;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach(GameObject s in spheres)
        {
            Vector3 difference = this.transform.position - s.transform.position;
            float dist = difference.magnitude;
            Vector3 gravityDirection = difference.normalized;
            float gravity = 6.7f * (this.transform.localScale.x * s.transform.localScale.x * 80)
                / (dist * dist);
            Vector3 gravityVector = (gravityDirection * gravity);
            s.transform.GetComponent<Rigidbody>().AddForce(s.transform.forward, ForceMode.Acceleration);
            s.transform.GetComponent<Rigidbody>().AddForce(gravityVector, ForceMode.Acceleration);
        }
    }
}
