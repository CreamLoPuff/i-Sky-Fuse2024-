using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoParent : MonoBehaviour
{
    [SerializeField] string parentName;

    [SerializeField]private Transform parent;
    

    private void Start()
    {
        parent = GameObject.Find(parentName).transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.parent == null)
        {
            transform.SetParent(parent, true);
        }

        transform.localScale = new Vector3(.55f, .55f, .55f);
    }
}
