using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public int health = 3;
    public int xpValue = 5; // quanto de XP esse inimigo dá
    public GameObject[] itemDrops; // lista de itens que ele pode dropar
    public float dropChance = 0.3f; // 30% de chance de dropar

    private Transform player;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player1");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            speed * Time.deltaTime
        );
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Dar XP para o player
        PlayerXP playerXP = FindObjectOfType<PlayerXP>();
        if (playerXP != null)
        {
            playerXP.AddXP(xpValue);
        }

        // Chance de dropar item
        if (itemDrops.Length > 0 && Random.value < dropChance)
        {
            int index = Random.Range(0, itemDrops.Length);
            Instantiate(itemDrops[index], transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}