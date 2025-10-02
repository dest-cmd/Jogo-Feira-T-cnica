using UnityEngine;

public class Bomba : MonoBehaviour
{
    public float delay = 0.5f;             // tempo até explodir
    public float radius = 4f;              // raio da explosão
    public int damage = 100;               // dano
    public LayerMask enemyLayerMask2D;     // defina como "Enemy" no Inspector
    public float explosionForce = 500f;    // força da explosão
    public Rigidbody2D rbBomba;            // Rigidbody2D da bomba
    public float velocidadeInicial = 10f;  // velocidade que a bomba será lançada

    void Start()
    {
        // Encontra o inimigo mais próximo
        GameObject inimigoMaisProximo = EncontrarInimigoMaisProximo();

        if (inimigoMaisProximo != null)
        {
            // Calcula a direção normalizada até o inimigo
            Vector2 direcao = ((Vector2)inimigoMaisProximo.transform.position - rbBomba.position).normalized;

            // Aplica a velocidade na direção do inimigo
            rbBomba.velocity = direcao * velocidadeInicial;
        }

        // Aguarda o tempo para explodir
        if (delay <= 0f)
            Explode();
        else
            Invoke("Explode", delay);
    }

    GameObject EncontrarInimigoMaisProximo()
    {
        GameObject[] inimigos = GameObject.FindGameObjectsWithTag("Enemy"); // os inimigos devem ter a tag "Enemy"
        GameObject maisProximo = null;
        float menorDistancia = Mathf.Infinity;
        Vector3 posicaoAtual = transform.position;

        foreach (GameObject inimigo in inimigos)
        {
            float distancia = Vector3.Distance(posicaoAtual, inimigo.transform.position);
            if (distancia < menorDistancia)
            {
                menorDistancia = distancia;
                maisProximo = inimigo;
            }
        }

        return maisProximo;
    }

    void Explode()
    {
        // Pega todos os inimigos dentro do raio
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, enemyLayerMask2D);

        foreach (Collider2D hit in hits)
        {
            // Aplica dano
            var enemyHealth = hit.GetComponent<Enemy>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            // Aplica força de explosão
            Rigidbody2D rb = hit.attachedRigidbody;
            if (rb != null)
            {
                Vector2 dir = rb.position - (Vector2)transform.position;
                float falloff = Mathf.Clamp01(1f - dir.magnitude / radius);
                rb.AddForce(dir.normalized * explosionForce * falloff);
            }
        }

        // Destroi a bomba após explodir
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0.5f, 0f, 0.3f);
        Gizmos.DrawSphere(transform.position, radius);
    }
}
