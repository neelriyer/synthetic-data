using UnityEngine;
using System.Collections;

public class exampleclass : MonoBehaviour
{

    Camera cam;

    void Start()
    {
        
    }

    void Update()
    {

    	cam = GetComponent<Camera>();

        //upper0
        Vector3 screenPos_upper0 = cam.WorldToScreenPoint(GameObject.Find("upper0").transform.position);
        Debug.Log("target is " + screenPos_upper0.x + " pixels from the left");
        Debug.Log("target is " + screenPos_upper0.y + " pixels from the idk");

        //upper1
        Vector3 screenPos_upper1 = cam.WorldToScreenPoint(GameObject.Find("upper1").transform.position);
        Debug.Log("target is " + screenPos_upper1.x + " pixels from the left");
        Debug.Log("target is " + screenPos_upper1.y + " pixels from the idk");

        //upper2
        Vector3 screenPos_upper2 = cam.WorldToScreenPoint(GameObject.Find("upper2").transform.position);
        Debug.Log("target is " + screenPos_upper2.x + " pixels from the left");
        Debug.Log("target is " + screenPos_upper2.y + " pixels from the idk");



         string serializedData = 
     "upper0, " + screenPos_upper0.x + "," + screenPos_upper0.y + "\n" +
     "upper1, " + screenPos_upper1.x + "," + screenPos_upper1.y + "\n" +
     "upper2, " + screenPos_upper2.x + "," + screenPos_upper2.y + "\n";

        System.IO.File.WriteAllText("Assets/filename.txt", serializedData);

    }
}