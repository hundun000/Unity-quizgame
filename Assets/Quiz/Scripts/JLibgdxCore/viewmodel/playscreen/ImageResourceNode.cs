using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class ImageResourceNode : MonoBehaviour
{
    Image selfImage;

    void Awake()
    {
        this.selfImage = this.GetComponent<Image>();
    }

    private Sprite getFile(String key)
    {
        int extendDotIndex = key.LastIndexOf('.');
        if (extendDotIndex > 0)
        {
            key = key.Substring(0, extendDotIndex);
        }
        String path = "quiz/pictures/" + key;
        return Resources.Load<Sprite>(path);
    }


    public void updateResource(String data)
    {
        Sprite sprite = getFile(data);
        this.selfImage.sprite = sprite;
        float aspectRatio = sprite.rect.width / sprite.rect.height;
        var fitter = this.GetComponent<AspectRatioFitter>();
        fitter.aspectRatio = aspectRatio;
    }

    internal void postParentCallActive()
    {
        
    }
}
