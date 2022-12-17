using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        Time.timeScale = 1f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        AudioManager.instance.Play("menuSound");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartGame()
    {
        SceneManager.LoadScene("Main");
        AudioManager.instance.Stop("menuSound");
    }
    public void ExitGAme()
    {
        Application.Quit();
    }
    public void SaveWeapon()
    {
        PlayerPrefs.SetInt("gunSelection", GunSelection.instance.myGunId);
        PlayerPrefs.SetInt("knifeSelection", KnifeSelection.instance.myKnifeId);
    }
    public void GetCurrentWeapon()
    {
        int gunId = PlayerPrefs.GetInt("gunSelection", 0);
        int knifeID = PlayerPrefs.GetInt("knifeSelection", 0);
        GameLoading.instance.knifeBackground.sprite = GameLoading.instance.knifeImg[knifeID];
        GameLoading.instance.gunBackground.sprite = GameLoading.instance.gunImg[gunId];
        GameLoading.instance.knifeName.text = GameLoading.instance.knifeImg[knifeID].name;
        GameLoading.instance.gunName.text = GameLoading.instance.gunImg[gunId].name;
    }
}
