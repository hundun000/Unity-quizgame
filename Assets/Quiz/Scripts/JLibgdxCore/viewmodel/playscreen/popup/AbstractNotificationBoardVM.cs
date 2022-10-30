using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

 public abstract class AbstractNotificationBoardVM<T> : MonoBehaviour
{
    protected QuizGdxGame game;
    protected IWaitConfirmNotificationCallback callback;

    // ------ unity adapter member ------
    //public GameObject simpleFillLabelPrefab;


    //protected virtual void Awake()
    //{

    //}
    public abstract void onCallShow(T arg);
    public interface IWaitConfirmNotificationCallback
    {
        void onNotificationConfirmed();
    }
}
