using UnityEngine;

public class tornadospawner : MonoBehaviour
{
	public GameObject tornadoPrefab;
	public float tornadoSpeed = 5f;
	public float cooldown = 4f; // tempo de espera em segundos

	private bool canSpawn = true;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q) && canSpawn)
		{
			SpawnTornado();
		}
	}

	void SpawnTornado()
	{
		// Pega posição do player
		Vector3 spawnPos = transform.position;

		// Pega posição do mouse no mundo
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePos.z = 0;

		// Instancia tornado
		GameObject tornado = Instantiate(tornadoPrefab, spawnPos, Quaternion.identity);

		// Define velocidade e direção
		tornado tornadoScript = tornado.GetComponent<tornado>();
		tornadoScript.speed = tornadoSpeed;
		Vector2 direction = (mousePos - spawnPos).normalized;
		tornadoScript.SetDirection(direction);

		// Inicia cooldown
		canSpawn = false;
		Invoke("ResetCooldown", cooldown);

	}

	void ResetCooldown()
	{
		canSpawn = true;
	}
}
