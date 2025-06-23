using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class BulletData : MonoBehaviour, IConvertGameObjectToEntity
{
    public float bulletSpeed;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new BulletMoveData() { bulletSpeed = bulletSpeed / 50 });
    }
}

public struct BulletMoveData : IComponentData
{
    public float bulletSpeed;
}
