using Unity.Entities;
using static CharacterHealth;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerHealthSystemm : ComponentSystem
{
    private EntityQuery removeHealth;

    protected override void OnCreate()
    {
        removeHealth = GetEntityQuery(ComponentType.ReadOnly<CharacterHealthData>());
    }

    protected override void OnUpdate()
    {
        Entities.With(removeHealth).ForEach((Entity entity, ref CharacterHealthData healthData) =>
        {
            //healthData.health -= TrapAbility.
        });
    }
}
