using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    private ShipEngine engine;
    private int maxHealth;

    private void Start()
    {
        engine = GetComponent<ShipEngine>();
        maxHealth = engine.GetHealth();
    }

    private void Update()
    {
        healthSlider.value = (float)engine.GetHealth() / maxHealth;
    }
}
