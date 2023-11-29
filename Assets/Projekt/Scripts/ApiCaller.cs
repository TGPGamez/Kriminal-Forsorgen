
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

    public JSONNode GetData()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(GetUrl("Subjects")))
        {
            request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(request.error);
            } else
            {
                JSONNode itemsData = JSON.Parse(request.downloadHandler.text);
                return itemsData;
            }
        }
        return null;
    }

    private string GetUrl(string uri)
    {
        return _url + uri;
    }
}