using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScrollHandler : MonoBehaviour
{
    [SerializeField] private GameObject scrollItemPrefab;
    [SerializeField] private Transform scrollViewContent;
    [SerializeField] private SceneChooseType sceneChooseType;
    [SerializeField] private string informationHolderKeyPrefix;
    [SerializeField] private string getInformationFromPrefix;
    [SerializeField] private string changeToSceneName;
    // Start is called before the first frame update
    private List<BaseGuidName> dataToUI;
    private void Awake()
    {
        switch (sceneChooseType)
        {
            case SceneChooseType.Subject:
                dataToUI = DataMock.GetMockSubjects();
                break;
            case SceneChooseType.Module:
                dataToUI = DataMock.GetMockModules(InformationHolder.Get<Guid>(getInformationFromPrefix));
                break;
            case SceneChooseType.Assigment:
                dataToUI = DataMock.GetMockAssigments(InformationHolder.Get<Guid>(getInformationFromPrefix));
                break;
        }
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
                SceneManager.LoadScene(changeToSceneName);
            }
            else
            {
                //Make an alternativ manager that will set the load the correct scene type
                Debug.Log($"{data.Name} with Guid: {data.Id}");
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
