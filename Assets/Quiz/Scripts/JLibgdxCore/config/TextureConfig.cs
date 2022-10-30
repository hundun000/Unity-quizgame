using System;
using UnityEditor;
using UnityEngine;

public class TextureConfig
{
    const String BASE_FOLDER = "Quiz-UI/"; 

    internal static Sprite getHistoryAreaVMBackgroundDrawable()
    {
        return Resources.Load<Sprite>(BASE_FOLDER + "testNinePatch");
    }

    internal static Sprite getPlayScreenUITextureAtlas_findRegion(string atlasKeys)
    {
        switch (atlasKeys)
        {
            default:
                return Resources.Load<Sprite>(BASE_FOLDER + "playScreenUI/" + atlasKeys);
        }

    }

}
