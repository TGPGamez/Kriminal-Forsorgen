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
}


public class SubjectMockModel : BaseGuidName
{
    public SubjectMockModel(Guid id, string name) : base(id, name)
    {
    }
}

public class ModuleMockModel : BaseGuidName
{
    public ModuleMockModel(Guid id, string name) : base(id, name)
    {
    }
}

public class AssigmentMockModel : BaseGuidName
{
    public AssigmentMockModel(Guid id, string name) : base(id, name)
    {
    }
}