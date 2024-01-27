using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : ManagerBehaviour, ICardHolder
{
    [SerializeField] private Transform deckHolder;
    private List<CardBlueprint> deck = new List<CardBlueprint>();


    private void Start()
    {
        var factory = GameMaster.GetFactory<CardFactory>();

        var initialDeck = factory.CreateInitialDeck();
        initialDeck.ForEach(x => ReceiveCard(x));
    }

    public void DrawCard(ICardHolder holder)
    {
        var rdm = Random.Range(0, deck.Count);
        var card = deck[rdm];
        deck.Remove(card);
        
        holder.ReceiveCard(card);
    }

    public void ReceiveCard(CardBlueprint card)
    {
        var transf = card.transform;
        transf.SetParent(deckHolder);
        transf.localScale = Vector3.one;
        
        deck.Add(card);
    }

    public void RemoveCard(CardBlueprint card)
    {
        deck.Remove(card);
    }
}
