using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : ManagerBehaviour
{
    public Action OnTurnStart;
    public Action OnTurnEnd;

    private JokeManager jokeManager;


    private IEnumerator Start()
    {
        jokeManager = GameMaster.GetManager<JokeManager>();

        yield return new WaitForSeconds(0.1f);

        StartTurn();
    }

    private void StartTurn()
    {
        OnTurnStart?.Invoke();
    }

    public void EndTurn()
    {
        if(jokeManager.CanTellJoke) OnTurnEnd?.Invoke();
    }
}
