using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//attach on buildManager
public class BuildManager : MonoBehaviour
{
    public BuildSystem buildSystem;
    public GameObject previewFundation;

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetKeyDown(KeyCode.B))
        {
            buildSystem.NewBuild(previewFundation);
        }
    }
}
