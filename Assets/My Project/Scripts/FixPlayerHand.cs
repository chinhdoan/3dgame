using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixPlayerHand : MonoBehaviour
{
    [SerializeField] Transform mainCam;

    private void dUpdate()
    {
        transform.rotation = mainCam.rotation;
    }

}
