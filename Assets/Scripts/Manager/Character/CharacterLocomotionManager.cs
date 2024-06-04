using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLocomotionManager : MonoBehaviour
{
    CharacterManager character;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] float groundCheckSphereRadius = 1;
    [SerializeField] protected Vector3 yVelocity;
    [SerializeField] protected float gravityForce = -5.55f;
    [SerializeField] protected float groundedVelocityY = -20;
    [SerializeField] protected float fallStartVelcityY = -10;
    protected bool fallingVelocityHasBeenSet = false;

    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();
    }
    protected virtual void Update()
    {
        HandleGroundCheck();

        if(character.isGrounded)
        {
            if(yVelocity.y <0)
            {
                fallingVelocityHasBeenSet = false;
                yVelocity.y = groundedVelocityY;
            }
        }
        else
        {
            //×ÔÓÉÂäÌå
            if(!character.isJumping && !fallingVelocityHasBeenSet)
            {
                fallingVelocityHasBeenSet =true;
                yVelocity.y = fallStartVelcityY;
            }
            character.anim.SetFloat("yVelocity", yVelocity.y);

            yVelocity.y += Time.deltaTime * gravityForce;
        }
        character.characterController.Move(yVelocity * Time.deltaTime);
    }
    protected void HandleGroundCheck()
    {
        character.isGrounded = Physics.CheckSphere(character.transform.position, groundCheckSphereRadius, groundLayer);
    }
    protected void OnDrawGizmosSelected()
    {
        //Gizmos.DrawSphere(character.transform.position, groundCheckSphereRadius);
    }
}
