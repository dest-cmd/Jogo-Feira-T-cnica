using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Weapon : MonoBehaviour
{
	[Header("Weapon Settings")]
	public GameObject bulletPrefab;
	public Transform firePoint;
	public float bulletSpeed = 20f;
	public float fireRate = 0.5f;

	[Header("Ammo")]
	public int magazineSize = 10;   // balas por pente
	public int totalAmmo = 50;      // estoque total
	private int currentAmmo;        // balas no pente
	public float reloadTime = 2f;
	private bool isReloading = false;

	[Header("UI")]
	public Text totalAmmoText;     // mostra apenas o estoque total
	public Image reloadBar;        // barra de recarga
	public Text reloadBarText;     // texto dentro da barra

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
			// Se ainda existe estoque → recarregar
			if (totalAmmo > 0)
			{
				StartCoroutine(Reload());
			}
			else
			{
				reloadBarText.text = "Sem munição!";
			}
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

		// Aplica velocidade
		Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
		rb.velocity = direction * bulletSpeed;

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

		// Calcula quanto ainda tem no estoque
		int ammoNeeded = magazineSize - currentAmmo; 
		int ammoToReload = Mathf.Min(ammoNeeded, totalAmmo);

		// Atualiza valores
		currentAmmo += ammoToReload;
		totalAmmo -= ammoToReload;

		// Reset da barra
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
