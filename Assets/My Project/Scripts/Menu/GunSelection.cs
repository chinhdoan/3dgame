using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GunSelection : MonoBehaviour
{

    [HideInInspector]
    public int myGunId;

    private void Awake()
    {
        myGunId = PlayerPrefs.GetInt("gunSelection");
        GameLoading.instance.gunBackground.sprite = GameLoading.instance.gunImg[myGunId];
        GameLoading.instance.gunName.text = GameLoading.instance.gunImg[myGunId].name;
    }
    private void Start()
    {
    }
    public void nextSelection()
    {
        myGunId++;
        Debug.Log(myGunId);
        if (myGunId >= GameLoading.instance.gunImg.Length)
        {
            myGunId = 0;
            PlayerPrefs.SetInt("gunSelection", 0);
        } 
        setgunIndex();
    }
    public void prevSelection()
    {
        Debug.Log(myGunId);
        if (myGunId >= 1)
        {
            myGunId--;
        }
        if (myGunId <= -1)
        {
            GameLoading.instance.gunBackground.sprite = GameLoading.instance.gunImg[myGunId];
        }
        setgunIndex();
    }
    void setgunIndex()
    {
        PlayerPrefs.SetInt("gunSelection", myGunId);
        GameLoading.instance.gunBackground.sprite = GameLoading.instance.gunImg[myGunId];
        GameLoading.instance.gunName.text = GameLoading.instance.gunImg[myGunId].name;
    }
}
