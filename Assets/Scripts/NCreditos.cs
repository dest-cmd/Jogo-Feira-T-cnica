using UnityEngine;

public class NCreditos : MonoBehaviour
{
    public RectTransform creditsPanel;     // Painel que contém os créditos (UI)
    public float scrollSpeed = 20f;        // Velocidade constante em unidades por segundo
    public float stopYPosition = 1000f;    // Posição Y para parar os créditos

    private Vector2 startPosition;

    void Start()
    {
        if (creditsPanel != null)
        {
            startPosition = creditsPanel.anchoredPosition;
        }
    }

    void Update()
    {
        if (creditsPanel != null)
        {
            // Novo valor Y com base no tempo real, velocidade constante
            float newY = creditsPanel.anchoredPosition.y + scrollSpeed * Time.deltaTime;

            // Atualiza a posição Y
            creditsPanel.anchoredPosition = new Vector2(startPosition.x, newY);

            // Se atingir o limite, para
            if (newY >= stopYPosition)
            {
                enabled = false; // Para o movimento
            }
        }
    }
}

