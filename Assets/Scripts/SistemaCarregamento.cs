using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SistemaCarregamento : MonoBehaviour {

	public Text UIPorcento;

	public int qtd;
	public int qtdMax;

	public Image[] branco;

	public Sprite cheio;
	public Sprite vazio;

	public float Tempo;
	float guardaTempo;

	float media;
	float UmaMedia;


	void Start () {
		guardaTempo = Tempo;
		media = 100 / 17;
		UmaMedia = media;
	}
	

	void Update () {
		Logica ();
		Voltar();

	}

	void Logica(){


		if (Tempo > 0) {
			Tempo -= Time.deltaTime;
		} else {
			qtd -= 1;
			Tempo = guardaTempo;
			media += UmaMedia;
			string TxtMedia = media.ToString ();

			UIPorcento.text = TxtMedia;
		}


		if (qtd > qtdMax) {
			qtd = qtdMax;
		}
		for (int i = 0; i < branco.Length; i++) {
			if (i < qtd) {
				branco [i].sprite = cheio;
			} else {
				branco [i].sprite = vazio;
			}

			if (i < qtdMax) {
				branco[i].enabled = true;
			
			} else {
				branco[i].enabled = false;
			}
		}

	}

	void Voltar (){
		if (media >= 100) {
			SceneManager.LoadScene ("Tela de Titulo");
		}
	
	}
}
