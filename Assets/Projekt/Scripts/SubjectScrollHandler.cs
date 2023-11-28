using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SubjectScrollHandler : MonoBehaviour
{
    [SerializeField] private GameObject scrollItemPrefab;
    [SerializeField] private Transform scrollViewContent;
    [SerializeField] private List<SubjectMockModel> subjects;
    // Start is called before the first frame update
    private void Awake()
    {
        subjects = DataMock.GetMockSubjects();
    }

    private void Start()
    {
        GenerateChooses();
    }

    private void GenerateChooses()
    {
        foreach (SubjectMockModel subject in subjects)
        {
            GameObject obj = Instantiate(scrollItemPrefab, scrollViewContent);
            obj.GetComponentInChildren<Text>().text = subject.Name;
            Toggle toggle = obj.GetComponentInChildren<Toggle>();
            toggle.group = scrollViewContent.gameObject.GetComponent<ToggleGroup>();
            toggle.onValueChanged.AddListener(delegate
            {
                ChangeScene(toggle, subject);
            });

        }
    }
    private void ChangeScene(Toggle change, SubjectMockModel subject)
    {
        if (change.isOn)
        {
            InformationHolder.Set("Subject.Id", subject.Id);
            InformationHolder.Set("Subject.Name", subject.Name);
            SceneManager.LoadScene("ChooseModule");
        }
    }
}
