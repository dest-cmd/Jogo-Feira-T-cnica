using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
	[Header("Vida do Jogador")]
	public int maxHealth = 100;
	private int currentHealth;

	[Header("UI")]
	public Image healthBar;

	[Header("Dano")]
	public int damagePerSecond = 10;   // dano por segundo
	private bool isTakingDamage = false;

	private Coroutine damageCoroutine;
	private GameObject currentEnemy; // inimigo atual que está causando dano

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
		SceneManager.LoadScene("GameOver");
	}

	void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
			if (!isTakingDamage || currentEnemy != collision.gameObject)
			{
				if (damageCoroutine != null)
					StopCoroutine(damageCoroutine);

				currentEnemy = collision.gameObject;
				damageCoroutine = StartCoroutine(DamageOverTime());
			}
		}
	}

	IEnumerator DamageOverTime()
	{
		isTakingDamage = true;

		while (currentEnemy != null)
		{
			// aplica metade do dano por tick (porque o intervalo é 0,5s)
			int damagePerTick = damagePerSecond / 2;
			TakeDamage(damagePerTick);

			if (currentEnemy == null || !currentEnemy.activeInHierarchy)
			{
				break;
			}

			yield return new WaitForSeconds(0.5f); // dano a cada 0,5 segundos
		}

		isTakingDamage = false;
		damageCoroutine = null;
	}

	void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject == currentEnemy)
		{
			if (damageCoroutine != null)
				StopCoroutine(damageCoroutine);

			damageCoroutine = null;
			currentEnemy = null;
			isTakingDamage = false;
		}
	}
}
