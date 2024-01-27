using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Avaliators/Triple")]
public class TripleAvaliator : JokeAvaliator
{
    public override bool Fulfilled(List<CardBlueprint> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            var value = cards[i].Value;

            var matches = cards.FindAll(x => x.Value == value);
            if (matches.Count >= 3) return true;
        }

        return false;
    }
}
