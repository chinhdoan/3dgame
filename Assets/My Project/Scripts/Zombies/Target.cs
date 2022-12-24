using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float demonHealth = 1000f;
    public GameObject self;
    public Animator anim;
    public AnimationClip reaction,die;
    public static Target instance;
    float currentHealth ;
    float newHealth;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        anim = gameObject.GetComponent<Animator>();
        currentHealth = demonHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (demonHealth < currentHealth) {
            StartCoroutine(react());
        }
    }
    public void TakeDamage(float damage) {
        demonHealth -= damage;
        anim.SetTrigger("isShoot");
        currentHealth = demonHealth;
        if (demonHealth <= 0 ) {
            GameManager.instance.showKillMark();
            StartCoroutine(destroyObj());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "knife") {
            AudioManager.instance.Play("knifeHitPlayer");
            TakeDamage(PlayerShooting.instance.knifeDamage);
        }
    }

    IEnumerator destroyObj() {
        anim.SetBool("die", true);
        yield return new WaitForSeconds(die.length);
        anim.SetBool("die", false);
        Destroy(gameObject, 0.5f);

    }
    IEnumerator react()
    {
        //anim.SetBool("isShooted", true);
        yield return new WaitForSeconds(reaction.length);
        //anim.SetBool("isShooted", false);
    }
}
