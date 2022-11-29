using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class neckfollow : MonoBehaviour
{
    [SerializeField] Transform orientation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = orientation.position;
    }
}
