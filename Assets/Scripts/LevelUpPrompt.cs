using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpPrompt : MonoBehaviour
{
  public PlayerAbilities playerAbilities;
  public Text abilityName;
  public Text gainsFromLeveling;
  public RawImage[] ability_Icons;

  public void Start()
  {
    for (int i = 0; i < playerAbilities.abilities.Length; i++)
    {
      ability_Icons[i].texture = playerAbilities.abilities[i].icon;
    }
  }

  public void HoverOverAbility(int ability_ID)
  {
    Ability ability = playerAbilities.abilities[ability_ID];
    int abilityLevel = playerAbilities.ability_level[ability_ID];

    abilityName.text = ability.name;

    if (abilityLevel == 0)
    {
      gainsFromLeveling.text = ability.description;
    }
    else
    {
      string gains = 
        "Mana cost: " + (ability.manaCosts[abilityLevel + 1] - ability.manaCosts[abilityLevel]) +
        "\n Cooldown: " + (ability.cooldowns[abilityLevel + 1] - ability.cooldowns[abilityLevel]);
      gainsFromLeveling.text = gains;
    }
  }
}
