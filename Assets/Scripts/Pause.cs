using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Pause : MonoBehaviour
{
    public Transform pause;

    private bool abriuMenu = false;

    void Update()
    {
        //trocar chave para quantidade de exp
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            pause.gameObject.SetActive(true);

        }
    }
    //trocar qtd chave por qtd exp, exemplo, quando chegar a 100% ele abrira e tal tal tal
    //nao esquecer do texto escolha um em cima, adicionar os botoes dentro do panel adicionar os onclick certos
    //colocar no exp:public menu scriptmenu; adicionar a camera com script no inspector 
    //adicionar isso scriptmenu.qtdexp += 1 no script do exp, para a tela poder funcionar
    //colocar uma imagem pro botão, com a descrição e nome do poder, 
    //nos botoes colocar o script de menu no onclick e tambem o script que da poder ao personagem
    public void SeletorFase()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Seletor de fases");
    }
    public void SairFase()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Tela de Titulo");
    }
    public void continuar()
    {
        Time.timeScale = 1;
        pause.gameObject.SetActive(false);
    }
}