using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class BulletMoveSystem : ComponentSystem
{
    private EntityQuery bulletQuery;

    protected override void OnCreate()
    {
        bulletQuery = GetEntityQuery(
            ComponentType.ReadOnly<BulletMoveData>(),
            ComponentType.ReadOnly<Transform>(),
            ComponentType.ReadOnly<BulletDirectionData>()
            );
    }

    protected override void OnUpdate()
    {
        Entities.With(bulletQuery)
            .ForEach((Entity entity, Transform transform, ref BulletMoveData bulletData, ref BulletDirectionData dirData) =>
            {
                var deltaTime = Time.DeltaTime;
                var pos = transform.position;
                pos += transform.forward * bulletData.bulletSpeed * deltaTime;
                transform.position = pos;   
            });
    }
}
