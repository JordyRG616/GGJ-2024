using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class JokeManager : ManagerBehaviour
{
    [SerializeField] private int jokeSize = 5;
    [SerializeField] private List<SelectedCardDisplay> cardDisplay;
    [Space]
    [SerializeField] private List<JokeAvaliator> avaliators;

    public bool CanTellJoke => cardDisplay.Count == jokeSize && cardsFromHand > 0 && cardsFromAudience > 0;

    private List<CardBlueprint> cardsInJoke = new List<CardBlueprint>();

    private int cardsFromHand;
    private int cardsFromAudience;


    private void Start()
    {
        GameMaster.GetManager<GameFlowManager>().OnTurnEnd += AvaliateJoke;
    }

    public bool CanReceiveCard(CardBlueprint card)
    {
        if (cardsInJoke.Count < jokeSize)
        {
            var display = cardDisplay[cardsInJoke.Count];
            SetDisplay(display, card);

            if (card.cardHolder is HandManager) cardsFromHand++;
            else cardsFromAudience++;

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

    private void AvaliateJoke()
    {
        for (int i = 0; i < avaliators.Count; i++)
        {
            var avaliator = avaliators[i];
            if (avaliator.Fulfilled(cardsInJoke)) Debug.Log("Fulfilled");
        }
    }
}

[System.Serializable]
public struct SelectedCardDisplay
{
    public GameObject gameObject;
    public Image theme;
    public TextMeshProUGUI tone;
}