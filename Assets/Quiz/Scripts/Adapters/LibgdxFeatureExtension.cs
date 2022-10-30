using hundun.quizlib.exception;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public static class LibgdxFeatureExtension
{
    static System.Object[] pushParams;

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
        if (vm == null)
        {
            throw new Exception("vmInstance.GetComponent<T> is null, maybe forget add Component");
        }
        return vm;
    }

    public static void SetScreenChangePushParams(System.Object[] pushParams)
    {
        LibgdxFeatureExtension.pushParams = pushParams;
    }

    public static System.Object[] GetScreenChangePushParams()
    {
        return pushParams;
    }

    internal static void log(string name, string str)
    {
        Debug.LogFormat("[{0}] {1}", name, str);
    }

    internal static void error(string name, string str, Exception e)
    {
        Debug.LogFormat("[{0}] {1}: {2}", name, str, e.Message);
    }
}