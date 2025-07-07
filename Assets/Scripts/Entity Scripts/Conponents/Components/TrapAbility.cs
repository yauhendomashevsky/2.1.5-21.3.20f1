using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapAbility : CollisionAbility, IcollisionAbility
{
    public int damage = 10;

    public void Execute()
    {
        foreach (var target in Colliders)
        {
            if (target != null)
            {
                if (target.gameObject.CompareTag("Player"))
                {
                    target.GetComponent<CharacterHealth>().health -= damage;
                    Destroy(gameObject);
                }
            }
        }
    }
}
