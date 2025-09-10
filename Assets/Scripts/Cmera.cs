using UnityEngine;

public class Cmera : MonoBehaviour
{
    public Transform player;      // Referência ao player
    public Vector3 offset;        // Distância fixa da câmera para o player

    void Start()
    {
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        Vector3 targetPos = player.position + offset;
        targetPos.z = transform.position.z;  // Mantém a distância Z da câmera
        transform.position = targetPos;

        // Não altera rotação, só posição
    }
}