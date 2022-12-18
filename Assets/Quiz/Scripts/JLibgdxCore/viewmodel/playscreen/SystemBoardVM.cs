using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemBoardVM : MonoBehaviour
{
    CallerAndCallback callerAndCallback;

    List<SystemButton> buttons = new List<SystemButton>();

    GameObject _nodesRoot;
    GameObject optionButtonPrefab;

    void Awake()
    {
        this._nodesRoot = this.transform.Find("_nodesRoot").gameObject;
        this.optionButtonPrefab = this.transform.Find("_templates/optionButtonPrefab").gameObject;
    }

    public void postPrefabInitialization(
        QuizGdxGame game,
        CallerAndCallback callerAndCallback
        )
    {
        this.callerAndCallback = callerAndCallback;

        SystemButton optionButton;
        for (int i = 0; i < SystemButtonTypeCompanion.types.Length; i++)
        {
            SystemButtonType type = SystemButtonTypeCompanion.types[i];

            Sprite buttonAtlasRegion;
            switch (type)
            {
                case SystemButtonType.PAUSE:
                    buttonAtlasRegion = TextureConfig.getPlayScreenUITextureAtlas_findRegion(TextureAtlasKeys.PLAYSCREEN_PAUSEBUTTON);
                    break;
                case SystemButtonType.EXIT:
                    buttonAtlasRegion = TextureConfig.getPlayScreenUITextureAtlas_findRegion(TextureAtlasKeys.PLAYSCREEN_EXITBUTTON);
                    break;
                default:
                    buttonAtlasRegion = null;
                    break;
            }

            optionButton = _nodesRoot.transform.AsTableAdd<SystemButton>(optionButtonPrefab);
            optionButton.postPrefabInitialization(game, callerAndCallback, type, buttonAtlasRegion);
            buttons.Add(optionButton);
        }

    }

    public interface CallerAndCallback
    {
        void onChooseSystem(SystemButtonType type);
    }
}

public class SystemButtonTypeCompanion
{
    public static SystemButtonType[] types = new SystemButtonType[] {
            SystemButtonType.PAUSE,
            SystemButtonType.EXIT
    };
}


public enum SystemButtonType
{
    PAUSE,
    EXIT
}
