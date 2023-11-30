using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class DataMock
{
    public static List<BaseGuidName> GetMockSubjects()
    {
        return new List<BaseGuidName>()
        {
            new SubjectMockModel(new("4e4287b8-d413-4ddc-96d7-227034a7f8a2"), "Matematik"),
            new SubjectMockModel(new("9b6d09ea-2476-426e-b708-b34325451e4b"), "Dansk"),
            new SubjectMockModel(new("8ee63da1-fee9-4189-8391-996b648745c9"), "Engelsk")
        };
    }

    public static List<BaseGuidName> GetMockModules(Guid guid)
    {
        if (guid.ToString().Equals("4e4287b8-d413-4ddc-96d7-227034a7f8a2"))
        {
            return new List<BaseGuidName>()
            {
                new ModuleMockModel(new Guid("059c7eea-1b1d-420d-ba24-1f13e9b4c8c1"), "Algebra"),
                new ModuleMockModel(new Guid("846ede18-550c-49b8-bff2-3feb3031330f"), "Ligninger"),
                new ModuleMockModel(new Guid("c2b80f08-b5cb-42c4-bc84-dc1e2047edd7"), "Trigonometri")
            };
        }
        return new List<BaseGuidName>();
    }

    public static List<BaseGuidName> GetMockAssigments(Guid guid)
    {
        if (guid.ToString().Equals("059c7eea-1b1d-420d-ba24-1f13e9b4c8c1"))
        {
            return new List<BaseGuidName>()
            {
                new ModuleMockModel(new Guid("715fc074-db8a-4c9b-825a-18432d1b08b6"), "Opgave 1"),
                new ModuleMockModel(new Guid("0efae0bb-32c2-4689-8840-a727bff6743d"), "Opgave 2"),
                new ModuleMockModel(new Guid("ad9b5edf-dee7-4910-b65b-0516ad82ba86"), "Opgave 3"),
                new ModuleMockModel(new Guid("d74e2b67-c2b8-4e14-9388-fed322a13f30"), "Opgave 4"),
                new ModuleMockModel(new Guid("059c7eea-1b1d-420d-ba24-1f13e9b4c8c1"), "Opgave 5"),
                new ModuleMockModel(new Guid("846ede18-550c-49b8-bff2-3feb3031330f"), "Opgave 6"),
                new ModuleMockModel(new Guid("c2b80f08-b5cb-42c4-bc84-dc1e2047edd7"), "Opgave 7"),
                new ModuleMockModel(new Guid("f3264209-93df-44c1-83bf-0f6c1c3024e7"), "Opgave 8"),
                new ModuleMockModel(new Guid("2e962972-b030-4dd5-b649-bd72a239e2f7"), "Opgave 9"),
                new ModuleMockModel(new Guid("3d974144-1a1b-4c33-85d7-cbe90fb59dad"), "Opgave 10")
            };
        }
        return new List<BaseGuidName>();
    }

    public static AssigmentMockModel GetAssigment(Guid? guid)
    {
        switch (guid.ToString())
        {
            case "715fc074-db8a-4c9b-825a-18432d1b08b6":
                return new AssigmentMockModel(guid, "Opgave 1",
                    "Træk -4 fra -10", "-6", new("0efae0bb-32c2-4689-8840-a727bff6743d"));
            case "0efae0bb-32c2-4689-8840-a727bff6743d":
                return new AssigmentMockModel(guid, "Opgave 2",
                    "Træk -7 fra -20", "-13", new("ad9b5edf-dee7-4910-b65b-0516ad82ba86"));
            case "ad9b5edf-dee7-4910-b65b-0516ad82ba86":
                return new AssigmentMockModel(guid, "Opgave 3",
                    "-13 + 5", "-8", new("d74e2b67-c2b8-4e14-9388-fed322a13f30"));
            case "d74e2b67-c2b8-4e14-9388-fed322a13f30":
                return new AssigmentMockModel(guid, "Opgave 4",
                    "-16 + 7", "-9", new("059c7eea-1b1d-420d-ba24-1f13e9b4c8c1"));
            case "059c7eea-1b1d-420d-ba24-1f13e9b4c8c1":
                return new AssigmentMockModel(guid, "Opgave 5",
                    "3(8 + 2 − 4)", "18", new("846ede18-550c-49b8-bff2-3feb3031330f"));
            case "846ede18-550c-49b8-bff2-3feb3031330f":
                return new AssigmentMockModel(guid, "Opgave 6",
                    "8 * (5 + 10)", "120", new Guid("c2b80f08-b5cb-42c4-bc84-dc1e2047edd7"));
            case "c2b80f08-b5cb-42c4-bc84-dc1e2047edd7":
                return new AssigmentMockModel(guid, "Opgave 7",
                    "3(8 + 14)", "66", new Guid("f3264209-93df-44c1-83bf-0f6c1c3024e7"));
            case "f3264209-93df-44c1-83bf-0f6c1c3024e7":
                return new AssigmentMockModel(guid, "Opgave 8",
                    "−6(5 + 10)", "-90", new Guid("2e962972-b030-4dd5-b649-bd72a239e2f7"));
            case "2e962972-b030-4dd5-b649-bd72a239e2f7":
                return new AssigmentMockModel(guid, "Opgave 9",
                    "-3(3 + 11)", "-42", new Guid("3d974144-1a1b-4c33-85d7-cbe90fb59dad"));
            case "3d974144-1a1b-4c33-85d7-cbe90fb59dad":
                return new AssigmentMockModel(guid, "Opgave 10",
                    "8(9 + 13)", "176", null);
            default:
                return null;
        }
    }
}

[Serializable]
public class SubjectMockModel : BaseGuidName
{
    public SubjectMockModel() : base()
    {
        
    }
    public SubjectMockModel(Guid id, string name) : base(id, name)
    {
    }
}

public class ModuleMockModel : BaseGuidName
{
    public ModuleMockModel() : base()
    {
        
    }
    public ModuleMockModel(Guid id, string name) : base(id, name)
    {
    }
}

public class AssigmentMockModel : BaseGuidName
{
    public string Question { get; set; }
    public string Answer { get; set; }
    public Guid? NextAssignmentId { get; set; }

    public AssigmentMockModel(Guid? id, string name) : base(id, name)
    {
    }

    public AssigmentMockModel(Guid? id, string name, string question, string answer, Guid? nextAssignmentId)
        : base(id, name)
    {
        Question = question;
        Answer = answer;
        NextAssignmentId = nextAssignmentId;
    }
}