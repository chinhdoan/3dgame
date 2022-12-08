using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform bulletSpawn;
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;
        transform.rotation = Quaternion.Euler(0,0,0); //Fix Crosshair Rotaion
    }
}
