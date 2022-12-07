using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFeet : MonoBehaviour
{
    float feetStayTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (feetStayTime > 0.01f)
        {
            PlayerManager.instance.isBhop = true;
        }
    }
  
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Box" || collision.gameObject.tag == "Ground")
        {
            feetStayTime += Time.deltaTime;
        }
    }
}
