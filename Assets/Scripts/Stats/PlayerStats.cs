using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
  public int gold;
  public int level;
  public int xp;

  private int xp_req;
  public int skill_points;

  public override void Start()
  {
    base.Start();

    xp_req = 2;
    GameManager.instance.UpdateUI(gold, level, xp_req - xp);
  }

  public void GiveExperience(int _exp)
  {
    if (xp + _exp >= xp_req)
    {
      int _leftOverXp = (xp + _exp) - xp_req;
      xp = 0 + _leftOverXp;
      LevelUp();
    }
    else
    {
      xp += _exp;
      GameManager.instance.UpdateUI(gold, level, xp_req - xp);
    }
  }

  private void LevelUp()
  {
    level += 1;
    skill_points += 1;
    xp_req = level * 2;
    GameManager.instance.UpdateUI(gold, level, xp_req - xp);
  }

  public void GiveGold(int _gold)
  {
    gold += _gold;
  }
}
