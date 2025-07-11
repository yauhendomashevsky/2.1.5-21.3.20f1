using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbilityTarget : Ability
{
    List<GameObject> targets { get; set; }
}
