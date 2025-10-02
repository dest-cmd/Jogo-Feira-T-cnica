using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[Header("Configuração de Spawn")]
	public GameObject enemyPrefab;
	public float spawnRate = 2f;
	public float spawnRadius = 8f;
	public int maxEnemies = 10;
	public bool spawnOnStart = true;

	[Header("Zona proibida")]
	public Transform player;                 // Referência ao Player
	public float noSpawnRadius = 3f;         // Raio de exclusão (zona amarela)

	void Start()
	{
		if (spawnOnStart)
			StartCoroutine(SpawnLoop());

		// Caso o player não tenha sido atribuído manualmente
		if (player == null)
		{
			GameObject p = GameObject.FindGameObjectWithTag("Player");
			if (p != null) player = p.transform;
		}
	}

	IEnumerator SpawnLoop()
	{
		if (enemyPrefab == null)
		{
			Debug.LogError("[EnemySpawner] enemyPrefab NÃO atribuído em: " + gameObject.name);
			yield break;
		}

		while (true)
		{
			int current = GameObject.FindGameObjectsWithTag("Enemy").Length;
			if (current < maxEnemies)
			{
				SpawnEnemy();
			}
			yield return new WaitForSeconds(spawnRate);
		}
	}

	public void SpawnEnemy()
	{
		Vector2 pos;
		int attempts = 0;

		do
		{
			pos = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
			attempts++;
		}
		while (IsInsideNoSpawnZone(pos) && attempts < 30); // Evita spawn na área proibida

		if (attempts < 30)
		{
			GameObject e = Instantiate(enemyPrefab, pos, Quaternion.identity);
			e.name = enemyPrefab.name;
			Debug.Log("[EnemySpawner] Spawned " + e.name + " at " + pos);
		}
	}

	bool IsInsideNoSpawnZone(Vector2 pos)
	{
		if (player == null) return false;
		return Vector2.Distance(pos, player.position) < noSpawnRadius;
	}

	void OnDrawGizmosSelected()
	{
		// Área de spawn (vermelho)
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, spawnRadius);

		// Zona proibida (amarelo)
		if (player != null)
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(player.position, noSpawnRadius);
		}
	}
}