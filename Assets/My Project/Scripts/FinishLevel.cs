using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FinishLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.instance.isFinished == true) {
            GameManager.instance.winName.text = GameLoading.instance.yourName.text;
        }
    }
}
