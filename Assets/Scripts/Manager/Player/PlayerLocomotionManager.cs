using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.LightAnchor;

public class PlayerLocomotionManager : CharacterLocomotionManager
{
    PlayerManager player;

    [HideInInspector] public float verticalMovement;
    [HideInInspector] public float horizontalMovement;
    [HideInInspector] public float moveAmount;

    private Vector3 moveDirection;//移动方向
    private Vector3 targetDirection;

    [Header("Movement Settings")]
    [SerializeField] float walkingSpeed = 2;
    [SerializeField] float runningSpeed = 5;
    [SerializeField] float sprintSpeed = 6.5f;
    [SerializeField] float rotationSpeed = 15;
    [SerializeField] float sprintingStaminaCost = 2;

    [Header("Jump")]
    [SerializeField] float jumpStaminaCost = 5;
    [SerializeField] float jumpHeight = 4;
    [SerializeField] float jumpForwardSpeed = 5;
    [SerializeField] float freeFallSpeed = 2;
    protected Vector3 jumpDirection;

    [Header("Dodge")]
    [SerializeField] Vector3 rollDirection;
    [SerializeField] float dodgeStaminaCost = 2;

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

        HandleJumpingMovement();
        HandleFreeFallMovement();
    }
    /// <summary>
    /// 拿到位移值
    /// </summary>
    private void GetMovementValues()
    {
        verticalMovement = PlayerInputManager.instance.verticalInput;
        horizontalMovement = PlayerInputManager.instance.horizontalInput;
        moveAmount = PlayerInputManager.instance.moveAmount;
    }
    private void HandleGroundedMovement()
    {
        if (!player.canMove)
            return;
        moveDirection = PlayerCamera.instance.transform.forward * verticalMovement;
        moveDirection += PlayerCamera.instance.transform.right * horizontalMovement;
        moveDirection.Normalize();
        moveDirection.y = 0;

        bool isSprinting;
        if(player.isSprinting)
        {
            player.characterController.Move(moveDirection * sprintSpeed * 1.3f * Time.deltaTime);
            isSprinting = true;
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
            isSprinting = false;
        }
        player.playerAnimatorManager.UpdateAnimatorMovementParameters(0, moveAmount, isSprinting);
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
        if(player.isPerformingAction)
        {
            player.isSprinting = false;
        }
        if(player.currentStamina <=0)
        {
            player.isSprinting = false;
        }
        if(moveAmount >= 0.5)
        {
            //避免原地冲刺
            player.isSprinting = true;
        }
        else
        {
            player.isSprinting = false;
        }

        if(player.isSprinting)
        {
            if(player.currentStamina < 0)
            {
                PlayerInputManager.instance.sprintInput = false;
            }
            player.currentStamina -= sprintingStaminaCost * Time.deltaTime;

            player.playerNetworkManager.SetCurrentStaminaValue(player.currentStamina);
        }
    }
    public void AttemptToPerformDodge()
    {
        if (player.isPerformingAction)
            return;
        if (player.currentStamina < 0)
            return;

        if(moveAmount > 0)
        {
            rollDirection = PlayerCamera.instance.cameraObject.transform.forward * verticalMovement;
            rollDirection += PlayerCamera.instance.cameraObject.transform.right * horizontalMovement;
            rollDirection.Normalize();
            rollDirection.y = 0;

            Quaternion playerRotation = Quaternion.LookRotation(rollDirection);
            player.transform.rotation = playerRotation;
            //向前
            player.playerAnimatorManager.PlayerTargetActionAnimation("Roll_Forward_short", true ,true);
        }
        else
        {
            player.playerAnimatorManager.PlayerTargetActionAnimation("Jump_Backward", true, true);
        }
        player.currentStamina -= dodgeStaminaCost;

        player.playerNetworkManager.SetCurrentStaminaValue(player.currentStamina);
    }

    public void AttemptToPerformJump()
    {
        if (player.isPerformingAction)
            return;
        if (player.currentStamina <= 0)
            return;
        if(player.isJumping) 
            return;
        if (!player.isGrounded)
            return;

        player.playerAnimatorManager.PlayerTargetActionAnimation("Main_Jump_Start", true);

        player.isJumping = true;

        player.currentStamina -= jumpStaminaCost;

        jumpDirection = PlayerCamera.instance.cameraObject.transform.forward * verticalMovement;
        jumpDirection += PlayerCamera.instance.gameObject.transform.right * horizontalMovement;
        jumpDirection.y = 0;
        if (jumpDirection != Vector3.zero)
        {
            if (player.isSprinting)
            {
                jumpDirection *= 1;
            }
            else if (PlayerInputManager.instance.moveAmount > 0.5f)
            {
                jumpDirection *= 0.5f;
            }
            else if (PlayerInputManager.instance.moveAmount <= 0.5f)
            {
                jumpDirection *= 0.25f;
            }
        }
    }
    private void HandleJumpingMovement()
    {
        if (player.isJumping)
        {
            player.characterController.Move(jumpDirection * jumpForwardSpeed * Time.deltaTime);
        }
    }
    private void HandleFreeFallMovement()
    {
        if (!player.isGrounded)
        {
            Vector3 freeFallDirection;

            freeFallDirection = PlayerCamera.instance.transform.forward * verticalMovement;
            freeFallDirection = freeFallDirection + PlayerCamera.instance.transform.right * horizontalMovement;
            freeFallDirection.y = 0;

            player.characterController.Move(freeFallDirection * freeFallSpeed * Time.deltaTime);
        }
    }



    //动画的event事件 Main Jump Up
    public void ApplyJumpingVelocity()
    {
        yVelocity.y = Mathf.Sqrt(jumpHeight * -2 * gravityForce); //V² = 2gh
    }
}
