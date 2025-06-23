using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class RushSystem : ComponentSystem
{
    private EntityQuery rushQuery;

    protected override void OnCreate()
    {
        rushQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(),
            ComponentType.ReadOnly<RushData>(),
            ComponentType.ReadOnly<Transform>());
    }

    protected override void OnUpdate()
    {
        Entities.With(rushQuery)
            .ForEach((Entity entity, Transform transform, ref InputData userData, ref RushData rushData) =>
            {
                if (userData.rush > 0f)
                {
                    var pos = transform.position;
                    pos += transform.forward * rushData.rushSpeed;
                    transform.position = pos;
                }
            });
    }
}
