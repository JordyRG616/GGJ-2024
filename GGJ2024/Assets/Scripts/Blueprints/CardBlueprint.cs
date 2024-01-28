using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;

public class CardBlueprint : MonoBehaviour, IBlueprint, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image bg;
    [Space]
    [SerializeField] private Image suitImage;
    [SerializeField] private Image valueImage;
    [SerializeField] private Sprite selectedSprite;
    [SerializeField] private AudioPlayer selectedSFX;
    [SerializeField] private AudioPlayer deselectedSFX;

    public static bool SelectingCardToRemember = false;
    public static System.Action<CardBlueprint> OnCardSelectedToRemember;

    public SuitEnum Suit { get; private set; }
    public ValueEnum Value { get; private set; }
    public ICardHolder cardHolder { get; private set; }

    private JokeManager jokeManager;
    private bool selected;


    private void Start()
    {
        jokeManager = GameMaster.GetManager<JokeManager>();
        GameMaster.GetManager<GameFlowManager>().OnTurnEnd += Unselect;
    }

    public void Configure(FactoryConfiguration configuration)
    {
        var config = configuration as CardConfiguration;

        Suit = config.suit;
        Value = config.value;

        suitImage.sprite = Suit.Icon;
        valueImage.sprite = Value.Icon;
    }

    public GameObject GetConcrete()
    {
        return gameObject;
    }

    public void SetHolder(ICardHolder holder)
    {
        cardHolder = holder;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (SelectingCardToRemember && cardHolder is HandManager)
        {
            SendToMemoryHolder();
        }
        else
        {
            SetSelected();
        }
    }

    private void SendToMemoryHolder()
    {
        OnCardSelectedToRemember?.Invoke(this);
        SelectingCardToRemember = false;
    }

    private void SetSelected()
    {
        if (!selected)
        {
            if (jokeManager.CanReceiveCard(this))
            {
                bg.overrideSprite = selectedSprite;
                selected = true;
                selectedSFX.Play();
            }
        }
        else
        {
            Unselect();
            deselectedSFX.Play();
        }
    }

    private void Unselect()
    {
        if (!selected) return;

        jokeManager.RemoveCard(this);
        bg.overrideSprite = null;
        selected = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = Vector3.one * 1.1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!selected) transform.localScale = Vector3.one;
    }

    public void SetFrameOrientation(Vector3 orientation)
    {
        bg.transform.localScale = orientation;

    }
}
