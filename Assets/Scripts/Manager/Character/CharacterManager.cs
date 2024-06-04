using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterManager : NetworkBehaviour
{
    [HideInInspector] public CharacterController characterController;
    [HideInInspector] public Animator anim;

    [HideInInspector] public CharacterNetworkManager characterNetworkManager;
    [HideInInspector] public CharacterStatsManager characterStatsManager;
    [HideInInspector] public CharacterEffectManager characterEffectManager;

    public bool isDead;
    [Header("变量")]
    public bool isPerformingAction = false;
    public bool isGrounded = false;
    public bool applyRootMotion = false;
    public bool canRotate = true;
    public bool canMove = true;
    public bool isSprinting = false;
    public bool isJumping = false;

    [Header("当前状态")]
    public float currentStamina;
    public int maxStamina;
    public int endurance;
    public float currentHealth;
    public int maxHealth;
    public int vitality;

    protected virtual void Awake()
    {
        DontDestroyOnLoad(this);
        
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        characterNetworkManager = GetComponent<CharacterNetworkManager>();
        characterStatsManager = GetComponent<CharacterStatsManager>();
        characterEffectManager = GetComponent<CharacterEffectManager>();
    }
    protected virtual void Update()
    {
    }
    protected virtual void LateUpdate()
    {

    }
}
