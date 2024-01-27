using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceManager : ManagerBehaviour, ICardHolder
{
    [SerializeField] private int initialFlip;
    [SerializeField] private Transform tableHolder;
    private List<CardBlueprint> cardsInTable = new List<CardBlueprint>();
    private DeckManager deckManager;


    private void Start()
    {
        deckManager = GameMaster.GetManager<DeckManager>();
        GameMaster.GetManager<GameFlowManager>().OnTurnStart += FlipInitialPrompts;
    }

    private void FlipInitialPrompts()
    {
        AskForPrompt(initialFlip);
    }

    public void ReceiveCard(CardBlueprint card)
    {
        card.transform.SetParent(tableHolder);
        cardsInTable.Add(card);
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
    }
}
