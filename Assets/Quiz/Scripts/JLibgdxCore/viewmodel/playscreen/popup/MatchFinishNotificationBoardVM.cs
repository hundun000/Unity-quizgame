using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static HistoryScreen;

public class MatchFinishNotificationBoardVM : AbstractNotificationBoardVM<MatchHistoryDTO>
{
    MatchHistoryDTO data;

    MatchHistoryVM vm;

    Button button;
    Text buttonText;
    Text title;
    GameObject _vmContainer;

    public GameObject matchHistoryVMPrefab;

    void Awake()
    {
        this.callback = this.GetComponentInParent<PlayScreen>().notificationCallerAndCallback;

        this.title = this.transform.Find("_title").gameObject.GetComponent<Text>();
        this.button = this.transform.Find("_textButton").gameObject.GetComponent<Button>();
        this.buttonText = this.transform.Find("_textButton/_text").gameObject.GetComponent<Text>();
        this._vmContainer = this.gameObject;

        title.text = "比赛记录";
        buttonText.text = "离开并保存";
        button.onClick.AddListener(() => {
            this.callback.onNotificationConfirmed();
        });
    }

    override public void onCallShow(MatchHistoryDTO history)
    {
        //this.setVisible(true);
        this.data = history;
        this.vm = _vmContainer.transform.AsTableAdd<MatchHistoryVM>(matchHistoryVMPrefab);
        vm.transform.localPosition = new Vector3(0, 0, 0);
        MatchHistoryVM.Factory.fromBO(this.vm, game, history);

    }
    
    
    public interface CallerAndCallback : IWaitConfirmNotificationCallback
    {
        void callShowMatchFinishConfirm();
    }
}
