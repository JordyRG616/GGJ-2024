using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Avaliators/Pair")]
public class PairAvaliator : JokeAvaliator
{
    public override bool Fulfilled(List<CardBlueprint> cards)
    {
        if (cards.Count < 2) return false;

        for (int i = 0; i < cards.Count; i++)
        {
            var value = cards[i].Value;

            for (int j = i + 1; j < cards.Count; j++)
            {
                if (cards[j].Value == value) return true;
            }
        }

        return false;
    }
}
