


using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class Throw : MonoBehaviour
{


    [SerializeField] InputActionProperty shootObjectInput;
    [SerializeField] GameObject hand;
    [SerializeField] private float speed;
    [SerializeField] private GameObject throwObject;
    private GameObject spawnedThrowGameObject;

    bool keepObjectInPlace = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shootObjectInput.action.WasPressedThisFrame())
        {
           spawnedThrowGameObject = Instantiate(throwObject, hand.transform.position, Quaternion.identity);
            keepObjectInPlace = true;
        }

        if (keepObjectInPlace && spawnedThrowGameObject != null)
        {
            spawnedThrowGameObject.transform.Rotate(15 * Time.deltaTime, 25 * Time.deltaTime, 15 * Time.deltaTime);
            spawnedThrowGameObject.transform.position = hand.transform.position;

        }

        if (shootObjectInput.action.WasReleasedThisFrame())
        {
            spawnedThrowGameObject.GetComponent<Rigidbody>().AddForce(hand.transform.forward * speed * Time.deltaTime, ForceMode.Impulse);
            keepObjectInPlace = false;
        }
    }


   
}
