using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeAnimation : MonoBehaviour
{

    [SerializeField] Animator anim;
    [SerializeField] Transform handPos;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float adInput = Input.GetAxis("Horizontal");
        float wsInput = Input.GetAxis("Vertical");
        if (adInput > 0 || adInput < 0 || wsInput < 0 || wsInput > 0)
        {
           // anim.SetBool("slash", false);

        }
        if (adInput == 0 || adInput == 0 || wsInput == 0 || wsInput == 0)
        {
            anim.SetBool("slash", false);
        }
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("slash", true);
        }
        if (Input.GetMouseButton(0)) {
            anim.SetBool("combo", true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            anim.SetBool("slash", false);
            anim.SetBool("combo", false);
        }
        if (Input.GetMouseButtonDown(1))
        {
            anim.SetBool("slash", false);
            anim.SetTrigger("bigshot");

        }


        //SwitchHand
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

    }
}
