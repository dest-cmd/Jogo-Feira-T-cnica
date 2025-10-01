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
		Destroy(gameObject, lifetime);
	}

	void FixedUpdate()
	{
		// Movimento constante, independente da física do Player
		rb.velocity = moveDirection * speed;
	}

	public void SetDirection(Vector2 dir)
	{
		moveDirection = dir.normalized;

		// Rotaciona tornado na direção do movimento
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0, 0, angle);
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
