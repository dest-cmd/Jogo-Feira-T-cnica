using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivadorObturador : MonoBehaviour {

	public GameObject orbitador;           // arraste o orbitador aqui
	public string tagObjetoAtivador = "ativador"; // Tag do objeto que ativa o orbitador

	private SpriteRenderer spriteRenderer;

	void Start()
	{
		if (orbitador != null)
		{
			// Pega o SpriteRenderer do orbitador
			spriteRenderer = orbitador.GetComponent<SpriteRenderer>();

			// Começa invisível
			if (spriteRenderer != null)
				spriteRenderer.enabled = false;
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		// Verifica se colidiu com o objeto que ativa
		if (collision.collider.CompareTag(tagObjetoAtivador))
		{
			// Torna visível
			if (spriteRenderer != null)
				spriteRenderer.enabled = true;
		}
	}
}