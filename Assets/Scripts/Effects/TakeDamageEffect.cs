using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Effect/Instant Effects/Take Damage")]
public class TakeDamageEffect : InstantCharacterEffect
{
    [Header("Character Causing Damage")]
    public CharacterManager characterCausingDamage;//造成伤害的角色
    [Header("Damage")]
    public float physicalDamage = 0;
    public float magicDamage = 0;
    public float fireDmage = 0;
    public float lightningDamage = 0;//闪电伤害
    public float holyDamage = 0;//神圣伤害

    [Header("Final Damage")]
    private int finalDmageDealt = 0;//dealt ： 处理
    [Header("Poise")]
    public float poiseDamage = 0;
    public bool poiseIsBroken = false;//如果为true 则播放“眩晕”动画
    [Header("Animation")]
    public bool playDamageAnimation = true;
    public bool manuallySelectDamageAnimation = false;//手动选择伤害动画
    public string damageAnimation;
    [Header("Sound FX")]
    public bool willPlayDamageSFX = true;
    public AudioClip elementalDamageSoundFX;

    [Header("Direction Damage Taken From")]
    public float angleHitFrom;
    public Vector3 contactPoint;
    public override void ProcessEffect(CharacterManager character)
    {
        if (character.isDead)
            return;
        CalculateDamage(character);
    }
    private void CalculateDamage(CharacterManager character)
    {
        if (!character.isOwned)
            return;
        if (characterCausingDamage != null)
        {

        }
        finalDmageDealt = Mathf.RoundToInt(physicalDamage + magicDamage + fireDmage + lightningDamage + holyDamage);
        if (finalDmageDealt < 0)
        {
            finalDmageDealt = 1;
        }
        character.currentHealth -= finalDmageDealt;
        character.characterNetworkManager.SetCurrentHealthValue(character.currentHealth);
    }
}
