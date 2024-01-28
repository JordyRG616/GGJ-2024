using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class RememberedCardHolder : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image bg;
    [SerializeField] private Sprite selectedSprite;
    [Space]
    [SerializeField] private Image value;
    [SerializeField] private Image suit;
    [SerializeField] private GameObject display;

    private CardBlueprint cachedCard;
    private JokeManager jokeManager;
    private Animator animator;
    private bool selected;


    private void Start()
    {
        jokeManager = GameMaster.GetManager<JokeManager>();
        animator = GetComponent<Animator>();
        CardBlueprint.OnCardSelectedToRemember += ReceiveCard;

        GameMaster.GetManager<GameFlowManager>().OnTurnEnd += Clear;
    }

    private void ReceiveCard(CardBlueprint card)
    {
        cachedCard = card;

        value.sprite = cachedCard.Value.Icon;
        suit.sprite = cachedCard.Suit.Icon;
        display.SetActive(true);
        card.gameObject.SetActive(false);
        animator.SetBool("Active", false);
        bg.overrideSprite = null;
    }

    public void Interact()
    {
        if (cachedCard == null)
        {
            CardBlueprint.SelectingCardToRemember = true;
            animator.SetBool("Active", true);
            bg.overrideSprite = selectedSprite;
        }
        else
        {
            if (!selected)
            {
                if (jokeManager.CanReceiveCard(cachedCard))
                {
                    selected = true;
                    bg.overrideSprite = selectedSprite;
                }
            } else
            {
                jokeManager.RemoveCard(cachedCard);
                bg.overrideSprite = null;
                selected = false;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Interact();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = Vector3.one * 1.1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!selected) transform.localScale = Vector3.one;
    }

    public void Clear()
    {
        if (selected)
        {
            jokeManager.RemoveCard(cachedCard);
            selected = false;
            bg.overrideSprite = null;
        }

        cachedCard = null;
        display.SetActive(false);
    }
}
