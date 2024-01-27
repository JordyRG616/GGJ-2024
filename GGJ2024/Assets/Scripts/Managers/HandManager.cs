using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : ManagerBehaviour, ICardHolder
{
    [SerializeField] private int drawPerTurn;
    [SerializeField] private Transform handHolder;
    private List<CardBlueprint> cardsInHand = new List<CardBlueprint>();
    private DeckManager deckManager;


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

        cardsInHand.Clear();
    }

    private void DrawTurnHand()
    {
        Draw(drawPerTurn);
    }

    public void Draw(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            deckManager.DrawCard(this);
        }
    }

    public void ReceiveCard(CardBlueprint card)
    {
        card.transform.SetParent(handHolder);
        card.SetHolder(this);
        cardsInHand.Add(card);
    }

    public void RemoveCard(CardBlueprint card)
    {
        cardsInHand.Remove(card);
    }
}
