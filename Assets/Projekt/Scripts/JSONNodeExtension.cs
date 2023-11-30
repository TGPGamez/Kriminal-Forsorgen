using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class JSONNodeExtension
{
    #region To single object extensions
    public static SubjectMockModel ToSimpleBaseGuid(this JSONObject jsonObject)
    {
        if (jsonObject == null) return new SubjectMockModel();
        return new(new(jsonObject["id"]), jsonObject["name"]);
    }

    public static AssigmentMockModel ToAssigment(this JSONObject jsonObject)
    {
        if (jsonObject == null) return new AssigmentMockModel();
        return new(
            new(jsonObject["id"]),
            jsonObject["name"],
            jsonObject["question"],
            jsonObject["answer"],
            new(jsonObject["nextAssignmentId"])
        );
    }

    #endregion

    #region To List of objects extensions
    public static List<BaseGuidName> ToSimpleBaseGuidList(this JSONNode data)
    {
        List<BaseGuidName> list = new List<BaseGuidName>();
        foreach (JSONObject item in data) 
        {
            list.Add(ToSimpleBaseGuid(item));
        }
        return list;
    }

    #endregion
}
