using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;

    public float fireRate;
    public float maxAmmo = 8;
    public float currentAmmo;

    public float reloadTime = 1.5f;
    public float bulletForce = 10f;
    private float lastTimeShoot = Mathf.NegativeInfinity;

    private void Awake()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            CanFire();
        } 
        else if (Input.GetButton("Fire1") && currentAmmo <= 1)
        {
            StartCoroutine(Reload());
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }

    private void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(bulletSpawnPoint.forward * bulletForce, ForceMode.Impulse);
        lastTimeShoot = Time.time;
    }

    private bool CanFire()
    {
        if (lastTimeShoot + fireRate < Time.time){
            if (currentAmmo >= 1)
            {
                Fire();
                currentAmmo--;
                return true;
            }
        }
        return false;
    }

    IEnumerator Reload()
    {
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        Debug.Log("Reloaded");
    }
}
