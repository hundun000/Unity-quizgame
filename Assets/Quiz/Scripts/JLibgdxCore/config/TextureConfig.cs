using System;
using UnityEditor;
using UnityEngine;

public class TextureConfig
{
    internal static Sprite getHistoryAreaVMBackgroundDrawable()
    {
        return Resources.Load<Sprite>("Quiz/testNinePatch");
    }

    internal static Sprite getPlayScreenUITextureAtlas_findRegion(string atlasKeys)
    {
        switch (atlasKeys)
        {
            default:
                return Resources.Load<Sprite>("Quiz/playScreenUI/" + atlasKeys);
        }

    }

}
