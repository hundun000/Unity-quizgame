using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LibgdxFeatureExtension
{
    public static void AsTableClear(this Transform thiz)
    {
        foreach (Transform child in thiz)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public static T AsTableAdd<T>(this Transform thiz, GameObject prefab)
    {
        GameObject vmInstance = GameObject.Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
        vmInstance.transform.SetParent(thiz.transform);
        vmInstance.transform.localPosition = new Vector3(0, 0, 0);
        T vm = vmInstance.GetComponent<T>();
        return vm;
    }
}