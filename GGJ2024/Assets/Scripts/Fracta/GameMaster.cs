using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameMaster
{
    private static List<ManagerBehaviour> managers = new();
    private static List<FactoryBehaviour> factories = new();


    public static void RegisterManager(ManagerBehaviour manager)
    {
        managers.Add(manager);
    }

    public static void RemoveManager(ManagerBehaviour manager)
    {
        if (!managers.Contains(manager)) return;
        
        managers.Remove(manager);
    }

    public static void RegisterFactory(FactoryBehaviour factory)
    {
        factories.Add(factory);
    }

    public static void RemoveFactory(FactoryBehaviour factory)
    {
        if (!factories.Contains(factory)) return;

        factories.Remove(factory);
    }

    public static T GetManager<T>() where T : ManagerBehaviour
    {
        var selection = managers.Find(manager => manager is T);
        return selection as T;
    }

    public static T GetFactory<T>() where T : FactoryBehaviour
    {
        var selection = factories.Find(factory => factory is T);
        return selection as T;
    }
}
