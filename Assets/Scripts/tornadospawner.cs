using UnityEngine;

public class tornadospawner : MonoBehaviour
{
	public GameObject tornadoPrefab;
	public Transform spawnPoint;   // de onde o tornado vai nascer
	public float cooldown = 4f;    // tempo de recarga
	public float detectionRadius = 6f; // raio de detecção dos inimigos

	private bool canSpawn = true;

	void Update()
	{
		GameObject inimigo = EncontrarInimigoMaisProximo();

		// Se tem inimigo no raio e pode spawnar
		if (inimigo != null && canSpawn)
		{
			SpawnTornado();
		}
	}

	void SpawnTornado()
	{
		Instantiate(tornadoPrefab, spawnPoint.position, spawnPoint.rotation);
		canSpawn = false;
		Invoke("ResetCooldown", cooldown);

	}

	void ResetCooldown()
	{
		canSpawn = true;
	}

	GameObject EncontrarInimigoMaisProximo()
	{
		GameObject[] inimigos = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject maisProximo = null;
		float menorDistancia = detectionRadius; // só considera dentro do raio
		Vector3 posicaoAtual = transform.position;

		foreach (GameObject inimigo in inimigos)
		{
			float distancia = Vector3.Distance(posicaoAtual, inimigo.transform.position);
			if (distancia < menorDistancia)
			{
				menorDistancia = distancia;
				maisProximo = inimigo;
			}
		}

		return maisProximo;
	}

	void OnDrawGizmosSelected()
	{
		// Gizmo para visualizar o raio de detecção
		Gizmos.color = new Color(0f, 0.7f, 1f, 0.3f);
		Gizmos.DrawSphere(transform.position, detectionRadius);
	}
}
