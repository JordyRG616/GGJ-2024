using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FactoryBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject blueprint;
    private FactoryHandler handler;
    private FactoryConfiguration defaultConfiguration;


    protected virtual void Awake()
    {
        GameMaster.RegisterFactory(this);
    }

    public virtual void InitializeFactory<T>() where T : IBlueprint
    {
        var iBlueprint = blueprint.GetComponent<IBlueprint>();
        handler = new FactoryHandler<T>(iBlueprint);
    }

    public virtual GameObject Create<T>(out T blueprint) where T : IBlueprint
    {
        var obj = Instantiate(handler.Print());
        blueprint = obj.GetComponent<T>();
        blueprint.Configure(defaultConfiguration);

        return obj;
    }

    public virtual GameObject Create<T>(FactoryConfiguration configuration, out T blueprint) where T : IBlueprint
    {
        var obj = Instantiate(handler.Print());
        blueprint = obj.GetComponent<T>();
        blueprint.Configure(configuration);

        return obj;
    }

    private void OnValidate()
    {
        if (blueprint != null)
        {
            if (!blueprint.TryGetComponent<IBlueprint>(out var _b))
            {
                Debug.LogError("The assigned Game Object does not have a component that implements IBlueprint");
                blueprint = null;
            }
        }
    }

    private void OnDestroy()
    {
        GameMaster.RemoveFactory(this);
    }
}

public abstract class FactoryHandler
{
    public abstract GameObject Print();
}

public class FactoryHandler<T> : FactoryHandler where T : IBlueprint
{
    private IBlueprint blueprint;


    public FactoryHandler(IBlueprint blueprint)
    {
        this.blueprint = blueprint;
    }

    public override GameObject Print()
    {
        return blueprint.GetConcrete();
    }
}

public class FactoryConfiguration
{

}
