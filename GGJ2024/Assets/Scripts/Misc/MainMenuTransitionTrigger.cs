using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainMenuTransitionTrigger : MonoBehaviour
{
    [SerializeField] private float delay;
    public UnityEvent callback;

    private void Start()
    {
        GameMaster.GetManager<GameFlowManager>().OnGameStart += () => StartCoroutine(InvokeCallback());
    }

    private IEnumerator InvokeCallback()
    {
        yield return new WaitForSeconds(delay);

        callback.Invoke();
    }
}
