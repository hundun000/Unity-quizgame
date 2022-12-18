using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScreenBackgroundVM : MonoBehaviour
{
    Image selfImage;

    void Awake()
    {
        selfImage = GetComponent<Image>();
    }

    public void init(string typeName)
    {
        Sprite sprite = TextureConfig.getScreenBackground(typeName);
        this.selfImage.sprite = sprite;
        //float aspectRatio = sprite.rect.width / sprite.rect.height;
        //var fitter = this.GetComponent<AspectRatioFitter>();
        //fitter.aspectRatio = aspectRatio;
    }

}
