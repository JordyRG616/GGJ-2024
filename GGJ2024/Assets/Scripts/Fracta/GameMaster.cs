using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameMaster
{
    private static List<ManagerBehaviour> managers = new();
    private static List<IFactory> factories = new();


    public static void RegisterManager(this ManagerBehaviour manager)
    {
        managers.Add(manager);
    }

    public static void RegisterFactory(IFactory factory)
    {
        factories.Add(factory);
    }

    public static T GetManager<T>() where T : ManagerBehaviour
    {
        var selection = managers.Find(manager => manager is T);
        return selection as T;
    }

    public static IFactory GetFactory<T>()
    {
        var selection = factories.Find(factory => factory.FactoryType == typeof(T));
        return selection;
    }
}
