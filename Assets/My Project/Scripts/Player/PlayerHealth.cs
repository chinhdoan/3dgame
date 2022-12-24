using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float baseTimer=2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SaveSscript.health > 0)
        {
            if (PlayerManager.instance.isDeathZone == true) { 
                 baseTimer -= Time.deltaTime;
                if (baseTimer <= 0) {
                    SaveSscript.health -= 25f;
                    AudioManager.instance.Play("playerHurtSound");
                    baseTimer = 1.2f;
                }
            }
           
        }
        if (SaveSscript.health <= 0) {
            GameManager.instance.setActiveLosePanel();
        }
    }

   
}
