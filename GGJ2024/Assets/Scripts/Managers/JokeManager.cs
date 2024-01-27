using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class JokeManager : ManagerBehaviour
{
    [SerializeField] private int jokeSize = 5;
    [SerializeField] private List<SelectedCardDisplay> cardDisplay;

    private List<CardBlueprint> cardsInJoke = new List<CardBlueprint>();


    public bool CanReceiveCard(CardBlueprint card)
    {
        if (cardsInJoke.Count < jokeSize)
        {
            var display = cardDisplay[cardsInJoke.Count];
            SetDisplay(display, card);

            cardsInJoke.Add(card);

            return true;
        }
        else return false;
    }

    public void RemoveCard(CardBlueprint card)
    {
        if (!cardsInJoke.Contains(card))
        {
            Debug.LogError("The selected card is not in the joke.");
        }

        var index = cardsInJoke.IndexOf(card);
        cardDisplay[index].gameObject.SetActive(false);
        cardsInJoke.RemoveAt(index);
    }

    public void SetDisplay(SelectedCardDisplay display, CardBlueprint card)
    {
        display.theme.sprite = card.Theme.Icon;
        display.tone.text = card.Tone.Value.ToString();
        display.gameObject.transform.SetAsLastSibling();
        display.gameObject.SetActive(true);
    }
}

[System.Serializable]
public struct SelectedCardDisplay
{
    public GameObject gameObject;
    public Image theme;
    public TextMeshProUGUI tone;
}