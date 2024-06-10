using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    bool isShooting = false;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public GameObject muzzleFlash;
    public float bulletSpeed = 10f;
    private PlayerManager pmanager;
    private float fireReset;


    void Start()
    {
        pmanager = GetComponent<PlayerManager>();

        fireReset = pmanager.fireReset;
        if (muzzleFlash)
        {
            muzzleFlash.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        StartShooting();
        
    }

    void StartShooting()
    {
        isShooting = true;
        fireReset -= 1 * Time.deltaTime;
        if (bulletPrefab && firePoint && fireReset <= 0)
        {
            StartCoroutine(ShootBullets());
            fireReset = pmanager.fireReset;
        }

    }

    IEnumerator ShootBullets()
    {
        for (int i = 0; i < pmanager.fireRate; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<bulletInfo>().damage = pmanager.damage;
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            muzzleFlash.SetActive(true);

            if (bulletRb)
            {
                bulletRb.velocity = firePoint.forward * bulletSpeed;
            }

            yield return new WaitForSeconds(0.15f);
        }

        muzzleFlash.SetActive(false); // Tắt muzzle flash sau khi bắn xong
    }
}
