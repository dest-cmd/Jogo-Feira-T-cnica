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
    public int magazineSize = 10;
    public int totalAmmo = 50;
    private int currentAmmo;
    public float reloadTime = 2f;
    private bool isReloading = false;

    [Header("UI")]
    public Text totalAmmoText;
    public Image reloadBar;
    public Text reloadBarText;

    private float nextFireTime = 0f;

    void Start()
    {
        currentAmmo = magazineSize;
        reloadBar.fillAmount = 0f;
        reloadBarText.text = currentAmmo + " / " + magazineSize;
        UpdateTotalAmmoUI();
    }

    void Update()
    {
        if (isReloading) return;

        if (currentAmmo <= 0)
        {
            if (totalAmmo > 0)
                StartCoroutine(Reload());
            else
                reloadBarText.text = "Sem munição!";
            return;
        }

        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        // pegamos a direção para frente do firePoint
        // supondo que o firePoint “aponta” para frente no seu eixo local Y
        Vector2 direction = firePoint.up;

        // Instancia a bala no firePoint com rotação igual ao firePoint
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Passa direção para o script da bala
        Bullet b = bullet.GetComponent<Bullet>();
        if (b != null)
        {
            b.SetDirection(direction);
        }

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
