using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorManager : MonoBehaviour
{
    CharacterManager character;

    int horizontal;
    int vertical;

    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();

        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
    }
    protected virtual void Update()
    {
        character.anim.SetBool("isGrounded", character.isGrounded);
    }
    public void UpdateAnimatorMovementParameters(float horizontalValue, float verticalValue, bool isSprinting)
    {
        float horizontalAmount = horizontalValue;
        float verticalAmount = verticalValue;
        if (isSprinting)
        {
            verticalAmount = 2;
        }
        character.anim.SetFloat(horizontal, horizontalAmount, 0.1f, Time.deltaTime);
        character.anim.SetFloat(vertical, verticalAmount, 0.1f, Time.deltaTime);
    }
    public virtual void PlayerTargetActionAnimation(string targetAnimation, bool isPerformingAction, bool applyRootMotion = true, bool canRotate = false, bool canMove = false)
    {

        //在后续 time 秒的时间段内，使名称为 animation 的动画淡入，使其他动画淡出。
        character.anim.CrossFade(targetAnimation, 0.2f);
        //开启后，如果动画带位移，则人物胶囊体会跟随
        character.applyRootMotion = applyRootMotion;
        //为fales会打断其他动作
        character.isPerformingAction = isPerformingAction;
        character.canRotate = canRotate;
        character.canMove = canMove;

        //客户端的动画请求发送到服务器。
        //character.characterNetworkManager.NotifyTheServerOfACtionAnimationServerRpc(NetworkManager.Singleton.LocalClientId, targetAnimation, applyRootMotion);
    }
}
