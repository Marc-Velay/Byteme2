using UnityEngine;
using System.Collections;
using System.Collections.Generic;

    public class MouseSelect : MonoBehaviour
{

    public Transform selectedTarget;
    public GameObject selectionCirclePrefab;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        { // when button clicked... 
            RaycastHit hit;
            // cast a ray from mouse pointer: 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // if enemy hit... 
            if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Enemy")){
                DeselectTarget(); // deselect previous target (if any)... 
                selectedTarget = hit.transform; // set the new one... 
                SelectTarget(); // and select it 
            }
        }
    }

    private void SelectTarget()
    {

        foreach (var selectableObject in FindObjectsOfType<SelectableUnitComponent>())
        {
            print("test1");
            if (selectableObject.gameObject == selectedTarget.gameObject)
            {
                print("test2");
                if (selectableObject.selectionCircle == null)
                {
                    selectableObject.selectionCircle = Instantiate(selectionCirclePrefab);
                    selectableObject.selectionCircle.transform.SetParent(selectableObject.transform, false);
                    selectableObject.selectionCircle.transform.eulerAngles = new Vector3(90, 0, 0);
                    print("selected" + selectedTarget.name);
                }
            }
            /*else
            {
                if (selectableObject.selectionCircle != null)
                {
                    Destroy(selectableObject.selectionCircle.gameObject);
                    selectableObject.selectionCircle = null;
                }
            }*/
        }
        
    }

    private void DeselectTarget() {
        if(selectedTarget)
        {
            foreach (var selectableObject in FindObjectsOfType<SelectableUnitComponent>())
            {
                if(selectedTarget != null && selectableObject != null)
                {
                    if (selectableObject.gameObject == selectedTarget.gameObject)
                    {
                        // if any guy selected, deselect it 

                        print("unselected" + selectedTarget.name);
                        if(selectableObject.selectionCircle.gameObject != null)
                        {
                            Destroy(selectableObject.selectionCircle.gameObject);
                        }                        
                        selectableObject.selectionCircle = null;
                        selectedTarget = null;
                    }
                }             
            }
        }
        
    }
}