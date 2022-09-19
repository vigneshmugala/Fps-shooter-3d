using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GunScript : MonoBehaviour
{
    public Camera fpsCam;
    public float range = 100f;
    public float damage = 10f;
    public GameObject gunFlare;
    public ParticleSystem muzzleFlash;
    //public GameObject muzzFlash;
    public Transform gunTip;
    public Animator anim;
    public TextMeshProUGUI reloadText;
    public float force = 35f;
    public float fireRate = 20f;
    private float nextTimetoFire = 0f;

    public int currentAmmo;
    public int maxAmmo = 10;

    public float reloadTime = 1.5f;
    private bool isReloading;
    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void OnEnable()
    {
        isReloading = false;
        currentAmmo = maxAmmo;
        if (reloadText.IsActive())
            reloadText.gameObject.SetActive(false);
        anim.SetBool("Reloading", false);
    }


    // Update is called once per frame
    void Update()
    {
        if (isReloading) return;

        if(currentAmmo <= 0f)
        {
            reloadText.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.R))
            {
                reloadText.gameObject.SetActive(false);
                StartCoroutine(Reload());
                return;
            }
        }


        if(Input.GetButton("Fire1") && Time.time > nextTimetoFire)
        {
            nextTimetoFire = Time.time + 1f / fireRate;
          if(currentAmmo > 0f)
            Shoot();
        }
    }


    IEnumerator Reload()
    {
        isReloading = true ;

        anim.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - 0.25f);

        currentAmmo = maxAmmo;

        yield return new WaitForSeconds(0.25f);

        anim.SetBool("Reloading", false);

        isReloading = false;
    }


    void Shoot()
    {
        //muzzleFlash.Play();
        //ParticleSystem flash = Instantiate(muzzleFlash, gunTip);
        muzzleFlash.Play();
        currentAmmo--;
        //Destroy(flash);
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Target t = hit.transform.GetComponent<Target>();
            if(t != null)
            {
                t.TakeDamage(damage);
            }
            
            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * force);
            }

            GameObject impactObj = Instantiate(gunFlare, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactObj, 2f);
        }


    }
}
