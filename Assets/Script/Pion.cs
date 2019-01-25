using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pion : MonoBehaviour {

	Vector3 point;
	public Color colorSelected = Color.green;

	public float CurrentX{set;get;}
	public float CurrentZ{set;get;}
	public bool player1;

	public Vector3 GetPosition () {
		// print(point);
		return point;
	}

	public void SetPosition(float x, float z){
		CurrentX = x;
		CurrentZ = z;
	}

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
