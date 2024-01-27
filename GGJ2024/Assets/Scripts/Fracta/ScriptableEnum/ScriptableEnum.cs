using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableEnum : ScriptableObject
{
    [field: SerializeField] public Sprite Icon { get; protected set; }
    [field: SerializeField] public int Value { get; protected set; }
}
