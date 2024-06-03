using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIHudManager : MonoBehaviour
{
    [SerializeField] UI_StatBar healthBar;
    [SerializeField] UI_StatBar staminaBar;

    VerticalLayoutGroup layoutGroup;
    private void Awake()
    {
        layoutGroup = GetComponentInChildren<VerticalLayoutGroup>();
    }
    public void RefreshHUD()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(layoutGroup.GetComponent<RectTransform>());
    }
    #region ÑªÁ¿
    public void SetNewHealthValue(int oldValue, int newValue)
    {
        healthBar.SetStat(newValue);
    }
    public void SetMaxHealthValue(int maxHealth)
    {
        healthBar.SetMaxStat(maxHealth);
    }
    #endregion
    #region ÄÍÁ¦
    public void SetNewStaminaValue(float oldValue, float newValue)
    {
        staminaBar.SetStat(newValue);
    }
    public void SetMaxStaminaValue(int maxStamina)
    {
        staminaBar.SetMaxStat(maxStamina);
    }
    #endregion
}
