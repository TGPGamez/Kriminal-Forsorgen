using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class extension to JSONNode
/// Used to convert JSONNode object into a specific object or objects
/// </summary>
public static class JSONNodeExtension
{
    #region To single object extensions

    /// <summary>
    /// Converts a JSONObject into a simple object that has as a Id and a Name
    /// </summary>
    /// <param name="jsonObject"></param>
    /// <returns>Object with Id and Name</returns>
    public static SubjectModel ToSimpleBaseGuid(this JSONObject jsonObject)
    {
        //Check if jsonObject is null
        if (jsonObject == null) return new SubjectModel();
        return new(new(jsonObject["id"]), jsonObject["name"]);
    }

    /// <summary>
    /// Converts a JSONObject into an Assignment object
    /// </summary>
    /// <param name="jsonObject"></param>
    /// <returns></returns>
    public static AssigmentModel ToAssigment(this JSONObject jsonObject)
    {
        //Check if jsonObject is null
        if (jsonObject == null) return new AssigmentModel();
        //Set NextGuidId to NextGuidId from jsonObject if not null, otherwise generate new Guid
        Guid nextGuidId = jsonObject["nextAssignmentId"] == null ? new Guid() : new Guid(jsonObject["nextAssignmentId"]);
        //Return Assignment Object
        return new(
            new(jsonObject["id"] ?? ""),
            jsonObject["name"] ?? "",
            jsonObject["question"] ?? "",
            jsonObject["answer"] ?? "",
            nextGuidId
        );
    }

    #endregion

    #region To List of objects extensions
    /// <summary>
    /// Convert a JSONNode into a list of Simple objects with a Id and Name
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static List<BaseGuidName> ToSimpleBaseGuidList(this JSONNode data)
    {
        List<BaseGuidName> list = new List<BaseGuidName>();
        //Cycle through data and add each object to list
        foreach (JSONObject item in data) 
        {
            list.Add(ToSimpleBaseGuid(item));
        }
        return list;
    }

    #endregion
}
