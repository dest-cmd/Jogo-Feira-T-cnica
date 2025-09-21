using UnityEngine;

public class Player_Titulo : MonoBehaviour {
	
	float VelocidadeX; 
	float VelocidadeY; 
	float DirecaoHorizontal; 
	float DirecaoVertical; 
	Vector2 VetorVelocidadePersonagem;
	Rigidbody2D CorpoRigidoPersonagem;

	void Start () {
		
		CorpoRigidoPersonagem = GetComponent<Rigidbody2D> ();
		CorpoRigidoPersonagem.gravityScale = 0f;
		CorpoRigidoPersonagem.freezeRotation = true;
		VelocidadeX = 2f;
		VelocidadeY = 0f;
		VetorVelocidadePersonagem = new Vector2 (VelocidadeX, VelocidadeY);
	}
	void Update () {
		MovimentoHorizontal ();
	}

	void MovimentoHorizontal(){
		VelocidadeX = 5f;
		VelocidadeY = 0f;
		VetorVelocidadePersonagem = new Vector2 (VelocidadeX, VelocidadeY);
		CorpoRigidoPersonagem.velocity = VetorVelocidadePersonagem;
	}
}