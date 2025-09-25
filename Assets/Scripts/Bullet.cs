using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Camera mainCamera;

    Vector2 movement;
    Vector2 mousePos;

	public float speed = 10f; // velocidade da bala
	public int damage = 1;    // dano que a bala causa

	private Vector2 direction;

	// Define a direção da bala ao ser instanciada
	public void SetDirection(Vector2 dir)
	{
		direction = dir.normalized;
	}

	void Update()
	{
		// Move a bala
		transform.Translate(direction * speed * Time.deltaTime, Space.World);

        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        Vector2 lookDir = mousePos - rb.position;

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        rb.rotation = angle;
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		// Checa se colidiu com um inimigo
		if (collision.CompareTag("Enemy"))
		{
			collision.GetComponent<Enemy>().TakeDamage(damage);
			Destroy(gameObject); // destrói a bala
		}
		else if (collision.CompareTag("Obstacle"))
		{
			Destroy(gameObject); // destrói se bater em paredes ou objetos
		}
	}
}
