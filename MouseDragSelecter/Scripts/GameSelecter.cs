using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSelecter : MonoBehaviour
{
    RaycastHit hit;
    List<Transform> selectedUnits = new List<Transform>();
    bool isDragging=false;
    Vector3 mousePosition;

    private void OnGUI()
    {
        if (isDragging) {
            var rect = ScreenHelper.GetScreenRect(mousePosition, Input.mousePosition);
            ScreenHelper.DrawScreenRect(rect, new Color(0.8f,0.8f,0.8f,0.1f));
            ScreenHelper.DrawScreenRectBorder(rect, 1, Color.white);
        }
       
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePosition = Input.mousePosition;
            var camRay = Camera.main.ScreenPointToRay
                (Input.mousePosition);
            if(Physics.Raycast(camRay,out hit))
            {
                if (hit.transform.CompareTag("unit"))
                {

                    SelectUnit(hit.transform, Input.GetKey(KeyCode.LeftShift));
                   
                }
                else
                {
                    isDragging = true;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isDragging == false) {
                return;
            }
            DeselectUnits();
                                                //this is the way for example 
                                                //in real game development loop from units pool
            foreach (var selectableObject in FindObjectsOfType<BoxCollider>())
            {
                
                if (IsWithinSelectionBounds(selectableObject.transform))
                {
                    SelectUnit(selectableObject.transform, true);
                }
            }

          
            isDragging = false;
        }
        
    }

    void SelectUnit(Transform unit,bool isMultiSelect = false)
    {
        //Debug.Log(isMultiSelect);
        if (!isMultiSelect)
        {
           DeselectUnits();
           //unit.GetComponent<cubeChangeColor>().turnGreen();
        }
        selectedUnits.Add(unit);
        //highlight
        unit.GetComponent<cubeChangeColor>().turnGreen();
        Debug.Log("Unit"+ unit.name + "been selected");
    }

    private void DeselectUnits()
    {
        for(int i = 0; i < selectedUnits.Count; i++)
        {
            //high light
            //Debug.Log(selectedUnits[i].transform);
            selectedUnits[i].GetComponent<cubeChangeColor>().turnRed();
        }
        selectedUnits.Clear();
    }

    private bool IsWithinSelectionBounds(Transform transform)
    {
        if (!isDragging)
        {
            return false;
        }
        var camera = Camera.main;
        var viewportBounds = ScreenHelper.
            GetViewportBounds(camera, mousePosition, Input.mousePosition);
        //position of unit is in game space need to cast to camera view pooint
        return viewportBounds.
            Contains(camera.WorldToViewportPoint(transform.position));
    }
}
