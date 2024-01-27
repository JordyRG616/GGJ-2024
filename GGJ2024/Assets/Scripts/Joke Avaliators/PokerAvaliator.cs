using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Avaliators/Poker")]
public class PokerAvaliator : JokeAvaliator
{
    [SerializeField] private int pokerSize = 4;

    public override bool Fulfilled(List<CardBlueprint> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            var value = cards[i].Value;

            var matches = cards.FindAll(x => x.Value == value);
            if (matches.Count >= pokerSize) return true;
        }

        return false;
    }
}
