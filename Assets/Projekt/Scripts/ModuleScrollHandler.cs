using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ModuleScrollHandler : MonoBehaviour
{
    [SerializeField] private GameObject scrollItemPrefab;
    [SerializeField] private Transform scrollViewContent;
    [SerializeField] private List<ModuleMockModel> modules;
    // Start is called before the first frame update
    private void Awake()
    {
        Guid guid = InformationHolder.Get<Guid>("Subject.Id");
        modules = DataMock.GetMockModules(guid);
    }

    private void Start()
    {
        GenerateChooses();
    }

    private void GenerateChooses()
    {
        foreach (ModuleMockModel module in modules)
        {
            GameObject obj = Instantiate(scrollItemPrefab, scrollViewContent);
            obj.GetComponentInChildren<Text>().text = module.Name;
            Toggle toggle = obj.GetComponentInChildren<Toggle>();
            toggle.group = scrollViewContent.gameObject.GetComponent<ToggleGroup>();
            toggle.onValueChanged.AddListener(delegate
            {
                ChangeScene(toggle, module);
            });

        }
    }
    private void ChangeScene(Toggle change, ModuleMockModel module)
    {
        if (change.isOn)
        {
            InformationHolder.Set("Module.Id", module.Id);
            InformationHolder.Set("Module.Name", module.Name);
            SceneManager.LoadScene("ChooseAssigment");
        }
    }
}
