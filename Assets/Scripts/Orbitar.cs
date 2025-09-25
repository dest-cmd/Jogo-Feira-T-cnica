using UnityEngine;

public class Orbitar : MonoBehaviour {
	public Transform personagem;        
	public float velocidade = 100f;     
	public float raio = 2f;             
	private float angulo = 0f;

	private SpriteRenderer spriteRenderer;
	private CircleCollider2D circleCollider;
	private CircleCollider2D gatilhoCollider; // para ativar poder
	private bool ativado = false;       

	// Configurações do poder
	public string layerAtivado = "Poder";
	public int orderInLayer = 5;        
	public float raioCollider = 0.5f;   
	public string tagAtivador = "ativar"; 

	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		if (spriteRenderer != null)
			spriteRenderer.enabled = false; // começa invisível

		// Collider principal do poder
		circleCollider = GetComponent<CircleCollider2D>();
		if (circleCollider == null)
		{
			circleCollider = gameObject.AddComponent<CircleCollider2D>();
			circleCollider.radius = raioCollider;
		}
		circleCollider.enabled = false; // só ativa ao orbitar
		circleCollider.isTrigger = true;

		// Collider apenas para ativação
		gatilhoCollider = gameObject.AddComponent<CircleCollider2D>();
		gatilhoCollider.radius = 0.1f; // pequeno, só para detecção
		gatilhoCollider.isTrigger = true;
	}

	void Update()
	{
		if (!ativado || personagem == null) return;

		angulo += velocidade * Time.deltaTime;

		float x = Mathf.Cos(angulo * Mathf.Deg2Rad) * raio;
		float y = Mathf.Sin(angulo * Mathf.Deg2Rad) * raio;

		transform.position = new Vector3(personagem.position.x + x,
			personagem.position.y + y,
			transform.position.z);
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag(tagAtivador) && !ativado)
		{
			AtivarPoder();
		}

		if (ativado)
		{
			if (collision.CompareTag("Enemy"))
				Destroy(collision.gameObject);
			else if (collision.CompareTag("Obstacle"))
				Destroy(gameObject);
		}
	}

	void AtivarPoder()
	{
		ativado = true;

		if (spriteRenderer == null)
		{
			spriteRenderer.enabled = true;
			spriteRenderer.sortingOrder = orderInLayer;
		}

		if (circleCollider == null)
			circleCollider.enabled = true;

		gameObject.layer = LayerMask.NameToLayer(layerAtivado);
	}
}
