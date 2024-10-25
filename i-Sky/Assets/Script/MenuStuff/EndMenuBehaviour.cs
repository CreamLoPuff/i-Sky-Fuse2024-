using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndMenuBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PopulateImages();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) PopulateImages();
        if (Input.GetKeyDown(KeyCode.R)) Return();
    }

    void PopulateImages()
    {
        TakeScreenshot ssm = FindObjectOfType<TakeScreenshot>();

        for (int i = 0; i < ssm.images.Count; i++) 
        {
            transform.GetChild(i).gameObject.SetActive(true);
            transform.GetChild(i).GetComponent<RawImage>().texture = ssm.images[i];
        }
    }

    public void Return()
    {
        SceneManager.LoadScene(0);
    }
}
