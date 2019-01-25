using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour 
{

	public float deltaRotation = 100f;
	public float deltaMovement = 100f;
	int x;
	int z;

	int[,] array2D = new int[10, 10] { { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 },
		{ 4, 3, 2, 1, 0, 9, 8, 7, 6, 9 },
		{ 5, 6, 5, 4, 3, 2, 1, 0, 5, 8 },
		{ 6, 7, 6, 5, 4, 3, 2, 9, 4, 7 },
		{ 7, 8, 7, 4, 3, 2, 1, 8, 3, 6 },
		{ 8, 9, 8, 5, 0, 1, 0, 7, 2, 5 },
		{ 9, 0, 9, 6, 7, 8, 9, 6, 1, 4 },
		{ 0, 1, 0, 1, 2, 3, 4, 5, 0, 3 },
		{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 2 },
		{ 2, 3, 4, 5, 6, 7, 8, 9, 0, 1 }
	};

	public Color color;

	// Use this for initialization
	void Start () 
	{
		// GetComponent<Renderer>().material.color = color;
		getPosition();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Rotate();
		Movements();
	}

	void Rotate ()
	{
		if (Input.GetKey(KeyCode.A))
			transform.Rotate(new Vector3(0f, -deltaRotation, 0f) * Time.deltaTime);
		else if (Input.GetKey(KeyCode.E))
			transform.Rotate(new Vector3(0f, deltaRotation, 0f) * Time.deltaTime);
	}

	void Movements () 
	{
		if (Input.GetKeyDown(KeyCode.UpArrow)) 
		{
			if ((int)transform.position.z < 9) {
				transform.Translate(0, 0, 1);
				getPosition();
			}
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow)) {
			if ((int)transform.position.z > 0) {
				transform.Translate(0, 0, -1);
				getPosition();
			}
		}
		else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			if ((int)transform.position.x > 0) {
				transform.Translate(-1, 0, 0);
				getPosition();
			}
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow)) {
			if ((int)transform.position.x < 9) {
				transform.Translate(1, 0, 0);
				getPosition();
			}
		}
	}

	void getPosition () {
		x = (int)transform.position.x;
		z = (int)transform.position.z;
		Debug.Log("z=" + z.ToString() + " x=" + x.ToString() + " chiffre=" + array2D[z, x] );
	}
}
