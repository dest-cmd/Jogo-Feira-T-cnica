using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load_Scene : MonoBehaviour
{

	public void Tutorial(){

		SceneManager.LoadScene("Tutorial");
	}

    public void TeladeTitulo()
    {

        SceneManager.LoadScene("Tela de Titulo");
    }

	public void Creditos(){

		SceneManager.LoadScene("Creditos");
	}
	public void SelecionarFase(){

		SceneManager.LoadScene("Seletor de fases");
	}
	public void Fase1(){

		SceneManager.LoadScene("Fase 1");
	}
	public void TeladeCarregamento(){

		SceneManager.LoadScene("Tela de Carregamento");
	}

}

