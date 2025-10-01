using UnityEngine;

public class tornadospawner : MonoBehaviour
{
	public GameObject tornadoPrefab;
	public float tornadoSpeed = 5f;
	public float cooldown = 4f; 
	public float spawnOffset = 1f; // distância à frente do Player

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
		// Posição do mouse no mundo
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePos.z = 0;

		// Calcula direção
		Vector2 direction = (mousePos - transform.position).normalized;

		// Posição de spawn com offset
		Vector3 spawnPos = transform.position + (Vector3)(direction * spawnOffset);

		// Instancia tornado sem pai
		GameObject tornado = Instantiate(tornadoPrefab, spawnPos, Quaternion.identity);

		// Configura velocidade e direção
		tornado tornadoScript = tornado.GetComponent<tornado>();
		tornadoScript.speed = tornadoSpeed;
		tornadoScript.SetDirection(direction);

		// Cooldown
		canSpawn = false;
		Invoke("ResetCooldown", cooldown);
	}

	void ResetCooldown()
	{
		canSpawn = true;
	}
}
