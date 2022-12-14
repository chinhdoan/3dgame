using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Threading;

public class GameManager : MonoBehaviour
{

    public GameObject finishPanel;
    public GameObject map, oldMap;
    public Transform mapSpawn;
    DestroyCurrentMap delCurentMap = new DestroyCurrentMap();

    public Animator anim;
    public AnimationClip WinAnimationClip;

    [Header("Player Name")]
    public TMP_Text winName;

    [Header("Back Menu")]
    public GameObject backPanel;
    bool isActive;
    int count;


    public static GameManager instance;
    // Start is called before the first frame update
    private void Awake()
    {
        AudioManager.instance.Play("enviromentSound");
    }
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        WinAnimationClip.AddEvent(new AnimationEvent()
        {
            time = WinAnimationClip.length,
            functionName = "ExitGame"
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            count += 1;
            if (backPanel.activeSelf == false) {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                backPanel.SetActive(true);
                isActive = true;
            }
          
            if (count == 2)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                backPanel.SetActive(false);
                count = 0;
                isActive = false;
            }

        }
        if (isActive == true) {
            if (Input.GetKeyDown(KeyCode.KeypadEnter)) {
                SceneManager.LoadScene("Menu");
            }
        }

    }
    public void ActiveFinishPanel() {
        if (finishPanel != null ) {
            Debug.Log("Hello");
            finishPanel.SetActive(true);
            Time.timeScale = 0.1f;
            CameraManager.instance.senX = 0.00001f;
            CameraManager.instance.senY = 0.00001f;
        }
    }
    public void ExitGame()
    {

        anim.SetBool("isRunning",false);
    }

    public void LoadNextMap() {
        var getMap = GameObject.FindWithTag("Step");
        Instantiate(map, mapSpawn.position, map.transform.rotation);
        if (getMap != null)
        {
            delCurentMap.delMap(getMap);
        }


    }
    public void Back()
    {
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
