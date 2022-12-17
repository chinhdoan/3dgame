using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollider : MonoBehaviour
{
    [SerializeField] CapsuleCollider cc;
    // Start is called before the first frame update
    private void Update()
    {
        if (SaveSscript.WeaponID == 0) {
            cc.radius = 21.21f;
            cc.center = new Vector3(5.5f,-15.2f,4.055f);
            cc.height = 53.5f;
        }
        if (SaveSscript.WeaponID == 1)
        {
            cc.height = 53.5f;
            cc.center = new Vector3(5.5f, -15.2f, 4.055f);
            cc.radius = 19.07f;
        }
    }
   
}
