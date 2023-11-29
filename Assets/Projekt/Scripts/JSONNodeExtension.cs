using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class JSONNodeExtension
{
    public static SubjectMockModel ToSubject(this JSONNode node)
    {
        if (node == null) return new SubjectMockModel();
        SubjectMockModel subject = new SubjectMockModel();

        subject.Id = Valid<Guid>(node["id"]);
        subject.Name = Valid<string>(node["name"]);

        return subject;
    }

    public static List<SubjectMockModel> ToSubjectList(this JSONNode data)
    {
        Debug.Log(data);
        foreach (JSONNode item in data) 
        {
            Debug.Log(item.Value);
        }

        return new List<SubjectMockModel>();
    }


    private static T Valid<T>(this object value)
    {
        if (value == null) return default;
        return (T)value;
    }
}
