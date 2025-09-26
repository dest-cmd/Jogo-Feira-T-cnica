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
	public int totalAmmo = 50;        // Estoque total
	private int currentAmmo;          // Balas no pente
	public float reloadTime = 2f;     // Tempo de recarga
	private bool isReloading = false; // Controle de recarga

	[Header("UI")]
	public Text totalAmmoText;        // Mostra estoque total
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
		UpdateTotalAmmoUI();
	}

	void Update()
	{
		if (isReloading) return;

		// Se não tem munição no pente
		if (currentAmmo <= 0)
		{
			if (totalAmmo > 0)
				StartCoroutine(Reload());
			else
				reloadBarText.text = "Sem munição!";
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

		// Atualiza munição
		currentAmmo--;
		reloadBarText.text = currentAmmo + " / " + magazineSize;
		UpdateTotalAmmoUI();
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

		int ammoNeeded = magazineSize - currentAmmo;
		int ammoToReload = Mathf.Min(ammoNeeded, totalAmmo);

		currentAmmo += ammoToReload;
		totalAmmo -= ammoToReload;

		reloadBar.fillAmount = 0f;
		reloadBarText.text = currentAmmo + " / " + magazineSize;

		UpdateTotalAmmoUI();
		isReloading = false;
	}

	void UpdateTotalAmmoUI()
	{
		totalAmmoText.text = totalAmmo.ToString();
	}
}
