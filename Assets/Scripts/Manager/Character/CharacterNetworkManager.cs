using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterNetworkManager : NetworkBehaviour
{
    CharacterManager character;

    [SyncVar]
    public int maxStamina;
    [SyncVar(hook =nameof(OnStaminaChanged))]
    public float currentStamina = -1;
    [SyncVar(hook =nameof(OnEnduranceChanged))]
    public int endurance;//体力:增加耐力值

    [SyncVar]
    public int maxHealth;
    [SyncVar(hook = nameof(OnCurrentHealthChanged))]
    public float currentHealth;
    [SyncVar(hook = nameof(OnVitalityChange))]
    public int Vitality;
        
    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();
    }

    #region 耐力
    [Command]
    public virtual void SetCurrentStaminaValue(float newValue)
    {
        currentStamina = newValue;
    }
    [Command]
    public virtual void SetNewEnduranceValue(int newValue)
    {
        endurance = newValue;
    }
    public virtual void OnStaminaChanged(float oldValue, float newValue)
    {
    }
    public virtual void OnEnduranceChanged(int oldValue, int newValue)
    {
    }
    #endregion
    #region 血量
    [Command]
    public virtual void SetNewVitalityValue(int vitality)
    {
    }
    [Command]
    public virtual void SetCurrentHealthValue(int newValue)
    {
        endurance = newValue;
    }
    public virtual void OnVitalityChange(int oldValue, int newValue)
    {

    }
    public virtual void OnCurrentHealthChanged(float oldValue, float newValue)
    {

    }
    #endregion
}
