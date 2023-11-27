using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeleteNumber : MonoBehaviour
{
    public UnityEvent DestroyEvent = new();
    public UnityEvent SnappedEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("BoardRelated") && !other.gameObject.CompareTag("NumberObject"))
        {
            DestroyEvent?.Invoke();
            Destroy(gameObject);
        }
    }

    public void Snapped()
    {
        SnappedEvent?.Invoke();
    }
}
