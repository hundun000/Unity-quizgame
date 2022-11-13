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

    public GameObject matchHistoryVMPrefab;

    void Awake()
    {
        this.callback = this.GetComponentInParent<PlayScreen>().notificationCallerAndCallback;

        this.title = this.transform.Find("_title").gameObject.GetComponent<Text>();
        this.button = this.transform.Find("_textButton").gameObject.GetComponent<Button>();
        this.buttonText = this.transform.Find("_textButton/_text").gameObject.GetComponent<Text>();

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
        this.vm = this.transform.AsTableAdd<MatchHistoryVM>(matchHistoryVMPrefab);
        MatchHistoryVM.Factory.fromBO(this.vm, game, history);

    }
    
    
    public interface CallerAndCallback : IWaitConfirmNotificationCallback
    {
        void callShowMatchFinishConfirm();
    }
}
