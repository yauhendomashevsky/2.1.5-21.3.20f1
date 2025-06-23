using Unity.Entities;
using UnityEngine;

public class PlayerRotationSystem : ComponentSystem
{
    private EntityQuery rotateQuery;

    protected override void OnCreate()
    {
        rotateQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(),
            ComponentType.ReadOnly<RotateData>(),
            ComponentType.ReadOnly<Transform>());
    }

    protected override void OnUpdate()
    {
        Entities.With(rotateQuery)
            .ForEach((Entity entity, Transform transform, ref InputData userData, ref RotateData rotateData) =>
            {
                
                Vector3 moveDir = new Vector3(userData.move.x, 0, userData.move.y);
                if (Vector3.Angle(transform.forward, moveDir) > 0)
                {
                    Quaternion quaternion = Quaternion.LookRotation(moveDir, Vector3.up);
                    Quaternion playerRot = Quaternion.Slerp(transform.rotation, quaternion, rotateData.rotateSpeed * Time.DeltaTime);
                    transform.rotation = quaternion;
                }
            });
    }

}
