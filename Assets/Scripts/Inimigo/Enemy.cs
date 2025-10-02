using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public int health = 3;
    public float flashTime = 0.1f;
    public float expDrop = 20f; // quanto de exp o inimigo vai dar

    private Transform player;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private BarraExp barraExp;

    void Start()
    {
        // pega o player
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player1");
        if (playerObj != null)
            player = playerObj.transform;

        // sprite para flash vermelho
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;

        // pega referência da barra de exp
        barraExp = FindObjectOfType<BarraExp>();
    }

    void Update()
    {
        if (player == null) return;

        // movimentação básica em direção ao player
        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            speed * Time.deltaTime
        );
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        // flash vermelho
        if (spriteRenderer != null)
            StartCoroutine(FlashRed());

        // morreu
        if (health <= 0)
        {
            if (barraExp != null)
                barraExp.adicionarexp(expDrop);

            Destroy(gameObject);
        }
    }

    // efeito de flash vermelho
    IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(flashTime);
        spriteRenderer.color = originalColor;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Aplica dano ao player quando colidir
        if (collision.CompareTag("Player1"))
        {
            Player playerScript = collision.GetComponent<Player>();
            if (playerScript != null)
                playerScript.TakeDamage(1); // exemplo: 1 de dano
        }
    }
}

