using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().AddForce(Vector3.right * 50f, ForceMode.Impulse);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
