using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Effect/Instant Effects/Take Damage")]
public class TakeDamageEffect : InstantCharacterEffect
{
    [Header("Character Causing Damage")]
    public CharacterManager characterCausingDamage;//����˺��Ľ�ɫ
    [Header("Damage")]
    public float physicalDamage = 0;
    public float magicDamage = 0;
    public float fireDmage = 0;
    public float lightningDamage = 0;//�����˺�
    public float holyDamage = 0;//��ʥ�˺�

    [Header("Final Damage")]
    private int finalDmageDealt = 0;//dealt �� ����
    [Header("Poise")]
    public float poiseDamage = 0;
    public bool poiseIsBroken = false;//���Ϊtrue �򲥷š�ѣ�Ρ�����
    [Header("Animation")]
    public bool playDamageAnimation = true;
    public bool manuallySelectDamageAnimation = false;//�ֶ�ѡ���˺�����
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
