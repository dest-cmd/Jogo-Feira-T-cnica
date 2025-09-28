using UnityEngine;
using UnityEngine.UI;

public class Camera_do_SLF : MonoBehaviour
{
	public Camera cam;                 // A câmera do jogo
	public Scrollbar scrollbar;        // Scrollbar Vertical
	public float minY = 0f;            // Posição mínima da câmera
	public float maxY = 20f;           // Posição máxima da câmera

	void Update()
	{
		// Pega o valor da barra (0 = embaixo, 1 = em cima)
		float scrollValue = scrollbar.value;

		// Move a câmera entre minY e maxY
		Vector3 pos = cam.transform.position;
		pos.y = Mathf.Lerp(minY, maxY, scrollValue);
		cam.transform.position = pos;
	}
}
