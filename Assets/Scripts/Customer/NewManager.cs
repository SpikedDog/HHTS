using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewManager : MonoBehaviour
{
    Start()
    {
        for (i = 10)
        {
            SpawnHector()
            }
    }

    SpawnHector()
    {
        if (SpawnPosInt > 10)
        {
            SpawnPosInt = 0
        }
        Instantiate(Hector, HectorSpawnPos(SpawnPosInt));
    }

    DespawnHector()
    {
        Destroy(Hector);
        SpawnHector();
    }
}
