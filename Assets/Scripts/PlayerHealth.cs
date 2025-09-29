using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
	[Header("Vida do Jogador")]
	public int maxHealth = 100;
	private int currentHealth;

	[Header("UI")]
	public Image healthBar;

	[Header("Dano")]
	public int damagePerSecond = 10; // quanto de dano por segundo o inimigo causa
	private bool isTakingDamage = false;

	void Start()
	{
		currentHealth = maxHealth;
		UpdateHealthUI();
	}

	void UpdateHealthUI()
	{
		healthBar.fillAmount = (float)currentHealth / maxHealth;
	}

	void TakeDamage(int amount)
	{
		currentHealth -= amount;
		if (currentHealth < 0) currentHealth = 0;

		UpdateHealthUI();

		if (currentHealth <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		// Carrega a cena "GameOver"
		SceneManager.LoadScene("GameOver");
	}

	void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
			if (!isTakingDamage)
				StartCoroutine(DamageOverTime());
		}
	}

	System.Collections.IEnumerator DamageOverTime()
	{
		isTakingDamage = true;
		while (true)
		{
			TakeDamage(damagePerSecond);
			yield return new WaitForSeconds(1f); // aplica dano a cada 1 segundo
		}
	}

	void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
			isTakingDamage = false;
			StopAllCoroutines();
		}
	}
}

