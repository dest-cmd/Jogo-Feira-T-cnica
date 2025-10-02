using UnityEngine;

public class tornado : MonoBehaviour
{
	public float speed = 5f;
	public int damage = 20;
	public float lifetime = 3f; // quanto tempo o tornado fica vivo
	private Rigidbody2D rb;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();

		// Procura inimigo mais próximo
		GameObject inimigoMaisProximo = EncontrarInimigoMaisProximo();

		if (inimigoMaisProximo != null)
		{
			// Direção do tornado para o inimigo
			Vector2 direcao = ((Vector2)inimigoMaisProximo.transform.position - rb.position).normalized;
			rb.velocity = direcao * speed;

			// Rotaciona tornado na direção do movimento
			float angle = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0, 0, angle);
		}
		else
		{
			// Se não tiver inimigo, segue reto pra frente (opcional)
			rb.velocity = transform.right * speed;
		}

		// Destroi tornado após o tempo de vida
		Destroy(gameObject, lifetime);
	}

	GameObject EncontrarInimigoMaisProximo()
	{
		GameObject[] inimigos = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject maisProximo = null;
		float menorDistancia = Mathf.Infinity;
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

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Enemy"))
		{
			Enemy enemyHealth = other.GetComponent<Enemy>();
			if (enemyHealth != null)
			{
				enemyHealth.TakeDamage(damage);
			}
		}
	}
}
