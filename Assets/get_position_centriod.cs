using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Camera cam1;

public class get_position_centriod : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{

		
		
	}

	// Update is called once per frame
	void Update()
	{
		/*
		bool visible = GetComponent<Renderer>().isVisible;
		Debug.Log(visible);
		if(visible){
			Debug.Log(string.Format("{0}", GameObject.Find("lower2").transform.position));
		}
		*/

		//Vector3 relative;
		//relative = transform.InverseTransformDirection(Vector3.forward);
		//Debug.Log(relative);

		//Camera maincam = GameObject.Find("Main Camera").GetComponent<Camera>();

		//float start_x =  GameObject.Find("start_corner").transform.position.z;
		//float start_y = GameObject.Find("start_corner").transform.position.x;

		//float target_x = GameObject.Find("upper0").transform.position.z;
		//float target_y = GameObject.Find("upper0").transform.position.x;

		//float adjusted_target_x = 

		//Debug.Log(string.Format("start = {0}, {1}", start_x, start_y));
		//Debug.Log(string.Format("upper0 = {0}, {1}", target_x, target_y));
		//Debug.Log(string.Format("{0}, {1} is the position of object", maincam.transform.position.x-transform.localPosition.x, transform.localPosition.z));


		/*
		RaycastHit hit;
		// Calculate Ray direction
		Vector3 direction = Camera.main.transform.position - transform.position; 

		if(Physics.Raycast(transform.position, direction, out hit))
		{
			//Debug.Log(string.Format("{0}", GameObject.Find("lower2").transform.position));
			if(hit.collider.tag == "MainCamera") //hit the camera
			{
				Debug.Log(string.Format("{0}", GameObject.Find("lower2").transform.position));
			}
		}
		*/
		
		/*
		bool isVisibleForCamera1 = cam1.IsObjectVisible(GetComponent<MeshRenderer>());
		//Debug.Log(isVisibleForCamera1);
		if(isVisibleForCamera1){
			Debug.Log(string.Format("{0}", GameObject.Find("lower2").transform.position));
		}
		*/
		
	}

}


