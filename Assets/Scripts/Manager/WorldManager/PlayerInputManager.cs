
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager instance;
    public PlayerManager player;
    PlayerControls playerControls;

    [Header("Movement Input")]
    private Vector2 movementInput;
    public float verticalInput;
    public float horizontalInput;
    public float moveAmount;

    [Header("camera input")]
    private Vector2 cameraInput;
    public float cameraVerticalInput;
    public float cameraHorizontalInput;

    [Header("Player Action Input")]
    public bool sprintInput;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += OnSceneChange;

        instance.enabled = false;
        if (playerControls != null)
            playerControls.Disable();
    }
    private void OnSceneChange(Scene oldScene, Scene newScene)
    {
        if (newScene.name == "GameScene")
        {
            instance.enabled = true;

            if (playerControls != null)
                playerControls.Enable();
        }
        else
        {
            instance.enabled = false;

            if (playerControls != null)
                playerControls.Disable();
        }
    }
    private void OnEnable()
    {
        if(playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerCamera.CameraControls.performed += i => cameraInput = i.ReadValue<Vector2>();

            playerControls.PlayerActions.Sprint.performed += i => sprintInput = true;
            playerControls.PlayerActions.Sprint.canceled += i => sprintInput = false;
        }
        playerControls.Enable();
    }
    private void Update()
    {
        HandleMovmentInput();
        HandleCameraMovementInput();
        HandleSprintInput();
    }
    //拿到移动值
    private void HandleMovmentInput()
    {
        horizontalInput = movementInput.x;
        verticalInput = movementInput.y;

        moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput));

        if (moveAmount <= 0.5f && moveAmount > 0)
        {
            moveAmount = 0.5f;
        }
        else if (moveAmount > 0.5 && moveAmount <= 1)
        {
            moveAmount = 1;
        }
    }
    //拿到相机移动值
    private void HandleCameraMovementInput()
    {
        cameraHorizontalInput = cameraInput.x;
        cameraVerticalInput = cameraInput.y;
    }
    //处理冲刺输入
    private void HandleSprintInput()
    {
        if(sprintInput)
        {
            player.playerLocomotionManager.HandleSprinting();
        }
    }
}
