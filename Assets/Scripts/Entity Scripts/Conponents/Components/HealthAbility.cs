using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAbility : CollisionAbility, IcollisionAbility
{
    public int health = 5;

    public void Execute()
    {
        foreach (var target in Colliders)
        {
            if (target != null)
            {
                if (target.gameObject.CompareTag("Player"))
                {
                    target.GetComponent<CharacterHealth>().health += health;
                    Destroy(gameObject);
                }
            }
        }
    }
}
