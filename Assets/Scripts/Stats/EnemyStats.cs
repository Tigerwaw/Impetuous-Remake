using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
  [Space]
  public int gold_reward;

  [HideInInspector]
  public Camp camp;
  private int currentLevel;

  public override void Start()
  {
    base.Start();

    currentLevel = GameManager.instance.playerStats.level;

    UpdateModifiers();
  }

  private void UpdateModifiers()
  {
    maxHealth *= (1 + maxHealthMod * currentLevel);
    maxMana *= (1 + maxManaMod * currentLevel);
    manaRegen *= (1 + manaRegenMod * currentLevel);
    damage *= (1 + damageMod * currentLevel);
    attackSpeed *= (1 + attackSpeedMod * currentLevel);
    armor *= (1 + armorMod * currentLevel);

    currentHealth = maxHealth;
  }

  public override void Die()
  {
    base.Die();

    GameManager.instance.playerStats.GiveGold(gold_reward);

    camp.enemies.Remove(this.gameObject);

    if (camp.enemies.Count <= 0)
    {
      camp.CampCleared();
    }
    else
    {
      camp.SwitchTarget();
    }
    Destroy(this.gameObject);
  }
}
