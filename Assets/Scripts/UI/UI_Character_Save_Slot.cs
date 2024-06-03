using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Character_Save_Slot : MonoBehaviour
{
    SaveFileDataWniter saveFileWriter;

    private Button btn;

    private Action<CharacterSlot> getSlot;
    private string saveDataDirectoryPath;
    private string saveFileName;

    [Header("Game Slot")]
    public CharacterSlot characterSlot;

    [Header("Character Info")]
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI timedPlayed;

    private void Awake()
    {
        btn = GetComponent<Button>();
    }
    private void Start()
    {
        btn.onClick.AddListener(SelectCurretnSlot);
    }

    public void Init(int slotIndex, Action<CharacterSlot> _getSlotAction)
    {
        characterSlot = (CharacterSlot)slotIndex;
        getSlot = _getSlotAction;

        LoadSaveSlots();
    }
    private void LoadSaveSlots()
    {
        saveFileWriter = new SaveFileDataWniter();
        saveDataDirectoryPath = Application.persistentDataPath;

        if (characterSlot == CharacterSlot.CharacterSlot_01)
        {
            saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            if (saveFileWriter.CheckToSeeIfFileExists(saveDataDirectoryPath,saveFileName))
            {
                characterName.text = WorldSaveGameManager.instance.characterSlot01.characterName;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        else if (characterSlot == CharacterSlot.CharacterSlot_02)
        {
            saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            if (saveFileWriter.CheckToSeeIfFileExists(saveDataDirectoryPath, saveFileName))
            {
                characterName.text = WorldSaveGameManager.instance.characterSlot02.characterName;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        else if (characterSlot == CharacterSlot.CharacterSlot_03)
        {
            saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            if (saveFileWriter.CheckToSeeIfFileExists(saveDataDirectoryPath, saveFileName))
            {
                characterName.text = WorldSaveGameManager.instance.characterSlot03.characterName;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        else if (characterSlot == CharacterSlot.CharacterSlot_04)
        {
            saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            if (saveFileWriter.CheckToSeeIfFileExists(saveDataDirectoryPath, saveFileName))
            {
                characterName.text = WorldSaveGameManager.instance.characterSlot04.characterName;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
    private void SelectCurretnSlot()
    {
        getSlot?.Invoke(characterSlot);
    }
}
