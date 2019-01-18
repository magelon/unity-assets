using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrian : MonoBehaviour
{
    int heightScale = 5;
    float detailScale = 5.0f;
    List<GameObject> myTrees = new List<GameObject>();
    //public GameObject tree;
    // Start is called before the first frame update
    //initialization
    void Start()
    {
        Mesh mesh = this.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        for (int v = 0; v < vertices.Length; v++)
        {
            vertices[v].y = Mathf.PerlinNoise(
            (vertices[v].x + this.transform.position.x) / detailScale,
            (vertices[v].z + this.transform.position.z) / detailScale) * heightScale;

            if (vertices[v].y > 2.6&&Mathf.PerlinNoise((vertices[v].x+5)/10,
                (vertices[v].z+5)/10)*10>4.6)
            {
                GameObject newTree = TreePool.getTree();
                if (newTree != null)
                {
                    Vector3 treePos = new Vector3(
               vertices[v].x + this.transform.position.x,
               vertices[v].y,
               vertices[v].z + this.transform.position.z);
                    newTree.transform.position = treePos;
                    newTree.SetActive(true);
                    myTrees.Add(newTree);
                }
            }
           
        }
        mesh.vertices = vertices;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        this.gameObject.AddComponent<MeshCollider>();

    }

    private void OnDestroy()
    {
        for(int i = 0; i < myTrees.Count; i++)
        {
            if (myTrees[i] != null)
                myTrees[i].SetActive(false);
        }
        myTrees.Clear();
    }
}
