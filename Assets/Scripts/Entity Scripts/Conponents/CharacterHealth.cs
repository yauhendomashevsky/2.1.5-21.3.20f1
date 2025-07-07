using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class CharacterHealth :MonoBehaviour, IConvertGameObjectToEntity
{
    public int health = 100;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new CharacterHealthData { health = health });
    }

    public struct CharacterHealthData : IComponentData
    {
        public float health;
    }
}
