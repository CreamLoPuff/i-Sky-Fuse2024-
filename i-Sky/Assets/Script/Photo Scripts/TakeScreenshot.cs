using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TakeScreenshot : MonoBehaviour
{
    public Camera displayCamera;
    public Camera snappingCamera;

    //public RenderTexture snapRenderTexture;
    public RawImage snapDisplay;


    [SerializeField] InputActionProperty TakePhoto;

    private bool isHoldingCamera = false;

    public void HoldingCamera()
    {
        isHoldingCamera = true;
    }

    public void NotHoldingCamera()
    {
        isHoldingCamera = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || TakePhoto.action.WasPressedThisFrame() && isHoldingCamera) 
        {
            var currentRT = RenderTexture.active;
            RenderTexture.active = snappingCamera.targetTexture;

            //print("Taking screenshot");
            //snappingCamera.CopyFrom(displayCamera);
            snappingCamera.Render();

            Texture2D image = new Texture2D(snappingCamera.targetTexture.width, snappingCamera.targetTexture.height);
            image.ReadPixels(new Rect(0, 0, snappingCamera.targetTexture.width, snappingCamera.targetTexture.height), 0, 0);
            image.Apply();

            RenderTexture.active = currentRT;
            snapDisplay.texture = image;

            byte[] bytes = ImageConversion.EncodeToPNG(image);

            // For testing purposes, also write to a file in the project folder
            File.WriteAllBytes(UnityEngine.Application.dataPath + "/../SavedScreen.png", bytes);
        }
    }
}
