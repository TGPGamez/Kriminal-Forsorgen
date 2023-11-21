using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NumberSpawner : MonoBehaviour
{
    [SerializeField] private string displayText;
    [SerializeField] private GameObject spawnObject;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject groupObjects;
    
    // Start is called before the first frame update
    void Start()
    {
        spawnObject.GetComponentInChildren<TextMeshProUGUI>().text = displayText;
        if (CanSpawn())
        {
            Spawn();
        }  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool CanSpawn()
    {
        return spawnObject != null && spawnPoint != null;
    }

    private void Spawn()
    {
        Instantiate(spawnObject, spawnPoint.transform.position, Quaternion.identity, groupObjects.transform);
    }
}
