using UnityEngine;

public class Player : MonoBehaviour
{
	[Header("Movimento")]
	public float moveSpeed = 5f;
	public Rigidbody2D rb;
	public Camera cam;

	[Header("Referência ao sprite (child)")]
	public Transform spriteTransform;

	[Header("Ajuste de ângulo (se sprite aponta pra cima = -90)")]
	public float angleOffset = -90f;

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

		// Normaliza para evitar diagonal mais rápida
		movement = movement.normalized;
	}

	void FixedUpdate()
	{
		// Movimento via física
		rb.velocity = movement * moveSpeed;

		// Rotação aplicada somente ao sprite
		Vector2 lookDir = mousePos - rb.position;
		float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + angleOffset;
		spriteTransform.rotation = Quaternion.Euler(0, 0, angle);
	}
}
