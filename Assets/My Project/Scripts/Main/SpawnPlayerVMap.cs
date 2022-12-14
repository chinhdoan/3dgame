using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayerVMap : MonoBehaviour
{
    [SerializeField] Transform mapSpawnPos;
    [SerializeField] Transform[] playerSpawnPos;
    [SerializeField] GameObject[] map;
    [SerializeField] GameObject player;
    // Start is called before the first frame update

    private void Awake()
    {
        var mapIndex = PlayerPrefs.GetInt("mapSelection",0);
        Instantiate(map[mapIndex], mapSpawnPos.position,Quaternion.identity);
        Instantiate(player, playerSpawnPos[mapIndex].position, Quaternion.identity);
    }
}
