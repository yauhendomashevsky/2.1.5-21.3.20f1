using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class UserInputData : MonoBehaviour, IConvertGameObjectToEntity
{
    public float speed;
    public float rushSpeed;
    public float rotSpeed;
    public MonoBehaviour shootAction;
    
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new InputData());
        dstManager.AddComponentData(entity, new MoveData { moveSpeed = speed  / 100});
        dstManager.AddComponentData(entity, new RushData { rushSpeed = rushSpeed / 100 });
        dstManager.AddComponentData(entity, new RotateData { rotateSpeed = rotSpeed / 100 });

        if(shootAction != null && shootAction is Ability)
        {
            dstManager.AddComponentData(entity, new ShootData());
        }
    }
}

public struct InputData : IComponentData
{
    public float2 move;
    public float shoot;
    public float rush;
    public float2 rotate;
}

public struct MoveData : IComponentData
{
    public float moveSpeed;
}

public struct ShootData : IComponentData
{
    public float shoot;
}

public struct RushData : IComponentData 
{
    public float rushSpeed;
}

public struct RotateData : IComponentData 
{
    public float rotateSpeed;
}