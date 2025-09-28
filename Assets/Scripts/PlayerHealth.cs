using System.Collections;
using System.Collections.Generic;
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
	public int damagePerSecond = 10; // quanto de dano por "tick" (1 segundo)

	// guarda os inimigos atualmente em contato (evita duplicatas)
	private HashSet<GameObject> enemiesTouching = new HashSet<GameObject>();

	// referência à corrotina para poder parar quando necessário
	private Coroutine damageCoroutine = null;

	void Start()
	{
		currentHealth = maxHealth;
		UpdateHealthUI();
	}

	void UpdateHealthUI()
	{
		if (healthBar != null)
			healthBar.fillAmount = Mathf.Clamp01((float)currentHealth / maxHealth);
	}

	public void TakeDamage(int amount)
	{
		if (currentHealth <= 0) return; // já morto, evita múltiplas chamadas
		currentHealth -= amount;
		currentHealth = Mathf.Max(currentHealth, 0);
		UpdateHealthUI();

		if (currentHealth <= 0)
			Die();
	}

	void Die()
	{
		// desative controles, animações, etc, se quiser
		SceneManager.LoadScene("GameOver");
	}

	// Usando colisões 2D (se você usa triggers, troque pelos métodos OnTriggerEnter2D/OnTriggerExit2D)
	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
			enemiesTouching.Add(collision.gameObject);
			if (damageCoroutine == null)
				damageCoroutine = StartCoroutine(DamageWhileTouching());
		}
	}

	void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
			enemiesTouching.Remove(collision.gameObject);

			// se não houver mais inimigos, para a corrotina
			if (enemiesTouching.Count == 0 && damageCoroutine != null)
			{
				StopCoroutine(damageCoroutine);
				damageCoroutine = null;
			}
		}
	}

	IEnumerator DamageWhileTouching()
	{
		while (true)
		{
			// Limpa inimigos destruídos ou desativados (Destroy -> become null; ou SetActive(false))
			enemiesTouching.RemoveWhere(item => item == null || !item.activeInHierarchy);

			// se não houver mais inimigos, encerra a corrotina
			if (enemiesTouching.Count == 0)
			{
				damageCoroutine = null;
				yield break;
			}

			// aplica dano 1 vez por segundo
			TakeDamage(damagePerSecond);
			yield return new WaitForSeconds(1f);
		}
	}
}
