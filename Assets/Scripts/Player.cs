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
		// Captura posição do mouse no mundo (apenas para rotação)
		mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

		// Movimento com teclado (WASD)
		movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
	}

	void FixedUpdate()
	{
		// Move o player sem interferência de forças externas
		rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

		// Rotaciona o player para olhar o mouse
		Vector2 lookDir = mousePos - rb.position;
		float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
		rb.rotation = angle;
	}

	// Função para receber dano (pode ser chamada pelo inimigo)
	public void TakeDamage(int damage)
	{
		// Aqui você pode reduzir vida ou chamar efeitos visuais
		Debug.Log("Player recebeu " + damage + " de dano!");
	}
}
