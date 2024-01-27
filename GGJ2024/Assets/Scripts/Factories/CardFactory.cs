using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFactory : FactoryBehaviour
{
    [Space]
    [SerializeField] private List<SuitEnum> themes;
    [SerializeField] private List<ValueEnum> tones;


    protected override void Awake()
    {
        InitializeFactory<CardBlueprint>();

        base.Awake();
    }

    public List<CardBlueprint> CreateInitialDeck()
    {
        var container = new List<CardBlueprint>();
        var configuration = new CardConfiguration();

        foreach (var theme in themes)
        {
            configuration.suit = theme;

            foreach (var tone in tones)
            {
                configuration.value = tone;

                Create<CardBlueprint>(configuration, out var card);
                container.Add(card);
            }
        }

        return container;
    }
}

public class CardConfiguration : FactoryConfiguration
{
    public SuitEnum suit;
    public ValueEnum value;
}