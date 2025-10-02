using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class menu : MonoBehaviour
{
    //adicionar um panel,Colocar o script na main camera,colocar o panel no pmenu e o script player no personagem
    public Transform pmenu;
    public int qtdExp;
    public BarraExp barra;

    private bool abriuMenu = false;

    void Update()
    {
        //trocar chave para quantidade de exp
        if (qtdExp == 1 && !abriuMenu)
        {
            Time.timeScale = 0;
            pmenu.gameObject.SetActive(true);
            abriuMenu = true;
        }
    }
    //trocar qtd chave por qtd exp, exemplo, quando chegar a 100% ele abrira e tal tal tal
    //nao esquecer do texto escolha um em cima, adicionar os botoes dentro do panel adicionar os onclick certos
    //colocar no exp:public menu scriptmenu; adicionar a camera com script no inspector 
    //adicionar isso scriptmenu.qtdexp += 1 no script do exp, para a tela poder funcionar
    //colocar uma imagem pro botão, com a descrição e nome do poder, 
    //nos botoes colocar o script de menu no onclick e tambem o script que da poder ao personagem
    public void FecharMenu()
    {
        qtdExp = 0;
        Time.timeScale = 1;
        pmenu.gameObject.SetActive(false);
        abriuMenu = false;
    }
}
