using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class GameOverTransitionTrigger : MonoBehaviour
{
    [SerializeField] private float delay;
    public UnityEvent callback;

    private void Start()
    {
        GameMaster.GetManager<AudienceManager>().OnToleranceChanged += (c, m) =>
        {
            if (c == 0)
            {
                StartCoroutine(InvokeCallback());
            }
        };
    }

    private IEnumerator InvokeCallback()
    {
        yield return new WaitForSeconds(delay);

        callback.Invoke();
    }
}
