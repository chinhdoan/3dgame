using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Damage")]
    public int knifeDamage = 50;

    [Header("GunBullet")]
    public int countBullet;
    [SerializeField] Transform muzzleSpawn, playerMuzzleSpawn;
    [SerializeField] Transform bulletSpawn, playerBulletSpawn;
    [SerializeField] GameObject gunFlash;
    [SerializeField] GameObject impactWall;
    [SerializeField] GameObject gunBullet;
    int tempCount;

    [Header("Reload")]
    [SerializeField] AnimationClip reloadAnim;
    bool isReloading;


    public float bulletSpeed = 500f;
    float rotateDirection;


    RaycastHit hit;
    [SerializeField] Camera cam;
    float range;
    Vector3 direction;
    public GameObject impactEffect;
    public GameObject bulletEffect;

    [Header("Shooting")]
    float delayTime = 0.5f;
    float baseAKDeplayTime = 0.1f;
    public bool isShooting;
    [SerializeField] float gunDamage;
    [SerializeField] GameObject bloodImpact;

    [Header("Spray")]
    public GameObject sprayImg;
    public Transform camRotaion;
    public Transform orientation;
    bool isSpraying = false;
    DestroySpray theSpray = new DestroySpray();


    [Header("Weapon")]
    public Transform playerHand;
    bool hasWeapon;
    public static PlayerShooting instance;
    GameObject tempWeapon;
    private int gunID;
    private int knifeID;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) {
            instance = this;
        }
        gunID = PlayerPrefs.GetInt("gunSelection");
        knifeID = PlayerPrefs.GetInt("knifeSelection");
    }

    // Update is called once per frame
    void Update()
    {

        direction = Vector3.forward;
        Debug.DrawRay(transform.position, transform.TransformDirection(direction * range));
        spray();
        //Gun SLot
        if (SaveSscript.WeaponID == 0) 
        {
            //only One
            if (!hasWeapon)
            {
                tempWeapon = Instantiate(GameLoading.instance.myGun[gunID], playerHand);
                hasWeapon = true;
            }

            if (SaveSscript.currentAmmor > 0) {
                //Shoot
                if (Input.GetMouseButtonDown(0))
                { 
                    shoot();
                    AudioManager.instance.Play("gunShootSound");
                    delayTime = 0.25f;
                }
                if (Input.GetMouseButton(0))
                {
                    delayTime -= Time.deltaTime;
                    Debug.Log(delayTime);
                    if (delayTime <= 0)
                    {
                        shoot();
                        AudioManager.instance.PlayOnce("gunShootSound");
                        delayTime = baseAKDeplayTime;
                    }
                }          
            }

            if (SaveSscript.currentAmmor <= 0 && Input.GetMouseButtonUp(0)) {
                StartCoroutine(reloadAmmor());
            }

            //Reload Bullet
            if (Input.GetKeyDown(KeyCode.R) && SaveSscript.currentAmmor < 30)
            {
                StartCoroutine(reloadAmmor());
            }
        }


        //Knife SLot
        if (SaveSscript.WeaponID == 1)
        { 
            //only One
            if (!hasWeapon)
            {
                tempWeapon = Instantiate(GameLoading.instance.myKnife[knifeID], playerHand);
                hasWeapon = true;
            }
           
        }
        {
            //only One
            if (!hasWeapon)
            {
                tempWeapon = Instantiate(GameLoading.instance.myKnife[knifeID], playerHand);
                hasWeapon = true;
            }

        }  
    }
    void spray() {
        if (Input.GetKeyDown(KeyCode.F)) {
            var getSpray = GameObject.FindWithTag("Spray");
            ImpactEffect(500f);
            if (getSpray != null)
            {
                theSpray.delSpray(getSpray);
            }
        }
    }

    void shoot()
    {
        SaveSscript.currentAmmor--;
        ImpactEffect(1000f);
        //Instantiate(gunFlash, muzzleSpawn.position, muzzleSpawn.rotation);
        GameObject bullet = Instantiate(gunBullet, bulletSpawn.position, bulletSpawn.rotation);

        rotateDirection = Input.GetAxis("Mouse X");

        if (rotateDirection == 0 || rotateDirection < 0 || rotateDirection > 0)
        {
            bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward * 10f * bulletSpeed, ForceMode.Impulse);
        }
    }

    public void ImpactEffect(float range)
    {
        direction = Vector3.forward;
        Ray theRay = new Ray(transform.position, transform.TransformDirection(direction * range));
        if (Physics.Raycast(theRay, out hit, range))
        {
            //Kill demon
            Target target = hit.transform.GetComponent<Target>();
            if (target != null) {
                target.TakeDamage(gunDamage);
                isReloading = false;
                Instantiate(bloodImpact, hit.point, Quaternion.LookRotation(hit.normal));
            }
           
        


             //Spray
            switch (range)
            {
                case (500f):
                    if (hit.collider.tag == "Wall")
                    {
                        float angle = camRotaion.eulerAngles.y;

                        Debug.Log("shoot");
                        Debug.Log(angle);

                        //forward= 0 & 360
                        if ((angle >= 0 && angle <= 1) || (angle <= 360 && angle >= 358))
                        {
                            Debug.Log("hit");
                            Instantiate(sprayImg, hit.point, Quaternion.Euler(-90, 0, 180));
                        }
                        //right = 90
                        if ((angle > 80 && angle < 90))
                        {
                            Debug.Log("hit");
                            Instantiate(sprayImg, hit.point, Quaternion.Euler(-90, 0, 180 + 90));
                        }
                        //left = - 90

                        if (angle < 280 && angle > 250)
                        {
                            Debug.Log("hit");
                            Instantiate(sprayImg, hit.point, Quaternion.Euler(-90, 0, -180 - 90));
                        }

                        //backward = 180

                        if (angle < 181 && angle > 177)
                        {
                            Debug.Log("hit");
                            Instantiate(sprayImg, hit.point, Quaternion.Euler(90, 0, 180 + 180));
                        }
                    }
                    else if (hit.collider.tag == "Box" || hit.collider.tag == "Ground") {
                        if (camRotaion.eulerAngles.x > 80)
                        {
                            Instantiate(sprayImg, hit.point, Quaternion.Euler(0, camRotaion.eulerAngles.y, 180));
                        }


                    }
                    break;
                case (1000f):
                    Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Instantiate(bulletEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    break;
            }
        }
    }
    public void delWeapon()
    {
        Destroy(tempWeapon);
        hasWeapon = false;
    }
    IEnumerator reloadAmmor()
    {
        if (isReloading == false)
        {
            AudioManager.instance.PlayOnce("akClipIn");
            yield return new WaitForSeconds(AudioManager.instance.clipLength);
            AudioManager.instance.PlayOnce("akReloadSound");
            SaveSscript.currentAmmor = 30;
            isReloading = true;
        }
    }
}
