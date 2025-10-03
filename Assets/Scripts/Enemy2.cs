using UnityEngine;
using System.Collections;

public class Enemy2 : MonoBehaviour
{
	private Transform player;
	private SpriteRenderer spriteRenderer;

	void Start()
	{
		GameObject playerObj = GameObject.FindGameObjectWithTag("Player1");
		if (playerObj != null)
			player = playerObj.transform;

		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		if (player == null || spriteRenderer == null) return;

		// Verifica se o player está à esquerda ou direita
		if (player.position.x < transform.position.x)
			spriteRenderer.flipX = true;  // vira para esquerda
		else
			spriteRenderer.flipX = false; // vira para direita
	}
}

