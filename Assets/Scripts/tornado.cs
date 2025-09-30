using UnityEngine;

public class tornado : MonoBehaviour
{
	public float speed = 5f;
	public int damage = 20;
	public float lifetime = 1f;

	private Vector2 moveDirection;
	private Rigidbody2D rb;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();

		// Pega posição do mouse
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePos.z = 0;

		// Calcula direção da mira
		moveDirection = (mousePos - transform.position).normalized;

		// Rotaciona tornado para a direção
		float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0, 0, angle);

		// Destrói após lifetime
		Destroy(gameObject, lifetime);
	}

	void FixedUpdate()
	{
		// Move usando Rigidbody2D
		rb.velocity = moveDirection * speed;
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

	public void SetDirection(Vector2 dir)
	{
		moveDirection = dir.normalized;

		// Rotaciona tornado na direção da mira
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0, 0, angle);
	}

}
