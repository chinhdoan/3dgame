using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("GunBullet")]
    [SerializeField] Transform muzzleSpawn;
    [SerializeField] Transform bulletSpawn;
    [SerializeField] GameObject gunFlash;
    [SerializeField] GameObject impactWall;
    [SerializeField] GameObject gunBullet;


    public float bulletSpeed = 500f;
    float rotateDirection;
    RaycastHit hit;

    [Header("Shooting")]
    float delayTime = 0.5f;
    float baseAKDeplayTime= 0.1f;


    // Start is called before the first frame update
    void Start()
    {
       // playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
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

    void shoot() {
        Instantiate(gunFlash, muzzleSpawn.position, muzzleSpawn.rotation);
        GameObject bullet = Instantiate(gunBullet, bulletSpawn.position, bulletSpawn.rotation);
        Instantiate(gunBullet, Input.mousePosition, muzzleSpawn.rotation);
        rotateDirection = Input.GetAxis("Mouse X");

        if (rotateDirection == 0 || rotateDirection < 0 || rotateDirection > 0)
        {
            bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward * 10f * bulletSpeed, ForceMode.Impulse);
        }
        Hits();
        /*      playerAudio.clip = singleShootSound;
              playerAudio.Play();*/
    }

    public void Hits() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000)) {
            if (hit.transform.tag == "Wall") {
                Instantiate(impactWall, hit.point, Quaternion.identity);
            }
        }
    }




}
