using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerManager : CharacterManager
{
    [HideInInspector] public PlayerLocomotionManager playerLocomotionManager;
    [HideInInspector] public PlayerAnimatorManager playerAnimatorManager;
    [HideInInspector] public PlayerStatsManager playerStatsManager;
    [HideInInspector] public PlayerNetworkManager playerNetworkManager;

    protected override void Awake()
    {
        base.Awake();
        playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
        playerAnimatorManager =GetComponent<PlayerAnimatorManager>();
        playerStatsManager = GetComponent<PlayerStatsManager>();
        playerNetworkManager = GetComponent<PlayerNetworkManager>();
    }
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        if(isOwned)
        {
            WorldSaveGameManager.instance.player = this;
            PlayerCamera.instance.player = this;
            PlayerInputManager.instance.player = this;
        }

        if (isOwned && !isServer)
            LoadGameDataToCurrentCharacterData(ref WorldSaveGameManager.instance.currentCharacterData);
    }
    protected override void Update()
    {
        base.Update();
        if (!isOwned)
            return;
        playerLocomotionManager.HandleAllMovement();
        playerStatsManager.RegenerateStamina();
    }
    protected override void LateUpdate()
    {
        base.LateUpdate();
        if(!isOwned) 
            return;
        PlayerCamera.instance.ControlAllCameraActions();
    }
    public void SaveGameDataToCurrentCharacterData(ref CharacterSaveData currentCharacterData)
    {
        currentCharacterData.xPosition = transform.position.x;
        currentCharacterData.yPosition = transform.position.y;
        currentCharacterData.zPosition = transform.position.z;

        //currentCharacterData.currentHealth = playerNetworkManager.currentHealth.Value;
        currentCharacterData.currentStamina = playerNetworkManager.currentStamina;

        //currentCharacterData.vitality = playerNetworkManager.vitality.Value;
        currentCharacterData.endurance = playerNetworkManager.endurance;
    }
    public void LoadGameDataToCurrentCharacterData(ref CharacterSaveData currentCharacterData)
    {
        Vector3 myPos = new Vector3(currentCharacterData.xPosition, currentCharacterData.yPosition, currentCharacterData.zPosition);
        transform.position = myPos;

        endurance = currentCharacterData.endurance;
        vitality = currentCharacterData.vitality;

        playerNetworkManager.SetNewEnduranceValue(endurance);
        playerNetworkManager.SetNewVitalityValue(vitality);

        maxStamina = playerStatsManager.CalculateStaminaBasedOnEndurancelevel(endurance);
        maxHealth = playerStatsManager.CalculateHealthBasedOnVitalityLevel(vitality);

        currentStamina = currentCharacterData.currentStamina;
        currentHealth = currentCharacterData.currentHealth;

        PlayerUIManager.instance.PlayerUIHudManager.SetMaxStaminaValue(maxStamina);
        PlayerUIManager.instance.PlayerUIHudManager.SetNewStaminaValue(0, currentStamina <= 0 ? maxStamina : currentStamina);

        PlayerUIManager.instance.PlayerUIHudManager.SetMaxHealthValue(maxHealth);
        PlayerUIManager.instance.PlayerUIHudManager.SetNewStaminaValue(0, currentHealth <= 0 ? maxHealth : currentHealth);
    }
}
