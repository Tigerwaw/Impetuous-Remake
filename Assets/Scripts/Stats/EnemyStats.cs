using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
  public int gold_reward;
  public int xp_reward;

  [HideInInspector]
  public Camp camp;

  public override void Die()
  {
    base.Die();

    GameManager.instance.playerStats.GiveGold(gold_reward);
    GameManager.instance.playerStats.GiveExperience(xp_reward);

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
