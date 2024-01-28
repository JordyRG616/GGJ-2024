using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : ManagerBehaviour, ICardHolder
{
    [SerializeField] private List<Transform> cardSlots;
    [SerializeField] private int drawPerTurn;
    [SerializeField] private int handSize = 4;
    private List<CardBlueprint> cardsInHand = new List<CardBlueprint>();
    private DeckManager deckManager;

    private bool usedNotebook = false;

    private void Start()
    {
        deckManager = GameMaster.GetManager<DeckManager>();
        var gameFlow = GameMaster.GetManager<GameFlowManager>();
        gameFlow.OnTurnStart += DrawTurnHand;
        gameFlow.OnTurnEnd += ReturnCardsToDeck;
    }

    private void ReturnCardsToDeck()
    {
        foreach (var card in cardsInHand)
        {
            deckManager.ReceiveCard(card);
        }

        usedNotebook = false;
        cardsInHand.Clear();
    }

    private void DrawTurnHand()
    {
        Draw(drawPerTurn);
    }

    public void UseNotebook()
    {
        Draw(1);
        usedNotebook = true;
    }

    public void Draw(int amount)
    {
        if (cardsInHand.Count == handSize) return;

        for (int i = 0; i < amount; i++)
        {
            deckManager.DrawCard(this);
        }
    }

    public void ReceiveCard(CardBlueprint card)
    {
        var slot = cardSlots.Find(x => x.childCount == 0);

        card.transform.SetParent(slot);
        card.SetHolder(this);
        cardsInHand.Add(card);
        card.SetFrameOrientation(new Vector3(Mathf.Sign(slot.localPosition.x), Mathf.Sign(slot.localPosition.y), 1));

        var rect = card.transform as RectTransform;
        rect.anchorMin = Vector2.one * .5f;
        rect.anchorMax = Vector2.one * .5f;
        rect.anchoredPosition = Vector2.zero;
    }

    public void RemoveCard(CardBlueprint card)
    {
        cardsInHand.Remove(card);
    }
}
