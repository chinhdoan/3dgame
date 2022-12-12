using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapSelection : MonoBehaviour
{

    [SerializeField] Image mapBackGround;
    [SerializeField] Sprite[] mapImg;
    public int mapSelected;
    [SerializeField] TextMeshProUGUI mapName;
    private void Awake()
    {
        mapSelected = PlayerPrefs.GetInt("mapSelection", 0);
        mapBackGround.sprite = mapImg[mapSelected];
        mapName.text = mapImg[mapSelected].name;
    }
    public void nextSelection()
    {
       
        mapSelected++;
        Debug.Log(mapSelected);
        if (mapSelected >= mapImg.Length)
        {
            mapSelected = 0;
            setMapIndex();
        }
        setMapIndex();
    }
    public void prevSelection()
    {
        
        Debug.Log(mapSelected);
        if (mapSelected >= 1)
        {
            mapSelected--;
        }
        if (mapSelected <= -1)
        {
            PlayerPrefs.SetInt("mapSelection", mapImg.Length -1);
            mapBackGround.sprite = mapImg[mapSelected];
        }
        setMapIndex();
    }
    void setMapIndex() {
        PlayerPrefs.SetInt("mapSelection", mapSelected);
        mapBackGround.sprite = mapImg[mapSelected];
        mapName.text = mapImg[mapSelected].name;
    }
}
