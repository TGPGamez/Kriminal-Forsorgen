using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Class that can create a scroll view
/// Which is used in the scenes where you need to choose Subject, Module or Assignment.
/// </summary>
public class ScrollHandler : MonoBehaviour
{
    [SerializeField] SceneHandler sceneHandler;
    [SerializeField] private GameObject scrollItemPrefab;
    [SerializeField] private Transform scrollViewContent;
    [SerializeField] private SceneChooseType sceneChooseType;
    [SerializeField] private string informationHolderKeyPrefix;
    [SerializeField] private string getInformationFromPrefix;
    [SerializeField] private string changeToSceneName;
    [SerializeField] private string changeBackSceneName;
    [SerializeField] private ApiCaller apiCaller;
    [SerializeField] private Button backButton;
    // Start is called before the first frame update
    private List<BaseGuidName> dataToUI;
    
    /// <summary>
    /// Method called when script instance is being loaded
    /// </summary>
    private void Awake()
    {
        //Check if backButton is set
        if (backButton != null)
        {
            //Check if changeBackSceneName is set to determine if the backbutton 
            //should be enabled, so you can go back to specific scene.
            if (string.IsNullOrEmpty(changeBackSceneName))
            {
                backButton.gameObject.SetActive(false);
            } else
            {
                backButton.onClick.AddListener(delegate { sceneHandler.ChangeScene(changeBackSceneName); });
            }
        }
        //Determine scene type to load data
        switch (sceneChooseType)
        {
            case SceneChooseType.Subject:
                //Get Subjects from api
                dataToUI = apiCaller.GetSubjects();
                break;
            case SceneChooseType.Module:
                //Get Module out from the subject id from api
                dataToUI = apiCaller.GetModules(InformationHolder.Get<Guid>("Subject.Id").ToString());
                break;
            case SceneChooseType.Assigment:
                //Get Assignments out from the subject id and module id from api
                dataToUI = apiCaller.GetAssignments(InformationHolder.Get<Guid>("Subject.Id").ToString(),
                    InformationHolder.Get<Guid>("Module.Id").ToString());
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Method is called before any Update methods
    /// </summary>
    private void Start()
    {   
        GenerateChooses();
    }

    /// <summary>
    /// Method to generate the chooses from the data recieved from example a api
    /// </summary>
    private void GenerateChooses()
    {
        foreach (BaseGuidName data in dataToUI)
        {
            //Instantiates a new object out from a prefab and parent transform.
            GameObject obj = Instantiate(scrollItemPrefab, scrollViewContent);
            //Sets the text on the object to name from data
            obj.GetComponentInChildren<Text>().text = data.Name;
            //Get Toggle script from component in children
            Toggle toggle = obj.GetComponentInChildren<Toggle>();
            //Set the group to the scrollViewContent, so only a single value can be selected
            toggle.group = scrollViewContent.gameObject.GetComponent<ToggleGroup>();
            //Add event to Toggle when value is changed
            toggle.onValueChanged.AddListener(delegate
            {
                ChangeScene(toggle, data);
            });

        }
    }
    /// <summary>
    /// Method to change scene when Toggle changed
    /// </summary>
    /// <param name="change"></param>
    /// <param name="data"></param>
    private void ChangeScene(Toggle change, BaseGuidName data)
    {
        if (change.isOn)
        {
            //Set information to InformationHolder, so another component/script can use the selected data
            InformationHolder.Set($"{informationHolderKeyPrefix}.Id", data.Id);
            InformationHolder.Set($"{informationHolderKeyPrefix}.Name", data.Name);
            //Check if a scene is typed in, othervise
            //use scenehandler's load assignment method, so custom scene can be loaded
            if (!string.IsNullOrEmpty(changeToSceneName))
            {
                sceneHandler.ChangeScene(changeToSceneName);
            }
            else
            {
                sceneHandler.LoadAssigment(InformationHolder.Get<string>(getInformationFromPrefix + ".Name"));
            }
        }
    }
}

public enum SceneChooseType
{
    Subject,
    Module,
    Assigment
}
