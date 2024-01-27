using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class JokeAvaliator : ScriptableObject
{
    public abstract bool Fulfilled(List<CardBlueprint> cards);

}
