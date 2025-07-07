using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class TrapSystem : MonoBehaviour, IConvertGameObjectToEntity
{
    public int damage = 1;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new TrapDamage { damaga = damage});
    }
}


public struct TrapDamage : IComponentData
{
    public int damaga;
}
