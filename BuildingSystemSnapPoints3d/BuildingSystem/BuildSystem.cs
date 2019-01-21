using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//attach on buildsystem
public class BuildSystem : MonoBehaviour
{
    public Camera cam;
    public LayerMask layer;
    private GameObject buildThing;
    public float stickTolerance = 1f;

    public bool isBuilding = false;
    private bool pauseBuilding = false;

    private void Update() {
        //rotate
        if (Input.GetKeyDown(KeyCode.R)) {
            buildThing.transform.Rotate(0, 90f, 0);
        }
        //stop
        if (Input.GetMouseButtonDown(0)&&buildThing!=null)
        {
            StopBuild();
        }
        //isbuilding
        if (isBuilding)
        {
            if (pauseBuilding)
            {
                float mouseX = Input.GetAxis("Mouse X");
                float mouseZ = Input.GetAxis("Mouse Y");

                if (Mathf.Abs(mouseX) >= stickTolerance || Mathf.Abs(mouseZ) >= stickTolerance)
                {
                    pauseBuilding = false;
                }
            }else
            DoBuildRay();
        }
    }
    public void NewBuild(GameObject _obj) {
        Vector3 pos = transform.position;
        GameObject go = Instantiate(_obj, pos, Quaternion.identity);
        buildThing = go;
        isBuilding = true;    
    }
    private void StopBuild() {
        buildThing.GetComponent<Preview_Obj>().Place();
        buildThing = null;
        isBuilding = false;

    }
    //used for snaping blocks
    public void PauseBuild(bool b) {
        pauseBuilding = b;
    }
    private void DoBuildRay() {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1500f, layer)) {
            buildThing.transform.position = hit.point ;
        }
    }
}
