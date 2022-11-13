using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TextureConfig
{
    const String BASE_FOLDER = "Quiz-UI/";

    internal static Sprite getAnimationsTextureAtlas(string key)
    {
        // FIXME temp always _0
        return Resources.Load<Sprite>(BASE_FOLDER + "playScreenAnimation/" + key + "_0");
    }

    internal static Sprite getPlayScreenUITextureAtlas_findRegion(string atlasKeys)
    {
        switch (atlasKeys)
        {
            default:
                return Resources.Load<Sprite>(BASE_FOLDER + "playScreenUI/" + atlasKeys);
        }

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
