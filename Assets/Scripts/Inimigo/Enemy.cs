using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public float speed = 2f;
	public int health = 3;
	public float flashTime = 0.1f;

	private Transform player;
	private SpriteRenderer spriteRenderer;
	private Color originalColor;

	void Start()
	{
		GameObject playerObj = GameObject.FindGameObjectWithTag("Player1");
		if (playerObj != null)
			player = playerObj.transform;

		spriteRenderer = GetComponent<SpriteRenderer>();
		if (spriteRenderer != null)
			originalColor = spriteRenderer.color;
	}

	void Update()
	{
		if (player == null) return;

		// Movimenta o inimigo kinematic, sem empurrar o player
		transform.position = Vector2.MoveTowards(
			transform.position,
			player.position,
			speed * Time.deltaTime
		);
	}

	public void TakeDamage(int damage)
	{
		health -= damage;
		if (spriteRenderer != null)
			StartCoroutine(FlashRed());

		if (health <= 0)
			Destroy(gameObject);
	}

	// Efeito de flash vermelho
	IEnumerator FlashRed()
	{
		spriteRenderer.color = Color.red;
		yield return new WaitForSeconds(flashTime);
		spriteRenderer.color = originalColor;
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		// Aplica dano ao player quando colidir
		if (collision.CompareTag("Player1"))
		{
			Player playerScript = collision.GetComponent<Player>();
			if (playerScript != null)
				playerScript.TakeDamage(1); // exemplo de 1 de dano
		}
	}
}
