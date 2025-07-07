using System.Collections.Generic;
using UnityEngine;

internal interface IcollisionAbility : Ability
{
    List<Collider> Colliders { get; set; }
}