
using System.Net.Http;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using SimpleJSON;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Net;

public class ApiCaller : MonoBehaviour
{
    private string _url;

    public ApiCaller()
    {
        _url = "https://fbmanage.dk/api/";
    }

    public List<BaseGuidName> GetSubjects()
    {
        JSONNode data = GetData("Subjects");
        return data.ToSimpleBaseGuidList();
    }

    public List<BaseGuidName> GetModules(string subjectId)
    {
        JSONNode data = GetData($"Subjects/{subjectId}/Modules");
        return data.ToSimpleBaseGuidList();
    }

    public List<BaseGuidName> GetAssignments(string subjectId, string moduleId)
    {
        JSONNode data = GetData($"Subjects/{subjectId}/Modules/{moduleId}");
        return data.ToSimpleBaseGuidList();
    }

    public AssigmentMockModel GetAssigment(string subjectId, string moduleId, string assignmentId)
    {
        JSONObject data = GetData($"Subjects/{subjectId}/Modules/{moduleId}/Assignments/{assignmentId}").AsObject;
        return data.ToAssigment();
    }


    private JSONNode GetData(string uri)
    {
        HttpWebRequest request =
          (HttpWebRequest)WebRequest.Create(GetUrl(uri));
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        return JSON.Parse(jsonResponse);

    }

    private string GetUrl(string uri)
    {
        return _url + uri;
    }
}