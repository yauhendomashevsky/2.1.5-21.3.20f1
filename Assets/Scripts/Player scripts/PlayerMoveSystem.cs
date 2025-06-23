using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine.InputSystem;

public class PlayerMoveSystem : ComponentSystem
{
    private EntityQuery moveQuery;

    protected override void OnCreate()
    {
        moveQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(), 
            ComponentType.ReadOnly<MoveData>(), 
            ComponentType.ReadOnly<Transform>());
    }

    protected override void OnUpdate()
    {
        Entities.With(moveQuery)
            .ForEach((Entity entity, Transform transform, ref InputData userData, ref MoveData moveData) =>
            {
                var pos = transform.position;
                pos += new Vector3(userData.move.x * moveData.moveSpeed, 0, userData.move.y * moveData.moveSpeed);
                transform.position = pos;
            });
    }
}
