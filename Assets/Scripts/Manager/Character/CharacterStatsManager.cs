using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsManager : MonoBehaviour
{
    CharacterManager character;
    
    [Header("��������")]
    [SerializeField] int staminaRegenerationAmount = 2;
    private float staminaRegenerationTimer = 0;//�����ظ���ʱ��
    private float staminaTickTimer = 0;
    [SerializeField] float staminaRegenerationDelay = 1;

    public bool isSprinting;
    public float currentStamina;
    public int maxStamina;
    public int endurance;

    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();
    }
    protected virtual void Start()
    {
    }
    /// <summary>
    /// ���������ȼ���������
    /// </summary>
    public int CalculateHealthBasedOnVitalityLevel(int vitality)
    {
        float health = 0;

        health += vitality * 15;

        return Mathf.RoundToInt(health);
    }
    /// <summary>
    /// ����������������
    /// </summary>
    public int CalculateStaminaBasedOnEndurancelevel(float endurance)
    {
        float stamina = 0;

        stamina += endurance * 10;

        return Mathf.RoundToInt(stamina);
    }
    public virtual void RegenerateStamina()
    {
        if (isSprinting)
            return;

        staminaRegenerationTimer += Time.deltaTime;
        //ֹͣ���������� staminaRegenerationDelay ��ظ�
        if (staminaRegenerationTimer >= staminaRegenerationDelay)
        {
            if (currentStamina < maxStamina)
            {
                var oldStamina = currentStamina;
                currentStamina += staminaRegenerationAmount;
                character.characterNetworkManager.SetCurrentStaminaValue(currentStamina);
                PlayerUIManager.instance.PlayerUIHudManager.SetNewStaminaValue(oldStamina, currentStamina);

                /*                staminaTickTimer += Time.deltaTime;
                                if (staminaTickTimer >= 0.05f)
                                {
                                    staminaTickTimer = 0;
                                    character.characterNetworkManager.currentStamina += staminaRegenerationAmount;
                                }*/
            }
        }
    }
    public virtual void ResetStaminaRegenerationTimer(float oldValue , float newValue)
    {
        //���ü�ʱ��
        if (newValue < oldValue)
            staminaRegenerationTimer = 0;
    }
}
