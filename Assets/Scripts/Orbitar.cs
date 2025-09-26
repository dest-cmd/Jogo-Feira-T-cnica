using System.Collections;
using UnityEngine;

public class Orbitar : MonoBehaviour
{
	[Header("Referências")]
	public Transform personagem;
	[HideInInspector] public ControladorDeOrbitadores controlador;

	[Header("Configuração da Órbita")]
	public float velocidade = 100f;
	public float raio = 2f;

	[Header("Renderização")]
	public string layerAtivado = "Poder";
	public int orderInLayer = 5;

	[Header("Collider")]
	public float raioCollider = 0.5f;
	public float delayCollider = 0.1f;

	[Header("Controle")]
	public int indiceOrbita = 0;
	public int quantidadeTotal = 1;

	private float angulo = 0f;
	private SpriteRenderer spriteRenderer;
	private CircleCollider2D circleCollider;
	private bool ativado = false;

	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		if (spriteRenderer != null)
			spriteRenderer.enabled = false;

		circleCollider = GetComponent<CircleCollider2D>();
		if (circleCollider == null)
			circleCollider = gameObject.AddComponent<CircleCollider2D>();

		circleCollider.radius = raioCollider;
		circleCollider.enabled = false;
		circleCollider.isTrigger = true;

		if (GetComponent<Rigidbody2D>() == null)
		{
			Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
			rb.bodyType = RigidbodyType2D.Kinematic;
			rb.simulated = true;
		}
	}

	void Update()
	{
		if (!ativado || personagem == null) return;

		angulo += velocidade * Time.deltaTime;

		int total = Mathf.Max(1, quantidadeTotal);
		int idx = ((indiceOrbita % total) + total) % total;

		float deslocamento = 360f / total;
		float anguloFinal = angulo + deslocamento * idx;

		float x = Mathf.Cos(anguloFinal * Mathf.Deg2Rad) * raio;
		float y = Mathf.Sin(anguloFinal * Mathf.Deg2Rad) * raio;

		transform.position = new Vector3(
			personagem.position.x + x,
			personagem.position.y + y,
			transform.position.z
		);
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (!ativado) return;

		if (collision.CompareTag("Enemy"))
		{
			Destroy(collision.gameObject);
		}
		else if (collision.CompareTag("Obstacle"))
		{
			if (controlador != null)
				controlador.RemoverOrbitador(this);

			Destroy(gameObject);
		}
	}

	public void Ativar()
	{
		ativado = true;

		if (spriteRenderer != null)
			spriteRenderer.enabled = true;

		StartCoroutine(AtivarColliderComDelay());

		int layerIndex = LayerMask.NameToLayer(layerAtivado);
		if (layerIndex != -1)
			gameObject.layer = layerIndex;
	}

	private IEnumerator AtivarColliderComDelay()
	{
		yield return new WaitForSeconds(delayCollider);
		if (circleCollider != null)
			circleCollider.enabled = true;
	}

	public void Desativar()
	{
		ativado = false;
		if (spriteRenderer != null)
			spriteRenderer.enabled = false;
		if (circleCollider != null)
			circleCollider.enabled = false;

		gameObject.layer = LayerMask.NameToLayer("Default");
	}

	public void SetAnguloInicial(float ang)
	{
		angulo = ang;
	}
}
