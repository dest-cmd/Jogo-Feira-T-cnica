using UnityEngine;

public class RaioPlayer : MonoBehaviour
{
	public GameObject lightningPrefab;
	public float Tempo = 2f;
	private float atual;

	public float detectionRadius = 6f;
	public string enemyTag = "Enemy";

	void Update()
	{
		if (atual <= 0)
		{
			// só instancia se houver inimigo perto
			if (ExisteInimigoPerto())
			{
				Instantiate(lightningPrefab, transform.position, Quaternion.identity);
				atual = Tempo;
			}
		}
		else
		{
			atual -= Time.deltaTime;
		}
	}

	bool ExisteInimigoPerto()
	{
		GameObject[] inimigos = GameObject.FindGameObjectsWithTag(enemyTag);
		foreach (GameObject inimigo in inimigos)
		{
			if (Vector3.Distance(transform.position, inimigo.transform.position) <= detectionRadius)
			{
				return true;
			}
		}
		return false;
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = new Color(1f, 1f, 0f, 0.3f);
		Gizmos.DrawSphere(transform.position, detectionRadius);
	}
}
