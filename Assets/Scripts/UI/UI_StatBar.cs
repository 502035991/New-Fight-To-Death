using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_StatBar : MonoBehaviour
{
    private Slider slider;
    protected RectTransform rectTransform;

    [Header("СЎПо")]
    [SerializeField] protected bool scaleBarLengthWithStats = true;
    [SerializeField] protected float widthScaleMultiplier = 1;

    protected virtual void Awake()
    {
        slider = GetComponent<Slider>();
        rectTransform = GetComponent<RectTransform>();
    }

    public virtual void SetStat(float newValue)
    {
        slider.value = newValue;
    }
    public virtual void SetMaxStat(int maxVlaue)
    {
        slider.maxValue = maxVlaue;
        slider.value = maxVlaue;
        if (scaleBarLengthWithStats)
        {
            rectTransform.sizeDelta = new Vector2(maxVlaue * widthScaleMultiplier, rectTransform.sizeDelta.y);

            PlayerUIManager.instance.PlayerUIHudManager.RefreshHUD();
        }
    }
}
