using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour
{
    public static MainPanel instance;

    [Header("Buttons")]
    [SerializeField] Button pressStartButton;
    [SerializeField] Button newGameButton;

    [Header("Menus")]
    [SerializeField] GameObject titlePressStartMenu;//第一个界面
    [SerializeField] GameObject titleScreenMainMenu;//开始菜单界面
    [SerializeField] GameObject titleScreenLoadMenu;//load界面

    [Header("Save Slots")]
    public CharacterSlot currentCharacterSlot = CharacterSlot.NO_SLOT;
    public UI_Character_Save_Slot SaveSlot;
    public RectTransform saveSlotsParent;
    

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        pressStartButton.onClick.AddListener(StartNetworkAsHost);
        newGameButton.onClick.AddListener(StartBtnFunction);

        InitializedSaveSlots();
    }
    private void StartNetworkAsHost()
    {
/*        if (ParrelSync.ClonesManager.IsClone())
        {
            NetworkManager.singleton.StopHost();

            NetworkManager.singleton.StartClient();
        }
        else
        {
            NetworkManager.singleton.StartHost();
        }
*/
        /*        NetworkManager.singleton.StartHost();
                NetworkManager.singleton.StartClient();*/
    }
    private void StartBtnFunction()
    {
        WorldSaveGameManager.instance.AttempToCreatNewGame();
    }
    #region LoadScreen
    private void InitializedSaveSlots()
    {
        int enumCount = Enum.GetValues(typeof(CharacterSlot)).Length;
        for(int slotIndex = 1; slotIndex < enumCount; slotIndex++)
        {
            UI_Character_Save_Slot slot = Instantiate(SaveSlot);
            slot.transform.SetParent(saveSlotsParent);

            slot.Init(slotIndex , GetCurrentSlot);
        }
    }
    private void GetCurrentSlot(CharacterSlot slot)
    {
        currentCharacterSlot = slot;

        WorldSaveGameManager.instance.currentCharacterSlotBeingUsed = currentCharacterSlot;
        WorldSaveGameManager.instance.LoadGame();
    }
    #endregion
}
