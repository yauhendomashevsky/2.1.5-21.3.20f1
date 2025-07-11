using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class TrapAbility : CollisionAbility
    {
        public int damage = 10;

        public void Execute()
        {
            foreach (var target in collisions)
            {
                var targetHealth = target?.gameObject?.GetComponent<CharacterH>();
                if (targetHealth != null)
                {
                    targetHealth.health -= damage;
                }
            }
        }
    }
