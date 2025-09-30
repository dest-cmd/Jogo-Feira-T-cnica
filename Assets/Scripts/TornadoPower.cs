using System.Collections;
using UnityEngine;

public class TornadoPower : MonoBehaviour
{
    public GameObject tornadoPrefab;  // Prefab do tornado
    public float tornadoSpeed = 10f;  // Velocidade do tornado
    public float tornadoDuration = 2f;  // Duração do tornado no mundo
    public float spawnInterval = 5f;   // Intervalo entre a criação de tornados
    public float damageRadius = 5f;    // Raio de dano do tornado

    private Transform player;  // Referência ao personagem jogador
    private float nextSpawnTime;  // Tempo para o próximo tornado aparecer

    void Start()
    {
        player = GameObject.FindWithTag("Player1").transform;  // Encontra o jogador na cena
        nextSpawnTime = Time.time + spawnInterval;  // Define o próximo tornado para daqui 5 segundos
    }

    void Update()
    {
        // Verifica se já passou o tempo para gerar o tornado
        if (Time.time >= nextSpawnTime)
        {
            SpawnTornado();
            nextSpawnTime = Time.time + spawnInterval;  // Reset o tempo para o próximo tornado
        }
    }

    void SpawnTornado()
    {
        // Cria o tornado na posição do jogador
        GameObject tornado = Instantiate(tornadoPrefab, player.position, Quaternion.identity);

        // Garantir que o tornado não tenha gravidade, pois estamos controlando o movimento manualmente
        Rigidbody rb = tornado.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = tornado.AddComponent<Rigidbody>();  // Adiciona um Rigidbody caso não tenha
        }

        // Desativa a gravidade para evitar que o tornado caia
        rb.useGravity = false;

        // Calcula uma direção aleatória para o tornado no plano horizontal
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;

        // Mover o tornado manualmente na direção calculada
        StartCoroutine(MoveTornado(tornado, randomDirection));

        // Destroi o tornado após a duração definida
        Destroy(tornado, tornadoDuration);

        // Ativar a lógica de dano no tornado
        StartCoroutine(DamageZombiesInRange(tornado.transform.position));
    }

    // Função para mover o tornado ao longo do tempo
    IEnumerator MoveTornado(GameObject tornado, Vector3 direction)
    {
        float timeElapsed = 0f;

        while (timeElapsed < tornadoDuration)
        {
            tornado.transform.position += direction * tornadoSpeed * Time.deltaTime;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    // Função para causar dano aos zumbis no raio do tornado
    IEnumerator DamageZombiesInRange(Vector3 tornadoPosition)
    {
        Collider[] hitColliders = Physics.OverlapSphere(tornadoPosition, damageRadius);
        foreach (var collider in hitColliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                // Aqui você pode aplicar o dano ao zumbi
                collider.GetComponent<Enemy>().TakeDamage(10);  // Exemplo de método para dano
            }
        }

        yield return null;
    }
}
