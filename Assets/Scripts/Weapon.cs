using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Weapon : MonoBehaviour
{
	[Header("Weapon Settings")]
	public GameObject bulletPrefab;   // Prefab da bala
	public Transform firePoint;       // Ponto de disparo
	public float bulletSpeed = 20f;   // Velocidade da bala
	public float fireRate = 0.5f;     // Intervalo entre tiros

	[Header("Ammo")]
	public int magazineSize = 10;     // Balas por pente
	private int currentAmmo;          // Balas no pente
	public float reloadTime = 2f;     // Tempo de recarga
	private bool isReloading = false; // Controle de recarga

	[Header("UI")]
	public Image reloadBar;           // Barra de recarga
	public Text reloadBarText;        // Texto dentro da barra

	private float nextFireTime = 0f;
	private Camera cam;

	void Start()
	{
		cam = Camera.main;

		currentAmmo = magazineSize;
		reloadBar.fillAmount = 0f;
		reloadBarText.text = currentAmmo + " / " + magazineSize;
	}

	void Update()
	{
		if (isReloading) return;

		// Se não tem munição no pente → recarrega automaticamente
		if (currentAmmo <= 0)
		{
			StartCoroutine(Reload());
			return;
		}

		// Disparo
		if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
		{
			Shoot();
			nextFireTime = Time.time + fireRate;
		}
	}

	void Shoot()
	{
		// Calcula direção até o mouse
		Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
		Vector2 direction = (mousePos - (Vector2)firePoint.position).normalized;

		// Cria a bala
		GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

		// Aplica direção e rotação da bala
		bullet.GetComponent<Bullet>().SetDirection(direction);

		// Atualiza munição do pente
		currentAmmo--;
		reloadBarText.text = currentAmmo + " / " + magazineSize;
	}

	IEnumerator Reload()
	{
		isReloading = true;
		reloadBar.fillAmount = 0f;
		reloadBarText.text = "Recarregando...";

		float elapsed = 0f;
		while (elapsed < reloadTime)
		{
			elapsed += Time.deltaTime;
			reloadBar.fillAmount = elapsed / reloadTime;
			yield return null;
		}

		// Sempre recarrega o pente completamente (munição infinita)
		currentAmmo = magazineSize;

		reloadBar.fillAmount = 0f;
		reloadBarText.text = currentAmmo + " / " + magazineSize;

		isReloading = false;
	}
}
