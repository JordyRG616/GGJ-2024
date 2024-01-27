using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBlueprint
{
    public GameObject GetConcrete();
    public void Configure(FactoryConfiguration configuration);
}
