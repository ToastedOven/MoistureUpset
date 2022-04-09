using System;
using RoR2;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MoistureUpset.Skins.Engi
{
    public class KillTagController : MonoBehaviour
    {
        public TextMeshProUGUI enemyLabel;
        public RawImage enemyPortrait;
        public Slider healthSlider;

        public void Awake()
        {
            ResolveReferences();
        }

        public void OnEnable()
        {
            ResolveReferences();
        }

        public void ResolveReferences()
        {
            var enemyStuff = gameObject.transform.Find("KillTag").Find("EnemyStuff").gameObject;
            
            enemyLabel = enemyStuff.GetComponentInChildren<TextMeshProUGUI>();
            enemyPortrait = enemyStuff.GetComponentInChildren<RawImage>();

            healthSlider = GetComponentInChildren<Slider>();
        }

        public void SetAttacker(CharacterBody attackerBody)
        {
            enemyLabel.text = Language.GetString(attackerBody.baseNameToken);
            enemyPortrait.texture = attackerBody.portraitIcon;

            healthSlider.value = CalculateHealth(attackerBody.healthComponent);
        }

        private float CalculateHealth(HealthComponent healthComponent)
        {
            return (healthComponent.health + healthComponent.shield) / healthComponent.fullCombinedHealth;
        }
    }
}