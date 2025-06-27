using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.InputSystem;

public class UserInputSystem : ComponentSystem
{
    private EntityQuery moveQuery;

    private InputAction moveAction;
    private InputAction shootAction;
    private InputAction rushAction;
    private InputAction rotateAction;

    private float2 moveInput;
    private float2 rotateInput;
    private float shootInput;
    private float rushInput;

    protected override void OnCreate()
    {
        moveQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>());
    }

    protected override void OnStartRunning()
    {
        moveAction = new InputAction("move", binding:"<Gamepad>/rightStick");
        moveAction.AddCompositeBinding("Dpad")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");

        moveAction.performed += context => { moveInput = context.ReadValue<Vector2>(); };
        moveAction.started += context => { moveInput = context.ReadValue<Vector2>(); };
        moveAction.canceled += context => { moveInput = context.ReadValue<Vector2>(); };
        moveAction.Enable();


        shootAction = new InputAction("shoot", InputActionType.Button, "<Keyboard>/Space");
        //shootAction.performed += context => { shootInput = 0f; };
        //shootAction.started += context => { shootInput = context.ReadValue<float>(); };
        //shootAction.canceled += context => { shootInput = 0f; };
        shootAction.Enable();


        rushAction = new InputAction("rush", binding: "<Keyboard>/Enter");
        rushAction.performed += context => { rushInput = context.ReadValue<float>(); };
        rushAction.started += context => { rushInput = context.ReadValue<float>(); };
        rushAction.canceled += context => { rushInput = context.ReadValue<float>(); };
        rushAction.Enable();
    }

    protected override void OnStopRunning()
    {
        moveAction.Disable();
        shootAction.Disable();
        rushAction.Disable();
    }

    //protected override void OnUpdate()
    //{
    //    bool isShootTriggered = shootAction.triggered;
    //    Entities.With(moveQuery)
    //        .ForEach((Entity entity, ref InputData userData) =>
    //        {
    //            userData.move = moveInput;
    //            userData.shoot = shootInput;
    //            userData.rush = rushInput;
    //            userData.rotate = moveInput;
    //        });
    //}

    protected override void OnUpdate()
    {
        bool isShootTriggered = shootAction.triggered; 
        Entities.With(moveQuery)
            .ForEach((Entity entity, ref InputData userData) => 
            {
                userData.move = moveAction.ReadValue<Vector2>(); 
                userData.shoot = isShootTriggered ? 1f : 0f;
                userData.rush = rushAction.ReadValue<float>();
                userData.rotate = moveAction.ReadValue<Vector2>();
            });
    }

}
