using UnityEngine;

public class BombaPlayer : MonoBehaviour
{
	public GameObject bombPrefab;   // arraste o prefab da bomba no inspector
	public Transform spawnPoint;    // opcional: posição exata para spawn
	public float Tempo;             // intervalo de spawn
	public float raioDeteccao = 8f; // raio para checar inimigos

	private float atual;

	void Update()
	{
		if (atual <= 0)
		{
			// procura inimigos por tag
			GameObject[] inimigos = GameObject.FindGameObjectsWithTag("Enemy");
			bool temInimigoPerto = false;

			foreach (GameObject inimigo in inimigos)
			{
				if (Vector2.Distance(transform.position, inimigo.transform.position) <= raioDeteccao)
				{
					temInimigoPerto = true;
					break;
				}
			}

			if (temInimigoPerto)
			{
				Vector3 pos = spawnPoint ? spawnPoint.position : transform.position;
				Instantiate(bombPrefab, pos, Quaternion.identity);
				atual = Tempo; // reseta cooldown
			}
		}
		else
		{
			atual -= Time.deltaTime;
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, raioDeteccao);
	}
}
