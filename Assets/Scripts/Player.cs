using UnityEngine;
using UnityEngine.SceneManagement; // Necessário para trocar de cena

public class Player : MonoBehaviour
{
	[Header("Movimento")]
	public float moveSpeed = 5f;
	public Rigidbody2D rb;
	public Camera cam;

	[Header("Troca de Cena")]
	public string cenaDestino; // Nome da cena para onde vai quando encostar

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

		// Normaliza para não correr mais na diagonal
		movement = movement.normalized;
	}

	void FixedUpdate()
	{
		// Move o player usando velocity
		rb.velocity = movement * moveSpeed;

		// Rotaciona o player para olhar para o mouse
		Vector2 lookDir = mousePos - rb.position;
		float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f; // usa -90f se sprite aponta pra cima
		rb.rotation = angle;
	}

	// Detecta colisão com colliders marcados como Trigger
	private void OnTriggerEnter2D(Collider2D other)
	{
		// Troca de cena quando encosta em um collider com tag "Portal"
		if (other.CompareTag("Portal"))
		{
			SceneManager.LoadScene("Seletor de fases");
		}
	}
}
