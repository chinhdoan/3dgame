using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MapSelection : MonoBehaviour
{


    public GameObject[] skins;
    public int charSelected;
    public TextMeshProUGUI numCoins;
    public TextMeshProUGUI status;
    int currentCoins;
    private void Awake()
    {

        currentCoins = PlayerPrefs.GetInt("coins");
        numCoins.text = PlayerPrefs.GetInt("coins").ToString();

        if (currentCoins < 999)
        {
            status.text = "X Coins do not enough to buy new characters X";

        }
        else if (currentCoins > 999)
        {
            status.text = "                                      Great! ";
        }
        charSelected = PlayerPrefs.GetInt("characterSelection", 0);

        foreach (GameObject skin in skins)
        {
            skin.SetActive(false);
        }

        skins[charSelected].SetActive(true);
    }
    public void nextSelection()
    {

        skins[charSelected].SetActive(false);
        charSelected++;
        if (charSelected >= skins.Length)
        {
            charSelected = 0;
        }
        if (currentCoins > 999)
        {
            currentCoins = currentCoins - 999;
            PlayerPrefs.SetInt("coins", currentCoins);
            PlayerPrefs.SetInt("characterSelection", charSelected);
            numCoins.text = currentCoins.ToString();
            skins[charSelected].SetActive(true);
        }
        else if (charSelected >= 4)
        {
            PlayerPrefs.SetInt("characterSelection", 0);
        }



    }
    public void prevSelection()
    {
        skins[charSelected].SetActive(false);
        if (charSelected >= 1)
        {
            charSelected--;
        }
        if (charSelected < -1)
        {
            PlayerPrefs.SetInt("characterSelection", skins.Length - 1);
            Debug.Log(charSelected);
        }
        if (currentCoins > 999)
        {
            PlayerPrefs.SetInt("characterSelection", charSelected);
            currentCoins = currentCoins - 999;
            PlayerPrefs.SetInt("coins", currentCoins);
            numCoins.text = currentCoins.ToString();
            skins[charSelected].SetActive(true);
        }
        Debug.Log(charSelected);
    }
}
