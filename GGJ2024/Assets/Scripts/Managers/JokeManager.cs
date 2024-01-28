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
    private AudienceManager audienceManager;
    private int cardsFromAudience;
    private int cardsFromHand;


    private void Start()
    {
        audienceManager = GameMaster.GetManager<AudienceManager>();
    }

    public bool CanReceiveCard(CardBlueprint card)
    {
        if (cardsInJoke.Count < jokeSize)
        {
            var display = cardDisplay.Find(x => x.active == false);
            display.SetActive(card);

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
            return;
        }

        var display = cardDisplay.Find(x => x.cachedCard == card);
        display.SetInactive();
        cardsInJoke.Remove(card);
    }


    public void EvaluateJoke()
    {
        var toldJoke = false;

        for (int i = 0; i < avaliators.Count; i++)
        {
            var avaliator = avaliators[i];
            if (avaliator.Fulfilled(cardsInJoke))
            {
                audienceManager.EvaluateToleranceChange(avaliator.toleranceReward);
                toldJoke = true;
                break;
            }
        }

        if(!toldJoke)
        {
            audienceManager.ApplyToleranceHit();
        }
    }
}

[System.Serializable]
public class SelectedCardDisplay
{
    public GameObject gameObject;
    public Image suit;
    public Image value;
    public CardBlueprint cachedCard;
    public bool active;


    public void SetActive(CardBlueprint card)
    {
        cachedCard = card;
        active = true;

        suit.sprite = card.Suit.Icon;
        value.sprite = card.Value.Icon;
        gameObject.transform.SetAsLastSibling();
        gameObject.SetActive(true);
    }

    public void SetInactive()
    {
        active = false;
        gameObject.SetActive(false);
    }
}