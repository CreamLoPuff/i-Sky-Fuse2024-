using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TakeScreenshot : MonoBehaviour
{
    [Header("Refrences:")]
    [SerializeField] Camera snappingCamera;
    [SerializeField] GameObject photoPrefab;
    [SerializeField] TextMeshProUGUI photosRemainingText;

    [Header("Images Variables:")]
    [SerializeField] int maxPics = 6;
    [SerializeField] List<Texture2D> images = new List<Texture2D>();

    [Header("Input Variables:")]
    [SerializeField] InputActionProperty TakePhoto;



    private bool isHoldingCamera = false;

    private void Start()
    {
        photosRemainingText.text = (maxPics - images.Count).ToString();
    }

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
        if((Input.GetKeyDown(KeyCode.Space) || TakePhoto.action.WasPressedThisFrame() && isHoldingCamera) && images.Count < maxPics)
        {
            TakeAndSpawnPhoto();

            UpdatePhotosRemainingText();
        }

        if(Input.GetKeyDown(KeyCode.Keypad0))
        {
            SavePhotos();
        }
    }

    void TakeAndSpawnPhoto()
    {
        GameObject spawnedPhoto = Instantiate(photoPrefab, Vector3.right * images.Count, Quaternion.identity);
        RawImage spawnedPhotoDisplay = spawnedPhoto.transform.GetChild(1).GetChild(0).GetComponent<RawImage>();

        var currentRT = RenderTexture.active;
        RenderTexture.active = snappingCamera.targetTexture;

        snappingCamera.Render();

        Texture2D image = new Texture2D(snappingCamera.targetTexture.width, snappingCamera.targetTexture.height);
        image.ReadPixels(new Rect(0, 0, snappingCamera.targetTexture.width, snappingCamera.targetTexture.height), 0, 0);
        image.Apply();

        images.Add(image);

        RenderTexture.active = currentRT;
        spawnedPhotoDisplay.texture = image;
    }

    void UpdatePhotosRemainingText()
    {
        photosRemainingText.text = (maxPics - images.Count).ToString();
        if (maxPics - images.Count <= 0)
        {
            photosRemainingText.color = Color.red;
        }
    }

    void SavePhotos()
    {
        for (int i = 0; i < images.Count; i++)
        {
            byte[] bytes = ImageConversion.EncodeToPNG(images[i]);

            // For testing purposes, also write to a file in the project folder
            File.WriteAllBytes(UnityEngine.Application.dataPath + "/../SavedPhoto("+ i +").png", bytes);
        }

        print("Photos Saved");
    }
}
