using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] Transform muzzleSpawn;
    [SerializeField] Transform bulletSpawn;
    [SerializeField] GameObject gunFlash;
    [SerializeField] GameObject impactWall;
    [SerializeField] GameObject gunBullet;
    [SerializeField] AudioClip singleShootSound;
    private AudioSource playerAudio;
    public float bulletSpeed = 500f;
    float rotateDirection, rotateXDirection;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0)) {
            Instantiate(gunFlash, muzzleSpawn.position, muzzleSpawn.rotation);
            GameObject bullet = Instantiate(gunBullet, bulletSpawn.position, muzzleSpawn.rotation);
            rotateDirection = Input.GetAxis("Mouse X");
            rotateXDirection = Input.GetAxis("Mouse Y");

            if (rotateDirection == 0 || rotateDirection < 0 || rotateDirection > 0 )
            {
                bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward * 10f * -bulletSpeed, ForceMode.Impulse);
            }
           

            playerAudio.clip = singleShootSound;
            playerAudio.Play();

            Hits();
        }
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
