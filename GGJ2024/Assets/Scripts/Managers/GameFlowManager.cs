using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : ManagerBehaviour
{
    [SerializeField] private float transitionDelay;

    public Action OnGameStart;
    public Action OnTurnStart;
    public Action OnTurnEnd;

    private JokeManager jokeManager;


    private void Start()
    {
        jokeManager = GameMaster.GetManager<JokeManager>();
    }

    public void StartGame()
    {
        OnGameStart?.Invoke();

        Invoke("StartTurn", 3f);
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
