using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CardBlueprint : MonoBehaviour, IBlueprint, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private List<Image> ThemeImages;
    [SerializeField] private List<TextMeshProUGUI> ToneValues;
    [Header("Placeholder")]
    [SerializeField] private GameObject outline;

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

        Suit = config.theme;
        Value = config.tone;

        ThemeImages.ForEach(x => x.sprite = Suit.Icon);
        ToneValues.ForEach(x => x.text = Value.Value.ToString());
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
        if (!selected)
        {
            if (jokeManager.CanReceiveCard(this))
            {
                outline.SetActive(true);
                selected = true;
            }
        } else
        {
            Unselect();
        }
    }

    private void Unselect()
    {
        if (!selected) return;

        jokeManager.RemoveCard(this);
        outline.SetActive(false);
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
}
