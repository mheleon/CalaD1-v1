using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {

	// Fetch pieces in x and z, array of all pions
	public Pion[,] Pions{ set; get;}

	// Wich on is selected, pion selected
	private Pion selectedPion;
	private bool pionSelectedBool;
	private int[] pionSelectedPosition;

	private const float TILE_SIZE = 1f;
	private const float TILE_OFFSET = 0.5f;

	// Current selection, -1 -> not ones is selected
	private float selectionX = -1f;
	private float selectionZ = -1f;

	public List<GameObject> pionPrefab;
	private List<GameObject> activePions = new List<GameObject>();
	public List<GameObject> arrowPrefab; 
	private List<GameObject> activeArrow = new List<GameObject>();

	readonly int[,] arrayChiffres = new int[10, 10] { { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 },
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

	// Use this for initialization
	void Start () {
		pionSelectedBool = false;
		Pions = new Pion[10, 10];
		for (float i = 1f; i <= 4f; i++)
			SpawnPions(0, 9f, i);
		for (float i = 1f; i <= 4f; i++)
			SpawnPions(1, 0f, i);
	}
		
	// Update is called once per frame
	void Update () {
		DrawBoard();
		// UpdateSelection();
		UpdateSelectionMouse();
		
	}

	void DrawBoard () {
		Vector3 widthLine = Vector3.right * 10;
		Vector3 heightLine = Vector3.forward * 10;

		for (int i = 0; i <= 10; i++) {
			Vector3 start = Vector3.forward * i;
			Debug.DrawLine (start, start + widthLine);
			for (int j = 0; j <= 10; j++) {
				start = Vector3.right * j;
				Debug.DrawLine (start, start + heightLine);
			}
		}

		// Draw selection
		if(selectionX >= 0 && selectionZ >= 0) {
			Debug.DrawLine(
				Vector3.forward * selectionZ + Vector3.right * selectionX,
				Vector3.forward * (selectionZ + 1) + Vector3.right * (selectionX + 1));
			Debug.DrawLine(
				Vector3.forward * (selectionZ + 1) + Vector3.right * selectionX,
				Vector3.forward * selectionZ + Vector3.right * (selectionX + 1));
		}
	}

	// Detect selection
	void UpdateSelection () {
		// is there a camera that we can use?
		if (!Camera.main)
			return;

		RaycastHit hit;
		if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("PlaneLayer"))) {
			// Where the colition happend
			selectionX = (int)hit.point.x;
			selectionZ = (int)hit.point.z;
		} else {
			selectionX = -1;
			selectionZ = -1;
		}
	}

	// Generate Pions
	private void SpawnPions (int index, float x, float z) {
		GameObject go = Instantiate(pionPrefab [index], GetTileCenter(x, z), Quaternion.identity) as GameObject;
		go.transform.SetParent (transform);
		// Add pion to ensemble de pions
		Pions[(int)x, (int)z] = go.GetComponent<Pion>();
		activePions.Add(go);
	}

	// Generate Arrows
	private void SpawnArrows (int rotate, float x, float z) {
		GameObject go = Instantiate(arrowPrefab [0], GetTileCenter(x, z), Quaternion.identity) as GameObject;
		go.transform.SetParent (transform);
		go.transform.Rotate(0, rotate, 0);
		activeArrow.Add(go);
	}

	private void SelectPion () {

	}

	private Vector3 GetTileCenter(float x, float z) {
		Vector3 origin = Vector3.zero;
		origin.x += (TILE_SIZE * x) + TILE_OFFSET;
		origin.y = 0.5f;
		origin.z += (TILE_SIZE * z) + TILE_OFFSET;
		return origin;
	}

	private void UpdateSelectionMouse () {
		if (Input.GetMouseButtonDown(0) && !pionSelectedBool) {
			Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			int x = (int)position.x;
			int z = (int)position.z;
			Debug.Log("Mouse position: x=" + x + "z=" + z);
			if(Pions[x, z] != null) {
				pionSelectedBool = true;
				selectedPion = Pions[(int)x, (int)z];
				// Pions[x, z].SelectPion(); // no necesario
				int ch = arrayChiffres[z, x];
				print("pion trouvé, chiffre = " + ch);

				// 	Draw arrows
				if(z < 9)
					SpawnArrows(0, x + 0.2f, z + 1f - 0.3f);
				if(z > 0)
					SpawnArrows(-180, x -0.2f, z - 1 + 0.3f);
				if(x < 9)
					SpawnArrows(90, x + 1 - 0.3f, z - 0.2f);
				if(x > 0)
					SpawnArrows(-90, x - 1 + 0.3f, z +0.2f);
			} else {
				print("pas de pion à la position x=" + x + " z=" + z);
			}
		} else if (Input.GetMouseButtonDown(0) && pionSelectedBool) {
			print("Pion à bouger");
			Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			// Arrow position selected
			int x = (int)position.x;
			int z = (int)position.z;
			if (x > 9 || x < 0 || z > 9 || z < 0)
				return;
			// Position pion selected
			int ppX = (int)selectedPion.GetPosition().x;
			int ppZ = (int)selectedPion.GetPosition().z;
			int ch = arrayChiffres[ppZ, ppX];
			if (ch == 0) {
				ClearPionSelected();
				return;
			}
			if (x == ppX && z == (ppZ + 1)) {
				print("UP=" + ch);
				if ((ppZ + ch) < 10) {
					selectedPion.transform.Translate(0, 0, ch);
					Pions[ppX, ppZ + ch] = Pions[ppX, ppZ];
				} else {
					selectedPion.transform.Translate(0, 0, 9 - ppZ);
					Pions[ppX, 9] = Pions[ppX, ppZ];
				}
				// Update Pion Position in plane
				// Pions[ppX, ppZ + ch] = Pions[ppX, ppZ];
				Pions[ppX, ppZ] = null;
				ClearPionSelected();
			} else if (x == ppX && z == (ppZ - 1)) {
				print("DOWN=" + ch);

				if ((ppZ - ch) > -1) {
					selectedPion.transform.Translate(0, 0, -ch);
					Pions[ppX, ppZ - ch] = Pions[ppX, ppZ];
				} else {
					selectedPion.transform.Translate(0, 0, -ppZ);
					Pions[ppX, 0] = Pions[ppX, ppZ];
				}

				// selectedPion.transform.Translate(0, 0, -ch);
				// Update Pion Position in plane
				// Pions[ppX, ppZ - ch] = Pions[ppX, ppZ];
				Pions[ppX, ppZ] = null;
				ClearPionSelected();
			} else if (x == (ppX + 1) && z == ppZ) {
				print("RIGHT=" + ch);

				if ((ppX + ch) < 9) {
					selectedPion.transform.Translate(ch, 0, 0);
					Pions[ppX + ch, ppZ] = Pions[ppX, ppZ];
				} else {
					selectedPion.transform.Translate(9 - ppX, 0, 0);
					Pions[9, ppZ] = Pions[ppX, ppZ];
				}

				// selectedPion.transform.Translate(ch, 0, 0);
				// Update Pion Position in plane
				// Pions[ppX + ch, ppZ] = Pions[ppX, ppZ];
				Pions[ppX, ppZ] = null;
				ClearPionSelected();
			} else if (x == (ppX - 1) && z == ppZ) {
				print("LEFT=" + ch);

				if ((ppX - ch) > -1) {
					selectedPion.transform.Translate(-ch, 0, 0);
					Pions[ppX - ch, ppZ] = Pions[ppX, ppZ];
				} else {
					selectedPion.transform.Translate(-ppX, 0, 0);
					Pions[0, ppZ] = Pions[ppX, ppZ];
				}

				// selectedPion.transform.Translate(-ch, 0, 0);
				// Update Pion Position in plane
				// Pions[ppX - ch, ppZ] = Pions[ppX, ppZ];
				Pions[ppX, ppZ] = null;
				ClearPionSelected();
			}
		}
	}

	void ClearPionSelected () {
		foreach (GameObject arrow in activeArrow) {
			Destroy(arrow);
		}
		activeArrow.Clear();
		pionSelectedBool = false;
		selectedPion = null;
	}
	
}
