using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsManager : MonoBehaviour
{
    CharacterManager character;
    
    [Header("耐力再生")]
    [SerializeField] int staminaRegenerationAmount = 2;
    private float staminaRegenerationTimer = 0;//耐力回复计时器
    private float staminaTickTimer = 0;
    [SerializeField] float staminaRegenerationDelay = 1;


    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();
    }
    protected virtual void Start()
    {
    }
    /// <summary>
    /// 根据体力等级计算体力
    /// </summary>
    public int CalculateHealthBasedOnVitalityLevel(int vitality)
    {
        float health = 0;

        health += vitality * 15;

        return Mathf.RoundToInt(health);
    }
    /// <summary>
    /// 根据体力计算耐力
    /// </summary>
    public int CalculateStaminaBasedOnEndurancelevel(float endurance)
    {
        float stamina = 0;

        stamina += endurance * 10;

        return Mathf.RoundToInt(stamina);
    }
    public virtual void RegenerateStamina()
    {
        if (character.isSprinting)
            return;

        staminaRegenerationTimer += Time.deltaTime;
        //停止降低耐力后 staminaRegenerationDelay 后回复
        if (staminaRegenerationTimer >= staminaRegenerationDelay)
        {
            if (character.currentStamina < character.maxStamina)
            {
                //PlayerUIManager.instance.PlayerUIHudManager.SetNewStaminaValue(oldStamina, character.currentStamina);

                staminaTickTimer += Time.deltaTime;
                if (staminaTickTimer >= 0.02f)
                {
                    staminaTickTimer = 0;
                    character.currentStamina += staminaRegenerationAmount;
                    character.characterNetworkManager.SetCurrentStaminaValue(character.currentStamina);
                }
            }
        }
    }
    public virtual void ResetStaminaRegenerationTimer(float oldValue , float newValue)
    {
        //重置计时器
        if (newValue < oldValue)
            staminaRegenerationTimer = 0;
    }
}
