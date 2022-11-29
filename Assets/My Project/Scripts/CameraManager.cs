using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Animator anim;
    [SerializeField] Transform camPos;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = camPos.position;

        if (Input.GetMouseButtonDown(1)) {
            anim.SetBool("aimOrientation", true);
        }
        if (Input.GetMouseButtonUp(1))
        {
            anim.SetBool("aimOrientation", false);
        }


    }
}
