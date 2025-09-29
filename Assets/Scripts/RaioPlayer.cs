using UnityEngine;

public class RaioPlayer : MonoBehaviour
{
	public GameObject lightningPrefab;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.H)) // tecla para chamar o raio
		{
			Instantiate(lightningPrefab, Vector3.zero, Quaternion.identity);
		}
	}
}
