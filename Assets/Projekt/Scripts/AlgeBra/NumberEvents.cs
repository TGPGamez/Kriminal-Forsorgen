using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NumberEvents : MonoBehaviour
{
    public UnityEvent DestroyEvent = new();
    public UnityEvent SnappedEvent;

    /// <summary>
    /// Event that triggers when gameobject collides with another gameobject
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        //Check if collided element is not related to board og number object
        if (!other.gameObject.CompareTag("BoardRelated") && !other.gameObject.CompareTag("NumberObject"))
        {
            //Invoke the destroy event
            DestroyEvent?.Invoke();
            //Detroy the gameobject (deletes it from game)
            Destroy(gameObject);
        }
    }

    public void Snapped()
    {
        SnappedEvent?.Invoke();
    }
}
