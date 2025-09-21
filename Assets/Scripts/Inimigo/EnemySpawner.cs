using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public GameObject enemyPrefab;    // arraste o prefab no Inspector
	public float spawnRate = 2f;      // segundos entre spawns
	public float spawnRadius = 8f;    // raio ao redor do spawner
	public int maxEnemies = 10;       // número máximo de inimigos ativos
	public bool spawnOnStart = true;

	void Start()
	{
		if (spawnOnStart)
			StartCoroutine(SpawnLoop());
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
		Vector2 pos = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
		GameObject e = Instantiate(enemyPrefab, pos, Quaternion.identity);
		e.name = enemyPrefab.name;
		Debug.Log("[EnemySpawner] Spawned " + e.name + " at " + pos);
	}

	[ContextMenu("SpawnNow")]
	void SpawnNowContext()   // ✅ versão compatível com C# 4.0
	{
		SpawnEnemy();
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, spawnRadius);
	}
}
