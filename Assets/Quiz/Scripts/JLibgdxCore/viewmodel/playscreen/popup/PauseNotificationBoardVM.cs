using hundun.quizlib.view.match;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseNotificationBoardVM : AbstractNotificationBoardVM<object>
{
    MatchSituationView data;
    Image image;

    // ------ unity adapter member ------
    GameObject _image;

    void Awake()
    {
        this.callback = this.GetComponentInParent<PlayScreen>().notificationCallerAndCallback;
        this._image = this.transform.Find("_image").gameObject;
        this.image = _image.GetComponent<Image>();

        image.sprite = TextureConfig.getPlayScreenUITextureAtlas_findRegion(TextureAtlasKeys.PLAYSCREEN_POPUP_PLAY);
        _image.GetComponent<Button>().onClick.AddListener(() => {
            callback.onNotificationConfirmed();
        });
    }



    public override void onCallShow(object arg)
    {
        // do nothing
    }


    public interface CallerAndCallback : IWaitConfirmNotificationCallback
    {
        void callShowPauseConfirm();
    }
}
