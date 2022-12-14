using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMuzzle : MonoBehaviour
{
    public GameObject gunMuzzle;
    public Transform muzzleSpawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            Instantiate(gunMuzzle, muzzleSpawn.position, muzzleSpawn.rotation);

        }
    }

}
