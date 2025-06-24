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
            ComponentType.ReadOnly<Transform>()
            );
    }

    protected override void OnUpdate()
    {
        Entities.With(bulletQuery)
            .ForEach((Entity entity, Transform transform, ref BulletMoveData bulletData) =>
            {
                var pos = transform.position;
                pos += new Vector3(0, 0, bulletData.bulletSpeed);
                transform.position = pos;   
            });
    }
}
