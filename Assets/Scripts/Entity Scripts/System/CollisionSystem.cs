using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;


public class CollisionSystem : ComponentSystem
{
    private EntityQuery _collisionQuery;
    private Collider[] _colliders = new Collider[50];

    protected override void OnCreate()
    {
        _collisionQuery = GetEntityQuery(ComponentType.ReadOnly<ActorColliderData>(), ComponentType.ReadOnly<Transform>());
    }


    protected override void OnUpdate()
    {
        var dstManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        Entities.With(_collisionQuery).ForEach(
            (Entity entity, CollisionAbility ability, ref ActorColliderData colliderData) =>
            {
                if (ability != null)
                {
                    var gameObject = ability.gameObject;
                    float3 position = gameObject.transform.position;
                    Quaternion rotation = gameObject.transform.rotation;

                    var _collList = new List<Collider>();
                    ability.collisions?.Clear();

                    int size = 0;

                    switch (colliderData.type)
                    {
                        case ColliderType.Sphere:
                            size = Physics.OverlapSphereNonAlloc(colliderData.SphereCenter + position, colliderData.SphereRadius, _colliders);
                            break;
                        case ColliderType.Capsule:
                            var centre = ((colliderData.CapsuleCenter + position) + (colliderData.CapsuleEnd + position)) / 2f;
                            var point1 = colliderData.CapsuleCenter + position;
                            var point2 = colliderData.CapsuleEnd + position;
                            point1 = (float3)(rotation * (point1 - centre)) + centre;
                            point2 = (float3)(rotation * (point2 - centre)) + centre;
                            size = Physics.OverlapCapsuleNonAlloc(point1, point2, colliderData.CapsuleRadius, _colliders);
                            break;
                        case ColliderType.Box:
                            size = Physics.OverlapBoxNonAlloc(colliderData.BoxCenter + position, colliderData.BoxHalfExtend, _colliders, colliderData.BoxOrientation * rotation);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    if (size > 0)
                    {
                        foreach (var collider in _colliders)
                        {
                            //_collList.Add(collider);
                            ability.collisions.Add(collider);
                        }

                        //ability.collisions = _colliders.ToList();
                        ability.Execute();
                    }
                }
            });
    }
}

