using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceManager : ManagerBehaviour, ICardHolder
{
    [SerializeField] private List<Transform> cardSlots;

    [SerializeField] private int initialFlip;
    [SerializeField] private int maxTolerance;
    [SerializeField] private int initialTolerance;
    [SerializeField] private List<int> noJokeToleranceHit;

    [SerializeField] ParticleSystem hahaVFX_1;
    [SerializeField] ParticleSystem hahaVFX_2;

    public Action<int, int> OnToleranceChanged;

    private List<CardBlueprint> cardsInTable = new List<CardBlueprint>();
    private DeckManager deckManager;
    private int toleranceHitIndex = 0;
    private int turnIndex;

    private int _tolerance;
    public int CurrentTolerance
    {
        get => _tolerance;
        set
        {
            _tolerance = Mathf.Clamp(value, 0, maxTolerance);
            OnToleranceChanged?.Invoke(_tolerance, maxTolerance);
        }
    }


    private void Start()
    {
        deckManager = GameMaster.GetManager<DeckManager>();
        var gameFlow = GameMaster.GetManager<GameFlowManager>();
        gameFlow.OnTurnStart += FlipInitialPrompts;
        gameFlow.OnTurnEnd += ReturnCardsToDeck;
        gameFlow.OnTurnEnd += ScaleHits;

        CurrentTolerance = initialTolerance;
        toleranceHitIndex = -initialFlip;
    }

    private void ScaleHits()
    {
        turnIndex++;

        for (int i = 0; i < noJokeToleranceHit.Count; i++)
        {
            noJokeToleranceHit[i] += (i + 1) * turnIndex;
        }
    }

    private void ReturnCardsToDeck()
    {
        foreach (var card in cardsInTable)
        {
            deckManager.ReceiveCard(card);
        }

        cardsInTable.Clear();
        toleranceHitIndex = -initialFlip;
    }

    private void FlipInitialPrompts()
    {
        AskForPrompt(initialFlip);
    }

    public void ReceiveCard(CardBlueprint card)
    {
        var slot = cardSlots.Find(x => x.childCount == 0);

        card.transform.SetParent(slot);
        card.SetHolder(this);
        cardsInTable.Add(card);
        card.SetFrameOrientation(new Vector3(Mathf.Sign(slot.localPosition.x), Mathf.Sign(slot.localPosition.y), 1));

        var rect = card.transform as RectTransform;
        rect.anchorMin = Vector2.one * .5f;
        rect.anchorMax = Vector2.one * .5f;
        rect.anchoredPosition = Vector2.zero;
    }

    public void RemoveCard(CardBlueprint card)
    {
        cardsInTable.Remove(card);
    }

    public void AskForPrompt(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            deckManager.DrawCard(this);
        }

        toleranceHitIndex++;
    }

    public void EvaluateToleranceChange(int toleranceReward)
    {
        CurrentTolerance += toleranceReward;

        if(toleranceReward > 0)
        {
            var rdm = UnityEngine.Random.Range(0, 1f);

            if (rdm < .33f) hahaVFX_1.Play();
            else if(rdm > .66f)
            {
                hahaVFX_1.Play();
                hahaVFX_2.Play();
            } else
            {
                hahaVFX_2.Play();
            }
        }
    }

    public void ApplyToleranceHit()
    {
        if (toleranceHitIndex >= noJokeToleranceHit.Count)
            toleranceHitIndex = noJokeToleranceHit.Count - 1;

        CurrentTolerance -= noJokeToleranceHit[toleranceHitIndex];
    }
}
