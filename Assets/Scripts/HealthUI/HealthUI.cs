using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class HealthUI : MonoBehaviour
{
  public float hpLost_FreezeTime;
  public float hpLost_VanishRate;

  [HideInInspector] public Text currentHealthText;
  [HideInInspector] public Transform ui;
  [HideInInspector] public GameObject dmgNrPrefab;
  [HideInInspector] public GameObject healNrPrefab;
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
    healNrPrefab = GameManager.instance.healNr_Prefab;
  }

  public void OnHealthChanged(float maxHealth, float currentHealth, float healthChanged, bool heal)
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

    float healthPercent = currentHealth / maxHealth;
    healthSlider.value = healthPercent;

    currentHealthText.text = currentHealth.ToString();

    if (!heal)
    {
      HealthLost(maxHealth, currentHealth, healthChanged);
    }
  }

  public void HealthLost(float maxHealth, float currentHealth, float healthChanged)
  {
    float previousHealth = currentHealth + healthChanged;
    float previousHealthPercent = (float)previousHealth / maxHealth;
    healthLostSlider.value = previousHealthPercent;

    wasHealthLost = true;
  }

  public void SpawnDamageNumber(float _dmg)
  {
    GameObject dmgNr = Instantiate(dmgNrPrefab, ui);
    dmgNr.GetComponentInChildren<Text>().text = _dmg.ToString("0");
    Destroy(dmgNr, 2f);
  }

  public void SpawnHealingNumber(float _heal)
  {
    GameObject healNr = Instantiate(healNrPrefab, ui);
    healNr.GetComponentInChildren<Text>().text = _heal.ToString("0");
    Destroy(healNr, 2f);
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
