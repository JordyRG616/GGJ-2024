using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : ManagerBehaviour
{
    public Action OnTurnStart;
    public Action OnTurnEnd;


    [ContextMenu("Start Turn")]
    private void StartTurn()
    {
        OnTurnStart?.Invoke();
    }

    public void EndTurn()
    {
        OnTurnEnd?.Invoke();
    }
}
