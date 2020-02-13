using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : HealthUI
{
  public float healthUIOffset;

  [HideInInspector] public GameObject uiPrefab;
  [HideInInspector] public Transform cam;

  public override void Start()
  {
    base.Start();

    uiPrefab = GameManager.instance.HP_Prefab;

    cam = Camera.main.transform;
    foreach (Canvas c in FindObjectsOfType<Canvas>())
    {
      if (c.renderMode == RenderMode.WorldSpace)
      {
        ui = Instantiate(uiPrefab, c.transform).transform;
        healthLostSlider = ui.GetChild(1).GetComponent<Slider>();
        healthSlider = ui.GetChild(2).GetComponent<Slider>();
        currentHealthText = ui.GetComponentInChildren<Text>();
        currentHealthText.text = stats.currentHealth.ToString();
        ui.gameObject.SetActive(false);
        break;
      }
    }
  }

  public override void LateUpdate()
  {
    base.LateUpdate();

    ui.position = transform.position + Vector3.up * healthUIOffset;
    ui.forward = -cam.forward;
  }
}
