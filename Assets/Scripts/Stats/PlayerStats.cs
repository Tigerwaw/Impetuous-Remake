using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
  public int gold;
  public int level;

  public int skill_points;

  // Player Specific Attribute Modifiers
  private int goldMod;

  public override void Start()
  {
    base.Start();

    UpdateModifiers();

    GameManager.instance.UpdateUI(gold, level);
  }

  private void UpdateModifiers()
  {
    maxHealth *= (1 + maxHealthMod);
    maxMana *= (1 + maxManaMod);
    manaRegen *= (1 + manaRegenMod);
    damage *= (1 + damageMod);
    attackSpeed *= (1 + attackSpeedMod);
    armor *= (1 + armorMod);

    currentHealth = maxHealth;

    /*
    goldModifier = talents;
    */
  }

  public void LevelUp()
  {
    level += 1;
    skill_points += 1;
    GameManager.instance.UpdateUI(gold, level);
    Debug.Log("Level up!");
  }

  public void GiveGold(int _gold)
  {
    gold += _gold;
  }

  public override void Die()
  {
    base.Die();

    GameManager.instance.RestartGame();
  }
}
