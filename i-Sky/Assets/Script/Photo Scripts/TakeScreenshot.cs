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
    public List<Texture2D> images = new List<Texture2D>();

    [Header("Input Variables:")]
    [SerializeField] InputActionProperty TakePhoto;


    private Transform photoParent;
    private bool isHoldingCamera = false;

    [SerializeField] GameObject platform;
    private void Start()
    {
        photoParent = snappingCamera.transform.parent.Find("Photo Parent");
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
        if((Input.GetKeyDown(KeyCode.Space) || TakePhoto.action.WasPressedThisFrame()) && images.Count < maxPics)
        {
            TakeAndSpawnPhoto();

            UpdatePhotosRemainingText();
        }

        if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1) && images[0]) { PrintTexture(images[0]); }
        if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2) && images[1]) { PrintTexture(images[1]); }
        if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3) && images[2]) { PrintTexture(images[2]); }
        if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4) && images[3]) { PrintTexture(images[3]); }
        if (Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Alpha5) && images[4]) { PrintTexture(images[4]); }
        if (Input.GetKeyDown(KeyCode.Keypad6) || Input.GetKeyDown(KeyCode.Alpha6) && images[5]) { PrintTexture(images[5]); }
        if (Input.GetKeyDown(KeyCode.Keypad7) || Input.GetKeyDown(KeyCode.Alpha7) && images[6]) { PrintTexture(images[6]); }
        if (Input.GetKeyDown(KeyCode.Keypad8) || Input.GetKeyDown(KeyCode.Alpha8) && images[7]) { PrintTexture(images[7]); }
        if (Input.GetKeyDown(KeyCode.Keypad9) || Input.GetKeyDown(KeyCode.Alpha9) && images[8]) { PrintTexture(images[8]); }
    }

    void TakeAndSpawnPhoto()
    {
        if (photoParent.childCount > 0)
        {
            print("running");
            Transform photoChild = photoParent.GetChild(0);

            photoChild.parent = null;
            //photoChild.GetComponent<Rigidbody>().isKinematic = false;
        }

        GameObject spawnedPhoto = Instantiate(photoPrefab, photoParent);
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

        

        print("Photo Taken");
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

    public static void PrintTexture(Texture2D texture)
    {
        byte[] byteArray = texture.EncodeToPNG();
        System.IO.File.WriteAllBytes(Application.dataPath + "/cameracapture.png", byteArray);
        System.Diagnostics.Process process = new System.Diagnostics.Process();
        process.StartInfo.CreateNoWindow = false;
        process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        process.StartInfo.UseShellExecute = true;
        process.StartInfo.FileName = Application.dataPath + "/cameracapture.png";
        process.StartInfo.Verb = "print";

        process.Start();
    }
}
