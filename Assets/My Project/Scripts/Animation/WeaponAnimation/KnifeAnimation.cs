using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeAnimation : MonoBehaviour
{

    [SerializeField] Animator anim;
    [SerializeField] AnimationClip bigshot,comboA,comboB;
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
            StartCoroutine(slashAttk());
        }
        if (Input.GetMouseButton(0) && anim.GetBool("slash") == false) {
            anim.SetBool("combo", true);
            StartCoroutine(combo());
        }
        if (anim.GetBool("slash") == false || anim.GetBool("combo") == false ) {
            StopAllCoroutines();
        }
        if (Input.GetMouseButtonUp(0))
        {
            anim.SetBool("slash", false);
            anim.SetBool("combo", false);
  
        }
        if (Input.GetMouseButtonDown(1))
        {
            anim.SetBool("slash", false);
            anim.SetTrigger("bigShot");
            AudioManager.instance.PlayOnce("knifeBigShot");
        }


        //SwitchHand
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

    }
    IEnumerator slashAttk() {
        AudioManager.instance.Play("knifeShoot");
        yield return new WaitForSeconds(3f);
        AudioManager.instance.Play("knifeShootA");
    }

    IEnumerator combo()
    {
        yield return new WaitForSeconds(2f);
        AudioManager.instance.Play("knifeShootB");
    }
}
