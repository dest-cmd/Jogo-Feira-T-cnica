using UnityEngine;

public class RaioPlayer : MonoBehaviour
{
	public GameObject lightningPrefab;
	public float Tempo = 2f;
	private float atual;

	public float detectionRadius = 6f;
	public string enemyTag = "Enemy";

	private bool desbloqueado = false; // começa bloqueado

	void Update()
	{
		if (!desbloqueado) return; // se não desbloqueou ainda, sai

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

	// Chame esse método ao apertar o botão
	public void DesbloquearRaio()
	{
		Tempo -= 1f;
		desbloqueado = true;
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = new Color(1f, 1f, 0f, 0.3f);
		Gizmos.DrawSphere(transform.position, detectionRadius);
	}
}
