using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletData : MonoBehaviour, IConvertGameObjectToEntity
{
    public float bulletSpeed;
    public float bulletLife;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new BulletMoveData() { bulletSpeed = bulletSpeed / 50 });
        dstManager.AddComponentData(entity, new BulletLifeData() { lifeTime = bulletLife });
    }
}

public struct BulletMoveData : IComponentData
{
    public float bulletSpeed;
}

public struct BulletLifeData :IComponentData
{
    public float lifeTime;
}
