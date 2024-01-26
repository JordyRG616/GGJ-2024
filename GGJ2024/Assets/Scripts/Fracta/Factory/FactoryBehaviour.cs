using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FactoryBehaviour<T> : MonoBehaviour, IFactory
{
    public Type FactoryType => typeof(T);

    protected virtual void Awake()
    {
        GameMaster.RegisterFactory(this);
    }

    public abstract T Fabricate(FactoryDemands demands);
}

public class FactoryDemands
{

}
