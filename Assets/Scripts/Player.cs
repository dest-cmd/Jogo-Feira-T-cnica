using UnityEngine;

public class Player : MonoBehaviour
{
	public float moveSpeed = 5f;
	public Rigidbody2D rb;
	public Camera cam;

	Vector2 movement;
	Vector2 mousePos;

	void Update()
	{
		// Pega posição do mouse no mundo
		mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

		// Movimento com teclado (WASD)
		movement = Vector2.zero;
		if (Input.GetKey(KeyCode.W)) movement.y = 1;
		if (Input.GetKey(KeyCode.S)) movement.y = -1;
		if (Input.GetKey(KeyCode.A)) movement.x = -1;
		if (Input.GetKey(KeyCode.D)) movement.x = 1;

		// Normaliza para evitar movimento mais rápido na diagonal
		movement = movement.normalized;
	}

	void FixedUpdate()
	{
		// Move o player
		rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

		// Rotaciona o player para olhar o mouse
		Vector2 lookDir = mousePos - rb.position;
		float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
		rb.rotation = angle;
	}
}
