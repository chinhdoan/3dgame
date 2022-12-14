using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class FirstTimePopUp : MonoBehaviour
{

    [Header("Set Info")]
    [SerializeField] TMP_InputField setNameTxt;
    [SerializeField] TMP_InputField setClanTxt;
    [SerializeField] TMP_Text infoTxt;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(SetName);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetName()
    {
        if (setNameTxt.text == "" || setClanTxt.text == "")
        {
            infoTxt.text = "Player or clan name can not blank!";
        }
        else if (setNameTxt.text.Length > 50 && setClanTxt.text.Length > 50)
        {
            infoTxt.text = "Player or clan name can not set over 50 characters!";
        }
        else {
            PlayerPrefs.SetString("PlayerName", setNameTxt.text);
            PlayerPrefs.SetString("Clan", setClanTxt.text);
            Destroy(gameObject);
            SceneManager.LoadScene("Menu");
        }
    }
}
