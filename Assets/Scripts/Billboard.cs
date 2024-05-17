using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    void LateUpdate()
    {
        Transform cam = Camera.main.transform;

        if (cam == null)
        {
            Debug.LogWarning("Main camera not found. Make sure you have a camera with the tag 'MainCamera'.");
            return;
        }

        transform.LookAt(transform.position + cam.forward);
    }
}
