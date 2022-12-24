using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerBaseInfo : MonoBehaviour
{
    [SerializeField] TMP_Text healthTxt;
    [SerializeField] TMP_Text currentAmmorTxt;
    [SerializeField] TMP_Text baseAmmorTxt;
    public GameObject gunPanel,knifePanel,ammoPanel;

    public static PlayerBaseInfo instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        healthTxt.text = SaveSscript.health.ToString();
        currentAmmorTxt.text = SaveSscript.currentAmmor.ToString();
        baseAmmorTxt.text = SaveSscript.ammor.ToString();


        //Gun Icon
        if (SaveSscript.WeaponID == 0) {
            knifePanel.SetActive(false);
            ammoPanel.SetActive(true);
            gunPanel.SetActive(true);
        }
        //Knife Icon
        if (SaveSscript.WeaponID == 1)
        {
            ammoPanel.SetActive(false);
            gunPanel.SetActive(false);
            knifePanel.SetActive(true);
        }
    }
}
