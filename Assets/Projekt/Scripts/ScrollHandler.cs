using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ScrollHandler : MonoBehaviour
{
    [SerializeField] SceneHandler sceneHandler;
    [SerializeField] private GameObject scrollItemPrefab;
    [SerializeField] private Transform scrollViewContent;
    [SerializeField] private SceneChooseType sceneChooseType;
    [SerializeField] private string informationHolderKeyPrefix;
    [SerializeField] private string getInformationFromPrefix;
    [SerializeField] private string changeToSceneName;
    [SerializeField] private ApiCaller apiCaller;
    // Start is called before the first frame update
    private List<BaseGuidName> dataToUI;
    private void Awake()
    {
        Test();
        switch (sceneChooseType)
        {
            case SceneChooseType.Subject:
                dataToUI = DataMock.GetMockSubjects();
                break;
            case SceneChooseType.Module:
                dataToUI = DataMock.GetMockModules(InformationHolder.Get<Guid>(getInformationFromPrefix + ".Id"));
                break;
            case SceneChooseType.Assigment:
                dataToUI = DataMock.GetMockAssigments(InformationHolder.Get<Guid>(getInformationFromPrefix + ".Id"));
                break;
        }
    }

    private void Test()
    {

        List<SubjectMockModel> subjects = apiCaller.Get<List<SubjectMockModel>>("Subjects").Result;
    }

    private void Start()
    {
        GenerateChooses();
    }

    private void GenerateChooses()
    {
        foreach (BaseGuidName data in dataToUI)
        {
            GameObject obj = Instantiate(scrollItemPrefab, scrollViewContent);
            obj.GetComponentInChildren<Text>().text = data.Name;
            Toggle toggle = obj.GetComponentInChildren<Toggle>();
            toggle.group = scrollViewContent.gameObject.GetComponent<ToggleGroup>();
            toggle.onValueChanged.AddListener(delegate
            {
                ChangeScene(toggle, data);
            });

        }
    }
    private void ChangeScene(Toggle change, BaseGuidName data)
    {
        if (change.isOn)
        {
            InformationHolder.Set($"{informationHolderKeyPrefix}.Id", data.Id);
            InformationHolder.Set($"{informationHolderKeyPrefix}.Name", data.Name);
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
