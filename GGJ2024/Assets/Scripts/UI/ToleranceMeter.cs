using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class ToleranceMeter : MonoBehaviour
{
    private Image fill;


    private void Start()
    {
        fill = GetComponent<Image>();
        GameMaster.GetManager<AudienceManager>().OnToleranceChanged += UpdateFill;
    }

    private void UpdateFill(int current, int max)
    {
        fill.fillAmount = current / (float)max;
    }
}
