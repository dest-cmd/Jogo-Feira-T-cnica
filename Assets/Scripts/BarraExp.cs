using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BarraExp : MonoBehaviour {
	public menu scriptmenu;
	public float nivelatual = 1;
	public float nivelmax = 5;
	public float explevel = 100;
	public float multiplicadorexp = 1.5f;
	private float expatual;
	private float explevelatual;
	private Text nivel;
	private UnityEngine.UI.Slider slider;
	private UnityEngine.UI.Image color;

	void Start () {
		nivel = transform.GetComponentInChildren<Text>();
		slider = transform.GetComponentInChildren<UnityEngine.UI.Slider>();
		color = transform.GetChild(1).transform.GetChild(0).GetComponent<UnityEngine.UI.Image>();

		if(explevelatual == 0) {
			explevelatual = explevel;
		}
	}

	void Update () {

	}

	public void adicionarexp(float exp) {
		float soma = expatual + exp;
		float sub = soma - explevelatual;

		if(soma < explevelatual) {
			expatual += exp;
		} 
		else if (soma == explevelatual) {
			Upar(0);
			expatual = 0;
		} 
		else if(soma > explevelatual) {
			expatual = 0;
			Upar(sub);
		}
		atualizar();
	}

	public void Upar(float exp) {
		if (nivelatual >= nivelmax) {
			expatual = explevelatual;
			atualizar();
			return;
		}

		float nextlevel = explevelatual * multiplicadorexp;
		nivelatual++;
		explevelatual = nextlevel;

		adicionarexp(exp);
		scriptmenu.qtdExp = 1;

		// ✅ Correção: agora usa o nome certo da cena "Vitoria"
		if(nivelatual >= 5) {
			string cenaAtual = SceneManager.GetActiveScene().name.Trim();
			Debug.Log("Cheguei no nível " + nivelatual + "! Cena atual: " + cenaAtual);

			if (cenaAtual == "Fase 3") {
				Debug.Log("Carregando Vitoria...");
				SceneManager.LoadScene("Vitoria");
			}
			else {
				Debug.Log("Carregando Seletor de fases...");
				SceneManager.LoadScene("Seletor de fases");
			}
		}
	} 

	public void atualizar() {
		nivel.text = "" + nivelatual;
		slider.maxValue = explevelatual;
		slider.value = expatual;

		color.color = Color.Lerp(Color.red, Color.green, slider.value / slider.maxValue);
	}
}
