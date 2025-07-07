using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class CollisionSystem : ComponentSystem
{
    private EntityQuery collisionQuery;
    private EntityQuery healthQuery;

    private Collider[] colliders = new Collider[50];

    protected override void OnCreate()
    {
        collisionQuery = GetEntityQuery(ComponentType.ReadOnly<ActorColliderData>(), ComponentType.ReadOnly<TrapAbility>());
        healthQuery = GetEntityQuery(ComponentType.ReadOnly<ActorColliderData>(), ComponentType.ReadOnly<HealthAbility>());
    }

    protected override void OnUpdate()
    {
        var dstManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        QueryUpdate(collisionQuery);
        QueryUpdate(healthQuery);
    }

    private void QueryUpdate(EntityQuery query)
    {
        Entities.With(query).ForEach(
            (Entity entity, Transform transform, ref ActorColliderData data) =>
            {
                if (transform != null)
                {
                    var gameObject = transform.gameObject;

                    float3 pos = gameObject.transform.position;
                    Quaternion quaternion = gameObject.transform.rotation;

                    var ability = gameObject.GetComponent<IcollisionAbility>();

                    if (ability == null) { return; }

                    ability.Colliders?.Clear();

                    int size = 0;

                    switch (data.type)
                    {
                        case ColliderType.Sphere:
                            size = Physics.OverlapSphereNonAlloc(data.SphereCenter + pos, data.SphereRadius, colliders);
                            break;
                        case ColliderType.Capsule:
                            var centre = ((data.CapsuleCenter + pos) + (data.CapsuleEnd + pos) / 2f);
                            var point1 = data.CapsuleCenter + pos;
                            var point2 = data.CapsuleEnd + pos;
                            point1 = (float3)(quaternion * (point1 - centre)) + centre;
                            point2 = (float3)(quaternion * (point2 - centre)) + centre;
                            size = Physics.OverlapCapsuleNonAlloc(point1, point2, data.CapsuleRadius, colliders);
                            break;
                        case ColliderType.Box:
                            size = Physics.OverlapBoxNonAlloc(data.BoxCenter + pos, data.BoxHalfExtend, colliders, data.BoxOrientation * quaternion);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }


                    if (size > 0)
                    {
                        ability.Colliders = colliders.ToList();
                        foreach (Collider c in colliders)
                        {
                            if (c != null)
                            {
                                ability.Colliders.Add(c);
                                ability.Execute();
                            }
                        }
                    }
                }
            });
    }
}
