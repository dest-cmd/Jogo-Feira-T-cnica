using UnityEngine;

public class BombaPlayer : MonoBehaviour
{
	public GameObject bombPrefab; // arraste o prefab da bomba no inspector
	public Transform spawnPoint;  // opcional: posição exata para spawn

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.G)) // tecla para soltar bomba
		{
			Vector3 pos = spawnPoint ? spawnPoint.position : transform.position;
			Instantiate(bombPrefab, pos, Quaternion.identity);
		}
	}
}
