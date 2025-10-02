using UnityEngine;

public class Raio : MonoBehaviour
{
	public float delay = 0.5f;
	public float duration = 0.5f;
	public string enemyTag = "Enemy";
	public int dano = 25;

	private GameObject target;
	private LineRenderer line;
	private float timer;
	private bool active = false;

	public Transform raio;
	public Animator animacao;
	private Transform inimigo;

	void Start()
	{
		line = gameObject.AddComponent<LineRenderer>();
		line.startWidth = 0.15f;
		line.endWidth = 0.05f;
		line.material = new Material(Shader.Find("Sprites/Default"));
		line.startColor = Color.yellow;
		line.endColor = Color.white;
		line.positionCount = 2;
		line.enabled = true;

		Invoke("Strike", delay);
	}

	void Strike()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		if (enemies.Length == 0)
		{
			Destroy(gameObject);
			return;
		}

		// 🔥 Encontra o inimigo mais próximo
		float menorDistancia = Mathf.Infinity;
		GameObject maisProximo = null;
		Vector3 origem = transform.position;

		foreach (GameObject enemy in enemies)
		{
			float distancia = Vector3.Distance(origem, enemy.transform.position);
			if (distancia < menorDistancia)
			{
				menorDistancia = distancia;
				maisProximo = enemy;
			}
		}

		if (maisProximo == null)
		{
			Destroy(gameObject);
			return;
		}

		target = maisProximo;

		inimigo = target.transform.GetChild(0).transform;
		raio.position = inimigo.position;
		raio.SetParent(inimigo, true);

		if (animacao != null)
		{
			animacao.SetBool("começar", true);
		}

		line.enabled = false;
		active = true;
		timer = duration;
	}

	void Update()
	{
		if (!active) return;

		if (target == null)
		{
			if (animacao != null)
				animacao.SetBool("começar", false);
			return;
		}

		Vector3 start = transform.position;
		Vector3 end = target.transform.position;

		line.SetPosition(0, start);
		line.SetPosition(1, end);

		timer -= Time.deltaTime;
		if (timer <= 0f)
		{
			if (target != null)
			{
				Enemy inimigo = target.GetComponent<Enemy>();
				if (inimigo != null)
				{
					inimigo.TakeDamage(dano);
				}
			}

			Destroy(gameObject);
		}
	}
}
