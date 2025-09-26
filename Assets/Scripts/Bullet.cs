using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float speed = 20f; // velocidade da bala
	public int damage = 1;    // dano que a bala causa
	public float lifetime = 2f; // tempo até a bala desaparecer

	private Vector2 direction;

	// Define a direção da bala ao ser instanciada
	public void SetDirection(Vector2 dir)
	{
		direction = dir.normalized;

		// calcula o ângulo baseado na direção
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

		// aplica a rotação inicial no sprite
		// -90 porque o eixo "frente" do sprite está apontando para cima (Y)
		transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
	}

	void Start()
	{
		Destroy(gameObject, lifetime);
	}

	void Update()
	{
		// Move a bala na direção definida
		transform.Translate(direction * speed * Time.deltaTime, Space.World);
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy"))
		{
			collision.GetComponent<Enemy>().TakeDamage(damage);
			Destroy(gameObject);
		}
		else if (collision.CompareTag("Obstacle"))
		{
			Destroy(gameObject);
		}
	}
}
