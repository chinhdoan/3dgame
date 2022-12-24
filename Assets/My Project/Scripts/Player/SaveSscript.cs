using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSscript : MonoBehaviour
{
    //0-AK, 1-Knife
    public static int WeaponID = 0;
    public static float health;
    public static int ammor;
    public static int currentAmmor;
    int currentWeaponID;
    public bool isGun, isKnife;


    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        health = 100f;
        currentAmmor = 30;
        ammor = 30;
    }

    // Update is called once per frame
    void Update()
    {
        SwitchGun();
    }
    void SwitchGun() {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            stopSound();
            AudioManager.instance.Play("akClipIn");
            isKnife = false;
            PlayerShooting.instance.delWeapon();
            WeaponID = 0;
            currentWeaponID = WeaponID;
            isGun = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            stopSound();
            AudioManager.instance.Play("knifeSwitchSound");
            isGun = false;
            PlayerShooting.instance.delWeapon();
            WeaponID = 1;
            currentWeaponID = WeaponID;
            isKnife = true;
        }

        //Switch Weapon
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayerShooting.instance.delWeapon();
            stopSound();
            //DEfault
            if (isKnife == true)
            {
                AudioManager.instance.Play("akClipIn");
                Debug.Log("Gun");
                WeaponID = 0;
                currentWeaponID = WeaponID;
                isGun = true;
                isKnife = false;
            }
            else if (isKnife == false && isGun == true)
            {
                AudioManager.instance.Play("knifeSwitchSound");
                Debug.Log("Knife");
                WeaponID = 1;
                currentWeaponID = WeaponID;
                isGun = false;
                isKnife = true;
            }
        }
    }
    void stopSound()
    {
        AudioManager.instance.Stop("knifeSwitchSound");
        AudioManager.instance.Stop("akClipOut");
    }
}
