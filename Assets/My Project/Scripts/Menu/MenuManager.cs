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
        AudioManager.instance.Stop("menuSound");
        AudioManager.instance.Play("playBtnSound");
        SceneManager.LoadScene("Main");
    }
    public void ExitGAme()
    {
        Application.Quit();
    }
    public void SaveWeapon()
    {
    }
}
