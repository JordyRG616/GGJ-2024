using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Avaliators/Flush")]
public class FlushAvaliator : JokeAvaliator
{
    public override bool Fulfilled(List<CardBlueprint> cards)
    {
        if (cards.Count < 5) return false;

        var suit = cards[0].Suit;

        foreach (var card in cards)
        {
            if (card.Suit != suit) return false;
        }

        return true;
    }
}
