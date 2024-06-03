using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerLocomotionManager : CharacterLocomotionManager
{
    PlayerManager player;

    [HideInInspector] public float verticalMovement;
    [HideInInspector] public float horizontalMovement;
    [HideInInspector] public float moveAmount;

    [HideInInspector] public bool isSprint;
    private Vector3 moveDirection;//移动方向
    private Vector3 targetDirection;

    [Header("Movement Settings")]
    [SerializeField] float walkingSpeed = 2;
    [SerializeField] float runningSpeed = 5;
    [SerializeField] float sprintSpeed = 6.5f;
    [SerializeField] float rotationSpeed = 15;
    [SerializeField] float sprintingStaminaCost = 2;

    protected override void Awake()
    {
        base.Awake();
        player = GetComponent<PlayerManager>();
    }
    public void HandleAllMovement()
    {
        GetMovementValues();
        HandleGroundedMovement();
        HandleRotation();
    }
    /// <summary>
    /// 拿到位移值
    /// </summary>
    private void GetMovementValues()
    {
        verticalMovement = PlayerInputManager.instance.verticalInput;
        horizontalMovement = PlayerInputManager.instance.horizontalInput;
        moveAmount = PlayerInputManager.instance.moveAmount;

        isSprint = PlayerInputManager.instance.sprintInput;
    }
    private void HandleGroundedMovement()
    {
        if (!player.canMove)
            return;
        moveDirection = PlayerCamera.instance.transform.forward * verticalMovement;
        moveDirection += PlayerCamera.instance.transform.right * horizontalMovement;
        moveDirection.Normalize();
        moveDirection.y = 0;

        if(isSprint)
        {
            player.characterController.Move(moveDirection * sprintSpeed * 1.3f * Time.deltaTime);
            player.playerAnimatorManager.UpdateAnimatorMovementParameters(0, moveAmount, true);
        }
        else
        {
            if (moveAmount == 1)
            {
                player.characterController.Move(moveDirection * runningSpeed * Time.deltaTime);
            }
            else if (moveAmount == 0.5f)
            {
                player.characterController.Move(moveDirection * walkingSpeed * Time.deltaTime);
            }
            player.playerAnimatorManager.UpdateAnimatorMovementParameters(0, moveAmount, false);
        }     
    }
    private void HandleRotation()
    {
        if (!player.canRotate)
            return;
        targetDirection = Vector3.zero;
        targetDirection = PlayerCamera.instance.cameraObject.transform.forward * verticalMovement;
        targetDirection += PlayerCamera.instance.cameraObject.transform.right * horizontalMovement;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if(targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }

        Quaternion newRotation = Quaternion.LookRotation(targetDirection);
        Quaternion targetRotation = Quaternion.Lerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = targetRotation;
    }
    public void HandleSprinting()
    {
        player.playerStatsManager.currentStamina -=sprintingStaminaCost * Time.deltaTime;
    }
}
