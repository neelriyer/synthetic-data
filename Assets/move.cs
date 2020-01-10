using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    void Update()
 {
     int speed = 10;
     if(Input.GetKey(KeyCode.RightArrow))
     {
         transform.Translate(new Vector3(speed * Time.deltaTime,0,0));
     }
     if(Input.GetKey(KeyCode.LeftArrow))
     {
         transform.Translate(new Vector3(-speed * Time.deltaTime,0,0));
     }
     if(Input.GetKey(KeyCode.DownArrow))
     {
         transform.Translate(new Vector3(0,-speed * Time.deltaTime,0));
     }
     if(Input.GetKey(KeyCode.UpArrow))
     {
         transform.Translate(new Vector3(0,speed * Time.deltaTime,0));
     }

      if (Input.GetKey(KeyCode.A)){
	  	transform.Rotate(0,0,5f);
	  }
	  if (Input.GetKey(KeyCode.D)){
	  	transform.Rotate(0,0,-5f);
	  }
	  if (Input.GetKey(KeyCode.W)){
	  	transform.Rotate(5f,0,0);
	  }
	  if (Input.GetKey(KeyCode.S)){
	  	transform.Rotate(-5f,0,0);
	  }
 }
}
