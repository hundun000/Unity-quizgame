using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NotificationCallerAndCallbackDelegation : MonoBehaviour,
    PauseNotificationBoardVM.CallerAndCallback
{

    Action<object> afterComfirmTask;

    PlayScreen owner;

    PauseNotificationBoardVM pauseNotificationBoardVM;
    //MatchFinishNotificationBoardVM waitConfirmMatchFinishMaskBoardVM;

    GameObject _pauseNotificationBoardVM;

    void Awake()
    {
        this.owner = this.GetComponentInParent<PlayScreen>();
        this._pauseNotificationBoardVM = owner.PopoupRoot.transform.Find("_pauseNotificationBoardVM").gameObject;
        this.pauseNotificationBoardVM = _pauseNotificationBoardVM.GetComponent<PauseNotificationBoardVM>();
    }


    public void callShowPauseConfirm()
    {
        generalCallShowNotificationBoard(
                    _pauseNotificationBoardVM,
                    pauseNotificationBoardVM,
                    null,
                    null
                    );
    }

    public void onNotificationConfirmed()
    {
        LibgdxFeatureExtension.log(this.GetType().Name, "onConfirmed called");
        // --- for screen ---
        foreach (Transform child in owner.PopoupRoot.transform)
        {
            child.gameObject.SetActive(false);
        }
        owner.logicFrameHelper.logicFramePause = (false);
        // --- for notificationBoardVM ---
        if (afterComfirmTask != null)
        {
            LibgdxFeatureExtension.log(this.GetType().Name, "has afterComfirmTask");
            Action<object> temp = afterComfirmTask;
            afterComfirmTask = null;
            temp.Invoke(null);
        }
    }

    private void generalCallShowNotificationBoard<T>(
                GameObject _notificationBoardVM,
                AbstractNotificationBoardVM<T> notificationBoardVM,
                T arg,
                Action<object> afterComfirmTask
                )
    {
        LibgdxFeatureExtension.log(this.GetType().Name, String.Format(
                "generalCallShowNotificationBoard called, notificationBoardVM = {0}",
                notificationBoardVM.GetType().Name
                ));
        // --- for screen ---
        _notificationBoardVM.SetActive(true);
        owner.logicFrameHelper.logicFramePause = (true);
        
        // --- for notificationBoardVM ---
        this.afterComfirmTask = afterComfirmTask;
        notificationBoardVM.onCallShow(arg);
    }
}
