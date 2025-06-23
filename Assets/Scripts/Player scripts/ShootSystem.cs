using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class ShootSystem : ComponentSystem
{
    private EntityQuery shootQuery;

    protected override void OnCreate()
    {
        shootQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(),
            ComponentType.ReadOnly<ShootData>(),
            ComponentType.ReadOnly<UserInputData>());
    }
    protected override void OnUpdate()
    {
        Entities.With(shootQuery)
            .ForEach((Entity entity, UserInputData inputData,ref InputData userData) =>
            {
            if (inputData.shootAction != null && inputData.shootAction is Ability ability && userData.shoot > 0f)
                {
                    ability.Execute();
                }
            });
    }
}
