using UnityEngine;

public class Enemy : MonoBehaviour
{
	public float speed = 2f;
	public int health = 3;

	private Transform player;

	void Start()
	{
		GameObject playerObj = GameObject.FindGameObjectWithTag("Player1");
		if (playerObj != null)
		{
			player = playerObj.transform;
		}
	}

	void Update()
	{
		if (player == null) return;

		// movimentação básica
		transform.position = Vector2.MoveTowards(
			transform.position,
			player.position,
			speed * Time.deltaTime
		);
	}

	public void TakeDamage(int damage)
	{
		health -= damage;
		if (health <= 0)
		{
			Destroy(gameObject);
		}
	}
}
