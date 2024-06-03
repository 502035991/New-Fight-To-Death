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
        
    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();
    }

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

}
