using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Hover : MonoBehaviour
{
    public Camera cam;
    public GameObject obj;
    public GameObject pref;
    public Transform parent;
    
    private void Update()
    {
        var mousePos = Input.mousePosition;
        mousePos.z = 10; // select distance = 10 units from the camera
        Debug.Log(cam.ScreenToWorldPoint(mousePos));
        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
        obj.transform.position = new Vector3(Mathf.Round(worldPos.x - 0.5f) +0.5f , Mathf.Round(worldPos.y -0.5f) +0.5f , Mathf.Round(worldPos.z));



        if (Input.GetMouseButtonDown(0))
        {
            GameObject temp = Instantiate(pref);
            temp.transform.position = obj.transform.position;
            temp.transform.SetParent(parent);
            temp.GetComponent<BoxCollider2D>().usedByComposite = true;
            
        }
    }
}
