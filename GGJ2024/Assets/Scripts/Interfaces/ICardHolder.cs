using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICardHolder
{
    public void ReceiveCard(CardBlueprint card);
    public void RemoveCard(CardBlueprint card);

}
