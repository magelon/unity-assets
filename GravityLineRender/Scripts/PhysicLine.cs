using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicLine : MonoBehaviour
{
    
    public Vector2[] points;
    public int len=2;
    public float force = 100;

    

    public Color c1 = Color.yellow;
    public Color c2 = Color.red;

    // Start is called before the first frame update
    void Start()
    {
        
        points = new Vector2[len];
        
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.2f;
        lineRenderer.positionCount = len;

        // A simple 2 color gradient with a fixed alpha of 1.0f.
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        lineRenderer.colorGradient = gradient;


    }

    // Update is called once per frame
    void Update()
    {
        LineRenderer line = GetComponent<LineRenderer>();
        float t = Time.time;
       // for (int j = 0; j < len; j++) {
        //    Vector3 po = new Vector3(j*0.5f,Mathf.Sin(j+t),0);
        //    line.SetPosition(j,po);
       // }

        for(int i = 0; i < len; i++)
        {
            Vector3 po = new Vector3(points[i].x, points[i].y, 0);
            line.SetPosition(i, po);
        }
        linePhysics();

    }


    //line objects
    void linePhysics() {
        //make points attracted to one another
        for (int j = 1; j < len - 1; j++)
        {
            Vector2 offsetToPrev = (points[j - 1] - points[j]);
            Vector2 offsetToNext = points[j + 1] - points[j];
            Vector2 velocity = offsetToPrev * force + offsetToNext * force;
            points[j] += velocity * Time.deltaTime / len;

        }
        //apply gravity
        for (int j = 1; j < len - 1; j++)
        {
            points[j] += Vector2.down * 9.8f * Time.deltaTime / len;

        }
 
    }

    
}
