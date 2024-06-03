using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInputManager : MonoBehaviour
{ 
    private bool isDelete;

    PlayerControls playerControls;

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.UI.Delete.performed += i => isDelete = true;
        }
        playerControls.Enable();
    }

     private void Update()
    {
        DeleteLoadData();
    }
    private void DeleteLoadData()
    {
        if(isDelete)
        {
            isDelete = false;
        }
    }
}
