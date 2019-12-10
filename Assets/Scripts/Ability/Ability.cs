using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityType
{
  Selfbuff,
  Targeted,
  AOE
};

public enum DebuffType
{
  MaxHealth,
  ManaRegen,
  Damage,
  AttackSpeed,
  Armor
};

[CreateAssetMenu(menuName = "New Ability")]
public class Ability : ScriptableObject
{
  [Header("Ability Information")]
  [Space]
  public Texture icon;
  new public string name;
  [TextArea]
  public string description;
  [Space]
  [Space]
  [Space]

  [Header("Ability Mechanics")]
  public AbilityType abilityType;
  [Space]

  public int maxLevel;
  public int[] manaCosts;
  public int[] cooldowns;
  [Space]

  public int damage;
  public bool appliesDebuff;
  public DebuffType debuffType;
  public int debuffAmount;
  public float debuffDuration;
  [Space]

  [Header("Ability Effects")]
  public GameObject particle_effect;
  public AudioClip sound_effect;
}
