using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;


public class BarraExp : MonoBehaviour {
    public menu scriptmenu;
	public float nivelatual = 1;
    public float nivelmax = 10;
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

		if(explevelatual == 0)
		{
			explevelatual = explevel;

		}
	}
	
	
	void Update () {
		
	}

	public void adicionarexp(float exp)
	{
		float soma = expatual + exp;
		float sub = soma - explevelatual;

		if(soma < explevelatual)
		{
			expatual += exp;
            
		} else if (soma == explevelatual)
		{
			Upar(0);
			expatual = 0;
            scriptmenu.qtdExp = 1;
		} else if(soma > explevelatual)
		{
			expatual = 0;
			Upar(sub);
            scriptmenu.qtdExp = 0;
		}
		atualizar();
	}
	public void Upar(float exp)
	{
		if (nivelatual >= nivelmax)
		{
			expatual = explevelatual;
            scriptmenu.qtdExp = 1;
			atualizar();
			return;
		}
		float nextlevel = explevelatual * multiplicadorexp;
		nivelatual++;
		explevelatual = nextlevel;

		adicionarexp(exp);
        
    }
	public void atualizar() {
		nivel.text = "" + nivelatual;
		slider.maxValue = explevelatual;
		slider.value = expatual;
        
        color.color = Color.Lerp(Color.white, Color.white, slider.value / slider.maxValue);
	}
}
