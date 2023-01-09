using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRayCast : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnClicked()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Clicked on " + hit.transform.name);
        }
        else
        {
            Debug.Log("Nothing hit");
        }
    }
}
