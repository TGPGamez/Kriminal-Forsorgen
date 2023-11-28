using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScrollHandler : MonoBehaviour
{
    [SerializeField] private GameObject scrollItemPrefab;
    [SerializeField] private Transform scrollViewContent;
    [SerializeField] private List<BaseGuidName> dataToUI;
    [SerializeField] private string informationHolderIdKey;
    [SerializeField] private string informationHolderNameKey;
    [SerializeField] private string changeToSceneName;
    // Start is called before the first frame update
    private void Awake()
    {
        Guid guid = InformationHolder.Get<Guid>("Subject.Id");
        dataToUI = DataMock.GetMockModules(guid);
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
            InformationHolder.Set($"{informationHolderIdKey}.Id", data.Id);
            InformationHolder.Set($"{informationHolderNameKey}.Id", data.Name);
            SceneManager.LoadScene(changeToSceneName);
        }
    }
}
