using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyDamage : MonoBehaviour, IAbilityTarget
{
    public int damage;
    public List<GameObject> targets { get ; set ; }

    public void Execute()
    {
        foreach (var target in targets)
        {
            var targets = target.gameObject.GetComponent<CharacterH>();
            if (targets != null)
            {
                targets.health -= damage;
                Destroy(gameObject);
            }
        }
    }
}
