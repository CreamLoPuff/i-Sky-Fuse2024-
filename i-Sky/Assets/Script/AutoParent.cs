using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoParent : MonoBehaviour
{
    [SerializeField] string parentName;

    private Transform parent;

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
    }
}
