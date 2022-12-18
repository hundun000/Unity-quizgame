using hundun.quizlib.prototype.match;
using hundun.quizlib.service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HistoryScreen : BaseHundunScreen
{

    List<MatchHistoryDTO> histories { get; set; }

    HistoryAreaVM historyAreaVM;

    Button toNextScreenButton;
    

    public class MatchHistoryDTO
    {
        public Dictionary<String, int> data { get; set; }
    }

    override protected void Awake()
    {
        base.Awake();

        this.historyAreaVM = this.UiRoot.transform.Find("_historyAreaVM").GetComponent<HistoryAreaVM>();
        this.toNextScreenButton = this.UiRoot.transform.Find("_toNextScreenButton").GetComponent<Button>();
    }

    override protected void Start()
    {
        base.Start();

        this.histories = new List<MatchHistoryDTO>();
        toNextScreenButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MenuScene");
        });

        // FIXME fake as first screen
        game.gameLoadOrNew(false);
        MatchHistoryDTO matchHistoryDTO = new MatchHistoryDTO();
        matchHistoryDTO.data = new Dictionary<String, int>();
        matchHistoryDTO.data.Add(LibDataConfiguration.ZACA_TEAM_NAME_1, 114);
        matchHistoryDTO.data.Add(LibDataConfiguration.ZACA_TEAM_NAME_2, 514);
        LibgdxFeatureExtension.SetScreenChangePushParams(new object[] { matchHistoryDTO });

        MatchHistoryDTO newHistory = (MatchHistoryDTO)LibgdxFeatureExtension.GetScreenChangePushParams()[0];
        LibgdxFeatureExtension.log(this.GetType().Name, String.Format(
                "pushParams by newHistory = {0}",
                newHistory.ToString()
                ));
        addNewHistory(newHistory);

        


        historyAreaVM.updateScrollPane(game, histories);

    }



    private void addNewHistory(MatchHistoryDTO history)
    {
        histories.Add(history);
    }
}
