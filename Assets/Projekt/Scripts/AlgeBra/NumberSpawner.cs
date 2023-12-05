using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
        if (CanSpawn())
        {
            Spawn();
        }
    }

    /// <summary>
    /// Check if number object can be spawned
    /// </summary>
    /// <returns>boolean</returns>
    private bool CanSpawn()
    {
        return spawnPrefab != null && spawnPoint != null;
    }

    /// <summary>
    /// Spawn number object and add neccesary scripts and components to gameobject
    /// </summary>
    private void Spawn()
    {
        //Instantiate object
        GameObject obj = Instantiate(spawnPrefab, spawnPoint.transform.position, Quaternion.identity, groupObjects.transform);
        //Set text to the right number from spawner
        obj.GetComponentInChildren<TextMeshProUGUI>().text = displayText;

        //Add Number events
        NumberEvents deleteNumber = obj.GetComponent<NumberEvents>();
        deleteNumber.DestroyEvent.AddListener(DestroyedGO);
        deleteNumber.SnappedEvent.AddListener(Snapped);
    }

    /// <summary>
    /// Reset spawner
    /// </summary>
    public void ResetSpawner()
    {
        //Destroy all number objects
        foreach (Transform t in groupObjects.transform)
        {
            Destroy(t.gameObject);
        }
        //Check if can spawn new
        if (CanSpawn())
        {
            Spawn();
        }
    }

    /// <summary>
    /// Method called when number object snap event has occured
    /// </summary>
    private void Snapped()
    {
        if (CanSpawn() && groupObjects.transform.childCount == 0)
        {
            Spawn();
        }
    }

    /// <summary>
    /// Method called when number gameobject is destroyed
    /// </summary>
    private void DestroyedGO()
    {
        
        if (CanSpawn() && groupObjects.transform.childCount <= 1)
        {
            Spawn();
        }
    }
}
