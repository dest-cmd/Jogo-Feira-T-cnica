using UnityEngine;

public class Bomba : MonoBehaviour
{
	public float delay = 0.5f;             // tempo até explodir
	public float radius = 3f;              // raio da explosão
	public int damage = 100;               // dano
	public LayerMask enemyLayerMask2D;     // defina como "Enemy" no inspector
	public float explosionForce = 500f;    // força aplicada (opcional)

	void Start()
	{
		if (delay <= 0f) Explode();
		else Invoke("Explode", delay);
	}

	void Explode()
	{
		// pega todos os inimigos no raio
		Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, enemyLayerMask2D);

		foreach (Collider2D hit in hits)
		{
			// aplica dano
			var enemyHealth = hit.GetComponent<Enemy>();
			if (enemyHealth != null)
			{
				enemyHealth.TakeDamage(damage);
			}

			// empurra com força de explosão (se tiver Rigidbody2D)
			Rigidbody2D rb = hit.attachedRigidbody;
			if (rb != null)
			{
				Vector2 dir = rb.position - (Vector2)transform.position;
				float falloff = Mathf.Clamp01(1f - dir.magnitude / radius);
				rb.AddForce(dir.normalized * explosionForce * falloff);
			}
		}

		// aqui você pode instanciar efeito de partículas, som, etc.
		Destroy(gameObject); // destrói a bomba
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = new Color(1f, 0.5f, 0f, 0.3f);
		Gizmos.DrawSphere(transform.position, radius);
	}
}
