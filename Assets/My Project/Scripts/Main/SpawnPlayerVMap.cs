using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayerVMap : MonoBehaviour
{
    [SerializeField] Transform mapSpawnPos, playerSpawnPos;
    [SerializeField] GameObject[] map;
    [SerializeField] GameObject player;
    // Start is called before the first frame update

    private void Awake()
    {
        Instantiate(map[0], mapSpawnPos.position,Quaternion.identity);
        Instantiate(player, playerSpawnPos.position, Quaternion.identity);
    }

}
