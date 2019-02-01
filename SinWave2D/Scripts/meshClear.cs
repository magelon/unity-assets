// Convert any GameObject into a single triangle

using UnityEngine;

public class meshClear : MonoBehaviour
{
    private Vector3[] baseHeight;
    public float speed = 1;
    public float scale = 0.3f;

    /*
    private bool once = false;
    Mesh mesh;
    int count = 0;

    private void Start()
    {
        mesh = gameObject.GetComponent<MeshFilter>().mesh;
    }

    void Update()
    {
        if (Time.time > 2.0f)
        {
            convertMesh();
        }
        //for each vertices, vertice.y = sin(vertice.x + counter) and then increment counter.﻿
        Vector3[] vertices = transform.GetComponent<MeshFilter>().mesh.vertices;
        for(int i = 0; i < vertices.Length; i++)
        {
            vertices[i].y = Mathf.Sin(vertices[i].x+count );
            count++;
        }
    }

    void convertMesh()
    {
        if (once)
            return;
        // Clears all the data that the mesh currently has
        mesh.Clear();

        // create 3 vertices for the triangle
        mesh.vertices = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0) };
        //mesh.uv = new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1) };
        mesh.triangles = new int[] { 0, 1, 2 };

        once = true;
    }
    */
    private float width=1;
    private float height=3;
    public int waveLenth = 1;
    private int uvWidth = 1;

    void Start()
    {
        MeshFilter mf= transform.GetComponent< MeshFilter>();
        mf.mesh.Clear();
        var mesh = new Mesh();
        mf.mesh = mesh;

        Vector3[] vertices = new Vector3[4+waveLenth*2];


        //first squard
        vertices[0] = new Vector3(0, 0, 0);
        vertices[1] = new Vector3(width, 0, 0);
        vertices[2] = new Vector3(0, height, 0);
        vertices[3] = new Vector3(width, height, 0);

        for(int i = 4; i < vertices.Length; i++)
        {
            //width++;
            if (i % 2 == 0) {
                width++;
                vertices[i] = new Vector3(width, 0, 0);
               
            }
            else
            {
                vertices[i] = new Vector3(width, height, 0);

            }

        }

       //vertices[4] = new Vector3(width + 1, 0, 0);
        //vertices[5] = new Vector3(width + 1, height, 0);

        mesh.vertices = vertices;

        int[] tri= new int[6+waveLenth*6];

        tri[0] = 0;
        tri[1] = 2;
        tri[2] = 1;

        tri[3] = 2;
        tri[4] = 3;
        tri[5] = 1;
        //+1
        tri[6] = 1;
        tri[7] = 3;
        tri[8] = 4;

        tri[9] = 3;
        tri[10] = 5;
        tri[11] = 4;
        //+2
        tri[12] = 4;
        tri[13] = 5;
        tri[14] = 6;

        tri[15] = 5;
        tri[16] = 7;
        tri[17] = 6;

        if (waveLenth > 2)
        {
            for(int i = 1; i < waveLenth - 2; i++)
            {
                tri[12 + 6 * i] = 4 + 2 * i;
                tri[13 + 6 * i] = 5 + 2 * i;
                tri[14 + 6 * i] = 6 + 2 * i;

                tri[15 + 6 * i] = 5 + 2 * i;
                tri[16 + 6 * i] = 7 + 2 * i;
                tri[17 + 6 * i] = 6 + 2 * i;
            }


        }
        mesh.triangles = tri;

       Vector3[] normals= new Vector3[4 + waveLenth * 2];

        for(int i = 0; i < normals.Length; i++)
        {
            normals[i] = -Vector3.forward;
        }
        /*
        normals[0] = -Vector3.forward;
        normals[1] = -Vector3.forward;
        normals[2] = -Vector3.forward;
        normals[3] = -Vector3.forward;
        */
        mesh.normals = normals;

        Vector2[] uv= new Vector2[4+waveLenth * 2];

        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(uvWidth, 0);
        uv[2] = new Vector2(0, 1);
        uv[3] = new Vector2(uvWidth, 1);

        for (int i = 4; i < uv.Length; i++)
        {
            //width++;
            if (i % 2 == 0)
            {
                uvWidth++;
                uv[i] = new Vector2(uvWidth, 0);

            }
            else
            {
                uv[i] = new Vector3(uvWidth, 1);

            }

        }


        mesh.uv = uv;
        if (baseHeight == null)
            baseHeight = mesh.vertices;
    }

    void Update()
    {
       
        //for each vertices, vertice.y = sin(vertice.x + counter) and then increment counter.﻿
        Vector3[] vertices = transform.GetComponent<MeshFilter>().mesh.vertices;
        Mesh mesh = GetComponent<MeshFilter>().mesh;

        for (int i = 2; i < vertices.Length; i++)
        {
            if (i == 2 || i%2!=0)
            {
                Vector3 vertex = baseHeight[i];
                vertex.y += Mathf.Sin(Time.time * speed + baseHeight[i].x) * scale;
                //vertex.y += Mathf.PerlinNoise(baseHeight[i].x + noiseWalk, baseHeight[i].y + Mathf.Sin(Time.time * 0.1f)) * noiseStrength;
                vertices[i] = vertex;
            }
           
        }
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }


}