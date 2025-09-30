using UnityEngine;

public class PlayerXP : MonoBehaviour
{
    public int currentXP = 0;
    public int level = 1;
    public int xpToNextLevel = 10;

    public GameObject levelUpPanel; // arrasta no Inspector a UI de habilidades

    public void AddXP(int amount)
    {
        currentXP += amount;

        if (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        level++;
        currentXP = 0;
        xpToNextLevel += 10; // cada vez fica mais difícil upar

        if (levelUpPanel != null)
        {
            levelUpPanel.SetActive(true); // abre a tela para escolher habilidade
            Time.timeScale = 0; // pausa o jogo para o jogador escolher
        }
    }
}