using Mirror;
using System.Collections;
using System.Collections.Generic;

public class PlayerNetworkManager : CharacterNetworkManager
{
    PlayerManager player;
    protected override void Awake()
    {
        base.Awake();
        player = GetComponent<PlayerManager>();
    }
    #region ÄÍÁ¦
    [Command]
    public override void SetNewEnduranceValue(int endurance)
    {
        if (isServer)
        {
            base.endurance = endurance;
            maxStamina = player.characterStatsManager.CalculateStaminaBasedOnEndurancelevel(endurance);
            currentStamina = maxStamina;
        }
    }
    [Command]
    public override void SetCurrentStaminaValue(float newValue)
    {
        if (isServer)
        {
            currentStamina = newValue;
        }
    }
    public override void OnStaminaChanged(float oldValue, float newValue)
    {
        if (!isOwned)
            return;
        player.characterStatsManager.ResetStaminaRegenerationTimer(oldValue, newValue);
        PlayerUIManager.instance.PlayerUIHudManager.SetNewStaminaValue(oldValue,newValue);
    }
    public override void OnEnduranceChanged(int oldValue, int newValue)
    {
        if (!isOwned)
            return;
        int maxStaminaValue = player.characterStatsManager.CalculateStaminaBasedOnEndurancelevel(newValue);
        PlayerUIManager.instance.PlayerUIHudManager.SetMaxStaminaValue(maxStaminaValue);
    }
    #endregion
    #region ÑªÁ¿
    [Command]
    public override void SetNewVitalityValue(int vitality)
    {
        if (isServer)
        {
            base.Vitality = vitality;
            maxHealth = player.characterStatsManager.CalculateHealthBasedOnVitalityLevel(vitality);
            currentHealth = maxHealth;
        }
    }
    [Command]
    public override void SetCurrentHealthValue(float currentHealth)
    {
        if (isServer)
        {
            base.currentHealth = currentHealth;
        }
    }
    public override void OnCurrentHealthChanged(float oldValue, float newValue)
    {
        if (!isOwned)
            return;
        player.characterStatsManager.ResetStaminaRegenerationTimer(oldValue, newValue);
        PlayerUIManager.instance.PlayerUIHudManager.SetNewHealthValue(oldValue, newValue);
    }
    public override void OnVitalityChange(int oldValue, int newValue)
    {
        if (!isOwned)
            return;
        int maxHealthValue = player.characterStatsManager.CalculateHealthBasedOnVitalityLevel(newValue);
        PlayerUIManager.instance.PlayerUIHudManager.SetMaxHealthValue(maxHealthValue);
    }
    #endregion
}
