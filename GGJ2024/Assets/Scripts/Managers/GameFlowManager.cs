using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : ManagerBehaviour
{
    [SerializeField] private float transitionDelay;

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
        StartCoroutine(DoTransitionBetweenTurns());
    }

    private IEnumerator DoTransitionBetweenTurns()
    {
        if (jokeManager.CanTellJoke)
        {
            jokeManager.EvaluateJoke();

            yield return new WaitForSeconds(transitionDelay);
        }

        OnTurnEnd?.Invoke();

        yield return new WaitForSeconds(transitionDelay);

        StartTurn();
    }
}
