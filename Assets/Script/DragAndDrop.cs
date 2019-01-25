using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour {

	Vector3 point;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDrag () {
		point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		point.x = (int)point.x + 0.5f;
		point.y = transform.position.y;
		point.z = (int)point.z + 0.5f;
		// print (point);
		transform.position = point;
	}
}
