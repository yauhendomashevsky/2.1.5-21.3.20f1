using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class CharacterH : MonoBehaviour, IConvertGameObjectToEntity
{
    public int health = 200;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new PlayerHealth { health = health });
    }

    public struct PlayerHealth : IComponentData
    {
        public int health;
    }
}
