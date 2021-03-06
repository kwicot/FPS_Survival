using System.Collections;
using System.Collections.Generic;
using _Core.Scripts;
using _Core.Scripts.InventorySystem;
using UnityEngine;
using UnityEngine.Serialization;

public class CarController : MonoBehaviour, IInteractable
{
    [SerializeField] private RCC_CarControllerV3 rcc;
    
    [SerializeField] private Transform playerInCarPositionTransform;
    [SerializeField] private Transform playerExitPositionTransform;
    [SerializeField] private InventoryBase inventory;


    public Vector3 PlayerPosition => playerInCarPositionTransform.position;
    public Vector3 ExitPosition => playerExitPositionTransform.position;

    public InventoryBase Inventory => inventory;
    void Start()
    {
        PlayerExit();
    }

    // Update is called once per frame
    void Update()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        //rcc.inputs.steerInput = v;
    }

    public void PlayerEnter()
    {
        rcc.StartEngine();
        rcc.canControl = true;
    }

    public void PlayerExit()
    {
        rcc.KillEngine();
        rcc.SetCanControl(false);
    }

}
