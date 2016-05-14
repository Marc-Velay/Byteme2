using UnityEngine;
using System.Collections;
using System.Collections.Generic;

    public class MouseSelect : MonoBehaviour
{

    public Transform selectedTarget;

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

    private void SelectTarget() {
        selectedTarget.GetComponent<Renderer>().material.color = Color.red;
        Debug.Log("selected", selectedTarget);
        //PlayerAttack pa = (PlayerAttack)GetComponent("PlayerAttack");
        //pa.target = selectedTarget.gameObject;
    }

    private void DeselectTarget() { if (selectedTarget) {
            // if any guy selected, deselect it 
            selectedTarget.GetComponent<Renderer>().material.color = Color.blue;
            selectedTarget = null;
        }
    }
}