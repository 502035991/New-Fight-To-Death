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

        //�ں��� time ���ʱ����ڣ�ʹ����Ϊ animation �Ķ������룬ʹ��������������
        character.anim.CrossFade(targetAnimation, 0.2f);
        //���������������λ�ƣ������ｺ��������
        character.applyRootMotion = applyRootMotion;
        //Ϊfales������������
        character.isPerformingAction = isPerformingAction;
        character.canRotate = canRotate;
        character.canMove = canMove;

        //�ͻ��˵Ķ��������͵���������
        //character.characterNetworkManager.NotifyTheServerOfACtionAnimationServerRpc(NetworkManager.Singleton.LocalClientId, targetAnimation, applyRootMotion);
    }
}
