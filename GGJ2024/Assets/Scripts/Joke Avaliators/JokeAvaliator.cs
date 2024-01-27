using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class JokeAvaliator : ScriptableObject
{
    [field:SerializeField] public int toleranceReward { get; protected set; }


    public abstract bool Fulfilled(List<CardBlueprint> cards);

}
