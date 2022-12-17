using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class KnifeSelection : MonoBehaviour
{
    [HideInInspector]
    public int myKnifeId;



    public static KnifeSelection instance;
    private void Awake()
    {
        if (PlayerPrefs.HasKey("KnifeSelection"))
        {
            myKnifeId = PlayerPrefs.GetInt("knifeSelection", 0);
            GameLoading.instance.knifeBackground.sprite = GameLoading.instance.knifeImg[myKnifeId];
            GameLoading.instance.knifeName.text = GameLoading.instance.knifeImg[myKnifeId].name;
        }
        else
        {
            PlayerPrefs.SetInt("knifeSelection", 0);
            GameLoading.instance.knifeBackground.sprite = GameLoading.instance.knifeImg[0];
            GameLoading.instance.knifeName.text = GameLoading.instance.knifeImg[0].name;
        }
    }

    public void Start()
    {
        if (instance == null) {
            instance = this;
         
        }
    }
    public void nextSelection()
    {
        GameLoading.instance.knifeBackground.sprite = null;
        myKnifeId++;
        Debug.Log(myKnifeId);
        if (myKnifeId >= GameLoading.instance.knifeImg.Length)
        {
            myKnifeId = 0;
            setgunIndex();
        }
        setgunIndex();
    }
    public void prevSelection()
    {
        GameLoading.instance.knifeBackground.sprite = null;
        Debug.Log(myKnifeId);
        if (myKnifeId >= 1)
        {
            myKnifeId--;
        }
        if (myKnifeId <= -1)
        {
            GameLoading.instance.knifeBackground.sprite = GameLoading.instance.knifeImg[myKnifeId];
        }
        setgunIndex();
    }
    void setgunIndex()
    {
        GameLoading.instance.knifeBackground.sprite = GameLoading.instance.knifeImg[myKnifeId];
        GameLoading.instance.knifeName.text = GameLoading.instance.knifeImg[myKnifeId].name;
    }
}
