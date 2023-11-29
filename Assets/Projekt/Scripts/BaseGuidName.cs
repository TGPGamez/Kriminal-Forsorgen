using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class BaseGuidName
{
    public BaseGuidName(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public BaseGuidName()
    {
        
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
}
