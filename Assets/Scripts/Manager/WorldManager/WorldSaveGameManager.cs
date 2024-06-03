using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YooAsset;

public class WorldSaveGameManager : MonoBehaviour
{
    public static WorldSaveGameManager instance;

    public bool isSave;

    public PlayerManager player;

    [Header("保存角色写入器")]
    private SaveFileDataWniter saveFileDataWriter;

    [Header("当前角色数据")]
    public CharacterSaveData currentCharacterData;
    public CharacterSlot currentCharacterSlotBeingUsed;

    [Header("角色插槽")]
    public CharacterSaveData characterSlot01;
    public CharacterSaveData characterSlot02;
    public CharacterSaveData characterSlot03;
    public CharacterSaveData characterSlot04;
    public CharacterSaveData characterSlot05;
    public CharacterSaveData characterSlot06;
    public CharacterSaveData characterSlot07;
    public CharacterSaveData characterSlot08;

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
        LoadAllCharacterProfiles();
    }
    private void Update()
    {
        if(isSave)
        {
            isSave = false;
            SaveGame();
        }
    }
    /// <summary>
    /// 尝试创建一个新的游戏
    /// </summary>
    public void AttempToCreatNewGame()
    {
        saveFileDataWriter = new SaveFileDataWniter();

       var saveDataDirectoryPath = Application.dataPath;
       var saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_01);

        if(!saveFileDataWriter.CheckToSeeIfFileExists(saveDataDirectoryPath, saveFileName))
        {
            currentCharacterSlotBeingUsed = CharacterSlot.CharacterSlot_01;
            currentCharacterData = new CharacterSaveData();
            NewGame();
            return;
        }
    }
    /// <summary>
    /// 根据插槽名字决定文件名
    /// </summary>
    /// <returns></returns>
    public string DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot characterSlot)
    {
        string fileName = "";
        switch (characterSlot)
        {
            case CharacterSlot.CharacterSlot_01:
                fileName = "CharacterSlot_01";
                break;
            case CharacterSlot.CharacterSlot_02:
                fileName = "CharacterSlot_02";
                break;
            case CharacterSlot.CharacterSlot_03:
                fileName = "CharacterSlot_03";
                break;
            case CharacterSlot.CharacterSlot_04:
                fileName = "CharacterSlot_04";
                break;
        }
        return fileName;
    }

    private void NewGame()
    {
        StartCoroutine(LoadWorldScene());
    }
    public void SaveGame()
    {
        saveFileDataWriter = new SaveFileDataWniter();

        var saveDataDirectoryPath = Application.persistentDataPath;
        var saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(currentCharacterSlotBeingUsed);

        player.SaveGameDataToCurrentCharacterData(ref currentCharacterData);
        //写入Json文件，保存到机器
        saveFileDataWriter.CreateNewCharacterSaveFile(currentCharacterData ,saveDataDirectoryPath ,saveFileName);
    }
    public void LoadGame()
    {
        saveFileDataWriter = new SaveFileDataWniter();

        var saveDataDirectoryPath = Application.persistentDataPath;
        var saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(currentCharacterSlotBeingUsed);

        currentCharacterData = saveFileDataWriter.LoadSaveFile(saveDataDirectoryPath,saveFileName);

        StartCoroutine(LoadWorldScene());
    }
    /// <summary>
    /// 读取所以的存档配置文件
    /// </summary>
    private void LoadAllCharacterProfiles()
    {
        saveFileDataWriter = new SaveFileDataWniter();

        var saveDataDirectoryPath = Application.persistentDataPath;

        var saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_01);
        if (saveFileDataWriter.CheckToSeeIfFileExists(saveDataDirectoryPath, saveFileName))
            characterSlot01 = saveFileDataWriter.LoadSaveFile(saveDataDirectoryPath, saveFileName);

        saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_02);
        if (saveFileDataWriter.CheckToSeeIfFileExists(saveDataDirectoryPath, saveFileName))
            characterSlot02 = saveFileDataWriter.LoadSaveFile(saveDataDirectoryPath, saveFileName);

        saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_03);
        if (saveFileDataWriter.CheckToSeeIfFileExists(saveDataDirectoryPath, saveFileName))
            characterSlot03 = saveFileDataWriter.LoadSaveFile(saveDataDirectoryPath, saveFileName);

        saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_04);
        if (saveFileDataWriter.CheckToSeeIfFileExists(saveDataDirectoryPath, saveFileName))
            characterSlot04 = saveFileDataWriter.LoadSaveFile(saveDataDirectoryPath, saveFileName);
    }
    public void DeleteGame(CharacterSlot characterSlot)
    {
        saveFileDataWriter = new SaveFileDataWniter();

        var saveDataDirectoryPath = Application.persistentDataPath;
        var saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

        saveFileDataWriter.DeleteSaveFile(saveDataDirectoryPath , saveFileName);
    }
    private IEnumerator LoadWorldScene()
    {
        YooAssets.LoadSceneAsync("Assets/Scenes/GameScene.unity", LoadSceneMode.Single);

        if (player.isOwned && player.isServer)
            player.LoadGameDataToCurrentCharacterData(ref currentCharacterData);

        yield return null;
    }
}
