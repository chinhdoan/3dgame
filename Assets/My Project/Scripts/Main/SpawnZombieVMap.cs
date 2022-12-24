using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZombieVMap : MonoBehaviour
{
    [SerializeField] Transform[] demonSpawnPos;
    [SerializeField] GameObject[] demon;
    int mapIndex;
    // Start is called before the first frame update
    private void Awake()
    {
        mapIndex = PlayerPrefs.GetInt("mapSelection", 0);
    }
    void Start()
    {
        spawnDemon();
    }

    // Update is called once per frame
    void Update()
    {
        if (Target.instance.demonHealth <=0) {
            spawnDemon();
        }
    }
    private void spawnDemon() {
        for (int i = 0; i < demon.Length; i++)
        {
            Instantiate(demon[i], demonSpawnPos[mapIndex].transform.position, Quaternion.identity);

        }
    }
}
