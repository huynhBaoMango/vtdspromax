using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    bool isShooting = false;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public GameObject muzzleFlash;
    public float bulletSpeed = 10f;
    private PlayerManager pmanager;
    private float fireReset;
    public TextMeshProUGUI bulletCountText;    
    private Coroutine reloadingCoroutine;
    public Image gunImage;
    public Slider reloadSlider;
    
    void Start()
    {
        pmanager = GetComponent<PlayerManager>();
        reloadSlider.gameObject.SetActive(false);
        fireReset = pmanager.fireReset;
        if (muzzleFlash)
        {
            muzzleFlash.SetActive(false);
        }
        UpdateBulletCountText();
    }

    void Update()
    {
        if (!pmanager.isDead)
        {
            StartShooting();
        }

    }

    void StartShooting()
    {
        isShooting = true;
        fireReset -= 1 * Time.deltaTime;
        
        if (pmanager.currentBulletCount > 0 && fireReset <= 0)
        {
            StartCoroutine(ShootBullets());
            fireReset = pmanager.fireReset;
        }
    }

    IEnumerator ShootBullets()
    {
       
        for (int i = 1; i <= pmanager.fireRate; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<bulletInfo>().damage = pmanager.damage;
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            muzzleFlash.SetActive(true);
            FindAnyObjectByType<AudioManager>().Play("Shoot");

            if (bulletRb)
            {
                bulletRb.velocity = firePoint.forward * bulletSpeed;
            }

            muzzleFlash.SetActive(false);

            
            yield return new WaitForSeconds(0.15f);
        }
        pmanager.DecreaseCurrentBulletCount(1);
        UpdateBulletCountText();
        if (pmanager.currentBulletCount == 0)
        {
            Reload();
            FindAnyObjectByType<AudioManager>().PlayButWait("reload");
        }
        else
        {
            FindAnyObjectByType<AudioManager>().Stop("reload");
        }


    }

    IEnumerator ReloadBullets()
    {
        gunImage.color = new Color(1f, 1f, 1f, 1f); 
        reloadSlider.gameObject.SetActive(true); 

        float reloadTime = pmanager.reloadSpeed;
        while (reloadTime > 0)
        {
            reloadTime -= Time.deltaTime;
            reloadSlider.value = 1 - (reloadTime / pmanager.reloadSpeed);
            yield return null;
        }

   
        pmanager.currentBulletCount = pmanager.maxBulletCount;
        UpdateBulletCountText();
        gunImage.color = Color.white; 
        reloadSlider.gameObject.SetActive(false); 

        reloadingCoroutine = null;
    }


    void UpdateBulletCountText()
    {
        bulletCountText.text = pmanager.currentBulletCount + "/" + pmanager.maxBulletCount;
    }

    public void Reload()
    {
        if (reloadingCoroutine == null)
        {
            reloadingCoroutine = StartCoroutine(ReloadBullets());
        }
    }
}
