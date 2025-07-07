using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class CollisionAbility : MonoBehaviour, IConvertGameObjectToEntity//, IcollisionAbility
{
    public Collider collider;

    public List<Collider> Colliders { get ; set; }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        float3 position = gameObject.transform.position;
        switch(collider)
        {
            case SphereCollider sphere:
                sphere.ToWorldSpaceSphere(out var sphereCenter, out var sphereRadius);
                dstManager.AddComponentData(entity, new ActorColliderData
                {
                    type = ColliderType.Sphere,
                    SphereCenter = sphereCenter - position,
                    SphereRadius = sphereRadius,
                    InitialTaeOff = true
                });
                break;
            case CapsuleCollider capsule:
                capsule.ToWorldSpaceCapsule(out var capsuleStart, out var capsuleEnd, out var capsuleRadius);
                dstManager.AddComponentData(entity, new ActorColliderData
                {
                    type = ColliderType.Capsule,
                    CapsuleCenter = capsuleStart - position,
                    CapsuleEnd = capsuleEnd - position,
                    CapsuleRadius = capsuleRadius,
                    InitialTaeOff = true
                });
                break;
            case BoxCollider box:
                box.ToWorldSpaceBox(out var boxCenter, out var boxHalfExtends, out var boxOrientation);
                dstManager.AddComponentData(entity, new ActorColliderData
                {
                    type = ColliderType.Box,
                    BoxCenter = boxCenter - position,
                    BoxHalfExtend = boxHalfExtends,
                    BoxOrientation = boxOrientation,
                    InitialTaeOff = true
                });
                break;
        }
    }

    public void Execute()
    {
        Debug.Log("asdasd");
    }
}

public struct ActorColliderData : IComponentData
{
    public ColliderType type;
    public float3 SphereCenter;
    public float SphereRadius;
    public float3 CapsuleCenter;
    public float3 CapsuleEnd;
    public float CapsuleRadius;
    public float3 BoxCenter;
    public float3 BoxHalfExtend;
    public quaternion BoxOrientation;
    public bool InitialTaeOff;
}

public enum ColliderType
{
    Sphere = 0,
    Capsule = 1,
    Box = 2
}
