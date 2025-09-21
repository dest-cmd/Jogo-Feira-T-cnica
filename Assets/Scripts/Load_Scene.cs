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
<<<<<<< HEAD
	public void TeladeCarregamento(){

		SceneManager.LoadScene("Tela de Carregamento");
	}

=======
>>>>>>> f5016c10548be24a5c7940b4342c03d6acf6d4fc
}

