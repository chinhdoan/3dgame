using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class KnifeSelection : MonoBehaviour
{
    [HideInInspector]
    public int myKnifeId;

    private void Awake()
    {
        myKnifeId = PlayerPrefs.GetInt("knifeSelection");
        GameLoading.instance.knifeBackground.sprite = GameLoading.instance.knifeImg[myKnifeId];
        GameLoading.instance.knifeName.text = GameLoading.instance.knifeImg[myKnifeId].name;
    }
    private void Start()
    {
    }

    private void Update()
    {
       
    }
    public void nextSelection()
    {
        myKnifeId++;
        Debug.Log(myKnifeId);
        if (myKnifeId >= GameLoading.instance.knifeImg.Length)
        {
            myKnifeId = 0;
            PlayerPrefs.SetInt("knifeSelection", 0);
        }
        setKnifeIndex();
    }
    public void prevSelection()
    {
        GameLoading.instance.knifeBackground.sprite = null;
        Debug.Log(myKnifeId);
        if (myKnifeId > 1)
        {
           myKnifeId--;
        }
        if (myKnifeId <= -1)
        {
            GameLoading.instance.knifeBackground.sprite = GameLoading.instance.knifeImg[myKnifeId];
        }
        setKnifeIndex();
    }
    void setKnifeIndex()
    {
        PlayerPrefs.SetInt("knifeSelection", myKnifeId);
        GameLoading.instance.knifeBackground.sprite = GameLoading.instance.knifeImg[myKnifeId];
        GameLoading.instance.knifeName.text = GameLoading.instance.knifeImg[myKnifeId].name;
    }
}
