using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class BulletLifeSystem : ComponentSystem
{
    private EntityQuery bulletLifeQuerry;

    protected override void OnCreate()
    {
        bulletLifeQuerry = GetEntityQuery(ComponentType.ReadOnly<BulletMoveData>(),
            ComponentType.ReadWrite<BulletLifeData>(),
            ComponentType.ReadOnly<Transform>());
    }

    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;
        Entities.With(bulletLifeQuerry).ForEach((Entity entity, Transform transform, ref BulletLifeData lifeTime) =>
        {
            lifeTime.lifeTime -= deltaTime;
            if (lifeTime.lifeTime <= 0)
            {
                EntityManager.DestroyEntity(entity);
                GameObject.Destroy(transform.gameObject);
            }
        });
    }
}
