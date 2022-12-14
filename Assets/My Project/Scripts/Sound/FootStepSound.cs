using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepSound : MonoBehaviour
{
    [Header("FootStep Sound")]
    [SerializeField] private float baseStepSpeed = 0.5f;
    [SerializeField] private float crouchStepMultipler = 1.5f;
    [SerializeField] private float sprintStepMultipler = 0.6f;
    [SerializeField] private AudioSource footStepAudioSource;
    [SerializeField] AudioClip[] metalClips;
    [SerializeField] AudioClip[] woodClips;
    private float footStepTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("testtt" + PlayerManager.instance.isGround);
        if (PlayerManager.instance.isGround == true) {
            if (PlayerManager.instance.isMoving == true)
            {
                HandleSound();
            }
           
        }
        if (PlayerManager.instance.isGround == false)
            HandleSound();
    }

    public void HandleSound()
    {
        if (!PlayerManager.instance.isGround) return;
        footStepTimer -= Time.deltaTime;
        if (footStepTimer <= 0)
        {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 3))
            {
                switch (hit.collider.tag)
                {
                    case "Ground":
                        footStepAudioSource.PlayOneShot(metalClips[Random.Range(0, metalClips.Length - 1)]);
                        break;
                }
            }
            footStepTimer = baseStepSpeed * 0.6f;
        }
    }
}
