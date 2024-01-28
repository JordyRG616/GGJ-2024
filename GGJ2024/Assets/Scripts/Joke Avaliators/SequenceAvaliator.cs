using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Avaliators/Sequence")]
public class SequenceAvaliator : JokeAvaliator
{
    public override bool Fulfilled(List<CardBlueprint> cards)
    {
        if (cards.Count < 5) return false;

        var orderedCards = cards.OrderBy(x => x.Value.Value).ToList();

        for (int i = 0; i < orderedCards.Count - 1; i++)
        {
            var value = orderedCards[i].Value.Value;
            if (orderedCards[i + 1].Value.Value != value + 1)
            {
                return false;
            }
        }

        return true;
    }
}
