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

    [Header("±‰¡ø")]
    public bool isPerformingAction = false;
    public bool isGrounded = false;
    public bool applyRootMotion = false;
    public bool canRotate = true;
    public bool canMove = true;

    protected virtual void Awake()
    {
        DontDestroyOnLoad(this);
        
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        characterNetworkManager = GetComponent<CharacterNetworkManager>();
        characterStatsManager = GetComponent<CharacterStatsManager>();
    }
    protected virtual void Update()
    {
    }
    protected virtual void LateUpdate()
    {

    }
}
