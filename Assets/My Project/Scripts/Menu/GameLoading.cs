using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameLoading : MonoBehaviour
{
    public TextMeshProUGUI yourName;
    public TextMeshProUGUI yourClan;


    [Header("Input Place")]
    [SerializeField] GameObject firstTimePanel;
    [SerializeField] Transform root;

    [Header("Panel")]
    [SerializeField] GameObject Body;
    [SerializeField] GameObject mapPanel;
    [SerializeField] GameObject buttonPanel;
    [SerializeField] GameObject characterPanel;
    [SerializeField] GameObject settingPanel;


    [Header("Weapon")]
    public GameObject[] myGun;
    [Header("Knife")]
    public GameObject[] myKnife;



    [Header("My Weapon")]
    [SerializeField] GameObject[] model;
    [SerializeField] TMP_Text[] weaponNameTxt;

    public static GameLoading instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != null) {
            Destroy(this);
        }
        setName();
    }
    private void Start()
    {   
        if (yourName.text == "" && yourClan.text == "")
        {
            mapPanel.SetActive(false);
            buttonPanel.SetActive(false);
            characterPanel.SetActive(false);
            Instantiate(firstTimePanel, root);
        }
    }
    private void Update()
    {
        setName();
    }

    void setName() {
        var playerName = PlayerPrefs.GetString("PlayerName");
        var clan = PlayerPrefs.GetString("Clan");
        if (playerName != null && clan != null)
        {
            yourName.text = playerName;
            yourClan.text = clan;
        }
    }
    void setWeapon() {
       /* var rifeWeapon = PlayerPrefs.GetString("PlayerName");
        var clan = PlayerPrefs.GetString("Clan");
        if (playerName != null && clan != null)
        {
            yourName.text = playerName;
            yourClan.text = clan;
        }*/

    }
}
