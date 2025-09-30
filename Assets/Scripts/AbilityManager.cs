using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    private Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public void IncreaseDamage()
    {
        player.IncreaseDamage(1);
        ClosePanel();
    }

    public void IncreaseSpeed()
    {
        player.IncreaseSpeed(1f);
        ClosePanel();
    }

    public void IncreaseHealth()
    {
        player.IncreaseHealth(5);
        ClosePanel();
    }

    void ClosePanel()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1; // volta o jogo
    }
}
