using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilities : MonoBehaviour
{
  private TriggerAbility triggerAbility;
  private CharacterCombat playerCombat;
  private PlayerStats playerStats;

  public Ability[] abilities;
  public int[] ability_level;
  public float[] ability_currentCD;
  public Text[] ability_cdTexts;
  public GameObject[] ability_greyOuts;
  public RawImage[] ability_Icons;

  private void Start()
  {
    playerCombat = GetComponent<CharacterCombat>();
    playerStats = GetComponent<PlayerStats>();
    triggerAbility = GetComponent<TriggerAbility>();

    for (int i = 0; i < abilities.Length; i++)
    {
      ability_level[i] = 0;
      ability_currentCD[i] = 0;
      ability_greyOuts[i].SetActive(true);
      ability_cdTexts[i].gameObject.SetActive(false);
      ability_Icons[i].texture = abilities[i].icon;
    }
  }

  public void Retarget_Button()
  {
    if (playerCombat.InCombat)
    {
      Camp _camp = playerCombat.opponentStats.GetComponent<EnemyStats>().camp;
      _camp.SwitchTarget();
    }
  }

  public virtual void UseAbility(int ability_ID)
  {
    Ability _ability = abilities[ability_ID];

    // Check whether the player has the required level and mana for the ability, as well as it being off cooldown.
    #region AbilityCheck
    if (ability_level[ability_ID] < 1)
    {
      Debug.Log("Ability not leveled!");
      return;
    }

    if (ability_currentCD[ability_ID] > 0)
    {
      Debug.Log("Ability on cooldown!");
      return;
    }

    if (playerStats.currentMana < _ability.manaCosts[ability_level[ability_ID]])
    {
      Debug.Log("Not enough mana!");
      return;
    }
    #endregion

    triggerAbility.UseAbility(_ability, playerStats, playerCombat.opponentStats);

    Debug.Log("Used Ability " + _ability.name);
    ability_currentCD[ability_ID] = _ability.cooldowns[ability_level[ability_ID]];
    playerStats.currentMana -= _ability.manaCosts[ability_level[ability_ID]];
    //UpdateManaBar();
  }

  public void LevelUpAbility(int _ability_ID)
  {
    ability_level[_ability_ID] += 1;
    playerStats.skill_points -= 1;
    ability_greyOuts[_ability_ID].SetActive(false);

    GameManager.instance.levelUpUI.SetActive(false);

    StartCoroutine(ReturnMovement(.6f));
  }

  IEnumerator ReturnMovement(float delay)
  {
    yield return new WaitForSeconds(delay);

    GameManager.instance.campSelect.allowMovement = true;
  }

  private void Update()
  {
    for (int i = 0; i < 3; i++)
    {
      ability_currentCD[i] -= Time.deltaTime;
      if (ability_currentCD[i] > 0)
      {
        ability_greyOuts[i].SetActive(true);
        ability_cdTexts[i].gameObject.SetActive(true);
        ability_cdTexts[i].text = ability_currentCD[i].ToString("0");
      }
      else
      {
        ability_cdTexts[i].gameObject.SetActive(false);
        
        if (ability_level[i] > 0)
        {
          ability_greyOuts[i].SetActive(false);
        }
      }
    }
  }
}
