using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GunSelection : MonoBehaviour
{

    [HideInInspector]
    public int myGunId;

    public static GunSelection instance;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("gunSelection"))
        {
            myGunId = PlayerPrefs.GetInt("gunSelection", 0);
            GameLoading.instance.gunBackground.sprite = GameLoading.instance.gunImg[myGunId];
            GameLoading.instance.gunName.text = GameLoading.instance.gunImg[myGunId].name;

        }
        else
        {
            PlayerPrefs.SetInt("gunSelection", 0);
            GameLoading.instance.gunBackground.sprite = GameLoading.instance.gunImg[0];
            GameLoading.instance.gunName.text = GameLoading.instance.gunImg[0].name;
        }
    }

    public void Start()
    {
        if (instance == null)
        {
            instance = this;
           
        }
    }
    public void nextSelection()
    {
        myGunId++;
        Debug.Log(myGunId);
        if (myGunId >= GameLoading.instance.gunImg.Length)
        {
            myGunId = 0;
            setgunIndex();
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
        GameLoading.instance.gunBackground.sprite = GameLoading.instance.gunImg[myGunId];
        GameLoading.instance.gunName.text = GameLoading.instance.gunImg[myGunId].name;
    }
}
