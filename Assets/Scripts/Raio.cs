using UnityEngine;

public class Raio : MonoBehaviour
{
	public float delay = 0.5f;          // tempo antes do raio aparecer
	public float duration = 1f;         // quanto tempo o raio dura
	public int damage = 100;            // dano
	public string enemyTag = "Enemy";   // tag dos inimigos

	private GameObject target;          // inimigo escolhido
	private LineRenderer line;
	private float timer;
	private bool active = false;

	void Start()
	{
		// cria o LineRenderer para o efeito do raio
		line = gameObject.AddComponent<LineRenderer>();
		line.startWidth = 0.15f;
		line.endWidth = 0.05f;
		line.material = new Material(Shader.Find("Sprites/Default"));
		line.startColor = Color.yellow;
		line.endColor = Color.white;
		line.positionCount = 2;
		line.enabled = false;

		// chama a função Strike após o delay
		Invoke("Strike", delay);
	}

	void Strike()
	{
		// pega todos os inimigos pela tag
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

		if (enemies.Length == 0)
		{
			Destroy(gameObject);
			return;
		}

		// escolhe um inimigo aleatório
		int index = Random.Range(0, enemies.Length);
		target = enemies[index];

		if (target == null)
		{
			Destroy(gameObject);
			return;
		}

		// ativa o raio visual
		line.enabled = true;
		active = true;
		timer = duration;
	}

	void Update()
	{
		if (!active) return;

		if (target == null)
		{
			Destroy(gameObject);
			return; // inimigo morreu antes do raio terminar
		}

		// ponto inicial no "céu" acima do inimigo
		Vector3 start = target.transform.position + Vector3.up * 10f;
		// ponto final no inimigo
		Vector3 end = target.transform.position;

		line.SetPosition(0, start);
		line.SetPosition(1, end);

		// controla o tempo do efeito
		timer -= Time.deltaTime;
		if (timer <= 0f)
		{
			// aplica dano apenas no final
			Enemy enemyHealth = target.GetComponent<Enemy>();
			if (enemyHealth != null)
			{
				enemyHealth.TakeDamage(damage);
			}
			Destroy(gameObject); // destrói o raio
		}
	}
}