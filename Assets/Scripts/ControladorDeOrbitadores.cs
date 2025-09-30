using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControladorDeOrbitadores : MonoBehaviour
{
	public GameObject orbitadorPrefab;
	public Transform pontoCentral;

	private List<Orbitar> orbitadores = new List<Orbitar>();

	public void CriarOrbitador()
	{
		if (orbitadorPrefab == null || pontoCentral == null)
		{
			Debug.LogError("[Controlador] Prefab ou pontoCentral não definido!");
			return;
		}

		GameObject novo = Instantiate(orbitadorPrefab, pontoCentral.position, Quaternion.identity);
		Orbitar orbitar = novo.GetComponent<Orbitar>();

		if (orbitar == null)
		{
			Debug.LogError("[Controlador] Prefab não contém Orbitar!");
			Destroy(novo);
			return;
		}

		orbitar.controlador = this;
		orbitar.personagem = pontoCentral;

		orbitadores.Add(orbitar);
		orbitar.Ativar();

		AtualizarIndices();
	}

	private void AtualizarIndices()
	{
		orbitadores.RemoveAll(o => o == null);

		int total = orbitadores.Count;

		for (int i = 0; i < total; i++)
		{
			orbitadores[i].indiceOrbita = i;
			orbitadores[i].quantidadeTotal = total;

			float deslocamento = 360f / total;
			orbitadores[i].SetAnguloInicial(deslocamento * i);
		}
	}

	public void RemoverOrbitador(Orbitar orb)
	{
		if (orb != null)
		{
			orbitadores.Remove(orb);
			StartCoroutine(AtualizarDepoisDeRemover());
		}
	}

	private IEnumerator AtualizarDepoisDeRemover()
	{
		yield return null;
		AtualizarIndices();
	}

	public void RemoverTodos()
	{
		foreach (var orb in orbitadores)
		{
			if (orb != null)
				Destroy(orb.gameObject);
		}
		orbitadores.Clear();
	}
}
