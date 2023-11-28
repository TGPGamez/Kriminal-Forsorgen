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

    private bool CanSpawn()
    {
        return spawnPrefab != null && spawnPoint != null;
    }

    private void Spawn()
    {
        GameObject obj = Instantiate(spawnPrefab, spawnPoint.transform.position, Quaternion.identity, groupObjects.transform);
        obj.GetComponentInChildren<TextMeshProUGUI>().text = displayText;

        DeleteNumber deleteNumber = obj.GetComponent<DeleteNumber>();
        deleteNumber.DestroyEvent.AddListener(DestroyedGO);
        deleteNumber.SnappedEvent.AddListener(Snapped);
    }

    public void ResetSpawner()
    {
        foreach (Transform t in groupObjects.transform)
        {
            Destroy(t.gameObject);
        }
        if (CanSpawn())
        {
            Spawn();
        }
    }
    private void Snapped()
    {
        if (CanSpawn() && groupObjects.transform.childCount == 0)
        {
            Spawn();
        }
    }
    private void DestroyedGO()
    {
        
        if (CanSpawn() && groupObjects.transform.childCount <= 1)
        {
            Spawn();
        }
    }
}
