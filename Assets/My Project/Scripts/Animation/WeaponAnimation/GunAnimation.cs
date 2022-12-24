using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Threading;
public class GunAnimation : MonoBehaviour
{
    public Animator anim;
    public AnimationClip reloadClip;
    public bool isReloading;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update    
    void Start()
    {

    }
 
    // Update is called once per frame
    void Update()
    {
        float adInput = Input.GetAxis("Horizontal");
        float wsInput = Input.GetAxis("Vertical");
        if (adInput > 0 || adInput < 0 || wsInput < 0 || wsInput > 0) {
            anim.SetBool("fire", false);
        }
        if (adInput == 0 || adInput == 0 || wsInput == 0 || wsInput == 0) {
            anim.SetBool("fire", false);
            anim.SetBool("reload", false);
        }
        if (Input.GetMouseButtonDown(0)|| Input.GetMouseButton(0)) {
            anim.SetBool("fire",true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            anim.SetBool("fire", false);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            anim.SetTrigger("reload");
        }
        if (SaveSscript.currentAmmor <= 0 && Input.GetMouseButtonUp(0))
        {
            anim.SetTrigger("reload");
        }
        //SwitchHand
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            transform.localScale =  new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
    private void FixedUpdate()
    {

    }

}
