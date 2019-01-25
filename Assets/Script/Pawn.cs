using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour {

	public GameObject prefab;
	private int count = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			GameObject cube = Instantiate(prefab, transform.position, transform.rotation) as GameObject;
			cube.name = "Pion" + count++;
			// Destroy(cube, 3f);
		}
	}
}
