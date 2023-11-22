using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NumberSpawner : MonoBehaviour
{
    [SerializeField] private string displayText;
    [SerializeField] private GameObject spawnPrefab;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject groupObjects;

    // Start is called before the first frame update
    void Start()
    {
        spawnPrefab.GetComponentInChildren<TextMeshProUGUI>().text = displayText;
        if (CanSpawn())
        {
            Spawn();
        }
    }

    private void FixedUpdate()
    {
        if (CanSpawn() && groupObjects.transform.childCount == 0)
        {
            Spawn();
        }
    }

    private bool CanSpawn()
    {
        return spawnPrefab != null && spawnPoint != null;
    }

    private void Spawn()
    {
        Instantiate(spawnPrefab, spawnPoint.transform.position, Quaternion.identity, groupObjects.transform);
    }
}
