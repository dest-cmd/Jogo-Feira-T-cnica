using UnityEngine;

public class Player : MonoBehaviour
{
	[Header("Movimento")]
	public float speed = 5f;                // Velocidade do player

	[Header("Rotação")]
	public float rotationOffset = -90f;     // Ajuste para alinhar o sprite com a mira

	private Rigidbody2D rb;
	private Vector2 moveInput;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		// Captura o input de movimento
		moveInput.x = Input.GetAxisRaw("Horizontal");
		moveInput.y = Input.GetAxisRaw("Vertical");

		// Faz o player olhar para o mouse
		LookAtMouse();
	}

	void FixedUpdate()
	{
		// Move o player
		rb.MovePosition(rb.position + moveInput.normalized * speed * Time.fixedDeltaTime);
	}

	void LookAtMouse()
	{
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePos.z = 0f; // Z do mouse deve ser zero no mundo 2D
		Vector3 direction = mousePos - transform.position;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

		// Aplica a rotação com offset
		transform.rotation = Quaternion.Euler(0f, 0f, angle + rotationOffset);
	}
}
