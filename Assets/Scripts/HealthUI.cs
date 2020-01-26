using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class HealthUI : MonoBehaviour
{
  public float hpLost_FreezeTime;
  public float hpLost_VanishRate;

  [HideInInspector] public Transform ui;
  [HideInInspector] public GameObject dmgNrPrefab;
  [HideInInspector] public Slider healthLostSlider;
  [HideInInspector] public Slider healthSlider;
  [HideInInspector] public CharacterStats stats;

  float healthLostTimer = 2f;
  bool wasHealthLost;

  // Use this for initialization
  public virtual void Start()
  {
    stats = GetComponent<CharacterStats>();
    dmgNrPrefab = GameManager.instance.dmgNr_Prefab;
  }

  public void OnHealthChanged(int maxHealth, int currentHealth, int healthLost)
  {
    if (ui == null)
    {
      return;
    }
    ui.gameObject.SetActive(true);

    if (currentHealth <= 0)
    {
      Destroy(ui.gameObject);
    }

    float healthPercent = (float)currentHealth / maxHealth;
    healthSlider.value = healthPercent;

    float previousHealth = currentHealth + healthLost;
    float previousHealthPercent = (float)previousHealth / maxHealth;
    healthLostSlider.value = previousHealthPercent;

    wasHealthLost = true;
  }

  public void UpdateHealthBar()
  {
    float healthPercent = (float)stats.currentHealth / stats.maxHealth;
    healthSlider.value = healthPercent;
  }

  public void SpawnDamageNumber(int _dmg)
  {
    GameObject dmgNr = Instantiate(dmgNrPrefab, ui);
    dmgNr.GetComponentInChildren<Text>().text = _dmg.ToString();
    Destroy(dmgNr, 2f);
  }

  // Update is called once per frame
  public virtual void LateUpdate()
  {
    if (ui == null)
    {
      return;
    }

    if (!wasHealthLost)
    {
      return;
    }

    healthLostTimer -= Time.deltaTime;
    if (healthLostTimer < hpLost_FreezeTime)
    {
      healthLostSlider.value -= (hpLost_VanishRate / 100);
      if (healthLostSlider.value < healthSlider.value)
      {
        healthLostSlider.value = healthSlider.value;
        wasHealthLost = false;
        healthLostTimer = 2f;
      }
    }
  }
}
