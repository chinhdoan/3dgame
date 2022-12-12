using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    public GameObject finishPanel;

    public Animator anim;
    public AnimationClip WinAnimationClip;


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
}
