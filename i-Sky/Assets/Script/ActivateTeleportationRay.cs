using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActivateTeleportationRay : MonoBehaviour
{

    [SerializeField] GameObject leftTeleportationRay;
    [SerializeField] GameObject rightTeleportationRay;

    [SerializeField] InputActionProperty leftActivate;
    [SerializeField] InputActionProperty rightActivate;

    [SerializeField] float activateDeadZone;
    
    // Update is called once per frame
    void Update()
    {

        leftTeleportationRay.SetActive(leftActivate.action.ReadValue<float>() > activateDeadZone);
        rightTeleportationRay.SetActive(rightActivate.action.ReadValue<float>() >activateDeadZone);

    }
}
