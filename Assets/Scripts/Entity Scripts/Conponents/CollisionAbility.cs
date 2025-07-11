using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class CollisionAbility : MonoBehaviour, IConvertGameObjectToEntity, Ability
{
    public Collider collider;

    public List<MonoBehaviour> collisionActions = new List<MonoBehaviour>();
    public List<IAbilityTarget> collisionActionAbility = new List<IAbilityTarget>();


    [HideInInspector] public List<Collider> collisions;

    public void Start()
    {
        foreach (var action in collisionActions) 
        {
            if (action is IAbilityTarget ability)
            {
                collisionActionAbility.Add(ability);
            }
            else 
            {
                Debug.Log("Wrong");
            }
        }
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        float3 position = gameObject.transform.position;

        switch (collider)
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
        foreach (var ability in collisionActionAbility)
        {
            ability.targets = new List<GameObject>();
            collisions.ForEach(c => 
            {
                if(c != null) ability.targets.Add(c.gameObject);
            });
            ability.Execute();
        }
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

