using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraFollow : MonoBehaviour
{
    [SerializeField] Transform camPos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        this.transform.rotation = Quaternion.Euler(camPos.eulerAngles.x, camPos.eulerAngles.y, 0);
    }
}
