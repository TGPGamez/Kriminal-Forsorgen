using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResetSpawners : MonoBehaviour
{
    private List<NumberSpawner> spawners;
    void Start()
    {
        spawners = gameObject.GetComponentsInChildren<NumberSpawner>().ToList();
    }

    public void ResetAll()
    {
        foreach (NumberSpawner spawner in spawners)
        {
            spawner.ResetSpawner();
        }
    }
}
