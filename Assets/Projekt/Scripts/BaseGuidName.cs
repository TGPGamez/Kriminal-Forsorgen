using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGuidName
{
    public BaseGuidName(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
}
