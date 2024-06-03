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
    [Command]
    public override void SetNewEnduranceValue(int newValue)
    {
        if (isServer)
        {
            endurance = newValue;
            maxStamina = player.characterStatsManager.CalculateStaminaBasedOnEndurancelevel(newValue);
            currentStamina = maxStamina;
        }
    }
    [Command]
    public override void SetMaxStaminaValue(int newValue)
    {
        if (isServer)
        {
            maxStamina = newValue;
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
/*        SetMaxStaminaValue(maxStaminaValue);
        SetCurrentStaminaValue(maxStaminaValue);*/
        player.playerStatsManager.maxStamina = maxStaminaValue;
        player.playerStatsManager.currentStamina = maxStaminaValue;

        PlayerUIManager.instance.PlayerUIHudManager.SetMaxStaminaValue(maxStaminaValue);
    }
}
