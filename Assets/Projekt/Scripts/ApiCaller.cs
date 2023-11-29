
using System.Net.Http;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using SimpleJSON;
using System.Threading.Tasks;

public class ApiCaller : MonoBehaviour
{
    private string _url;

    public ApiCaller()
    {
        _url = "https://fbmanage.dk/api/";
    }

    public IEnumerator GetData(string uri)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(GetUrl(uri)))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(request.error);
            } else
            {
                JSONNode itemsData = JSON.Parse(request.downloadHandler.text);
                Debug.Log(itemsData);
            }
        }

    }

    private string GetUrl(string uri)
    {
        return _url + uri;
    }
}