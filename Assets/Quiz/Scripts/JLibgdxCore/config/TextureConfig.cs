using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TextureConfig
{
    const String BASE_FOLDER = "Quiz-UI/";
    const String BASE_ADAPTER_FOLDER = "Quiz-Adapter/";

    internal static List<Sprite> getAnimationsTextureAtlas(string key)
    {
        List<Sprite> list = new List<Sprite>();
        int i = 0;
        while (true)
        {
            Sprite sprite = Resources.Load<Sprite>(BASE_FOLDER + "playScreenAnimation/" + key + "_" + i);
            if (sprite != null)
            {
                list.Add(sprite);
                i++;
            }
            else
            {
                break;
            }
        }
        return list;
    }

    internal static List<Sprite> getSkillAnimationsTextureAtlas(string key)
    {
        List<Sprite> list = new List<Sprite>();
        int i = 0;
        while (true)
        {
            Sprite sprite = Resources.Load<Sprite>(BASE_ADAPTER_FOLDER + "animations/" + key + "_" + i);
            if (sprite != null)
            {
                list.Add(sprite);
                i++;
            }
            else
            {
                break;
            }
        }
        return list;
    }

    internal static Sprite getPlayScreenUITextureAtlas_findRegion(string atlasKeys)
    {
        switch (atlasKeys)
        {
            default:
                return Resources.Load<Sprite>(BASE_FOLDER + "playScreenUI/" + atlasKeys);
        }

    }

    internal static Sprite getMyNinePatch()
    {
        return Resources.Load<Sprite>(BASE_ADAPTER_FOLDER + "myNinePatch");
    }

    internal static Sprite getMaskUITextureAtlas_findRegion(string atlasKeys)
    {
        switch (atlasKeys)
        {
            default:
                return Resources.Load<Sprite>(BASE_FOLDER + "maskUI/" + atlasKeys);
        }

    }

    internal static Sprite getPlayScreenUITextureAtlas_findRegion(string atlasKeys, int index)
    {
        switch (atlasKeys)
        {
            default:
                return Resources.Load<Sprite>(BASE_FOLDER + "playScreenUI/" + atlasKeys + "_" + index);
        }
    }

    internal static Sprite[] getPlayScreenUITextureAtlas_findRegions(string atlasKeys)
    {
        List<Sprite> list = new List<Sprite>();
        int i = 0;
        while (true)
        {
            Sprite sprite = getPlayScreenUITextureAtlas_findRegion(atlasKeys, i);
            if (sprite != null)
            {
                list.Add(sprite);
                i++;
            } 
            else
            {
                break;
            }
        }
        return list.ToArray();
    }
}
