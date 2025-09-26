using UnityEngine;

public class AtivadorObturador : MonoBehaviour
{
	public ControladorDeOrbitadores controlador;
	private bool ativado = false;

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (ativado) return;

		if (collision.CompareTag("Player1"))
		{
			ativado = true;
			if (controlador != null)
			{
				controlador.CriarOrbitador();
				Destroy(gameObject);
			}
		}
	}
}
