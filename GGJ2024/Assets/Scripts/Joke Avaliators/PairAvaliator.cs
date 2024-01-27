using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Avaliators/Pair")]
public class PairAvaliator : JokeAvaliator
{
    public override bool Fulfilled(List<CardBlueprint> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            var tone = cards[i].Tone;

            for (int j = i + 1; j < cards.Count; j++)
            {
                if (cards[j].Tone == tone) return true;
            }
        }

        return false;
    }
}
