using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HistoryScreen;
using static QuizRootSaveData;
using static QuizSaveHandler;

public class HistoryScreenAsSubGameSaveHandlerDelegation : ISubGameSaveHandler
{
    internal List<MatchHistoryDTO> histories;

    public void applyGameSaveData(MyGameSaveData myGameSaveData)
    {
        histories = myGameSaveData.matchFinishHistories;
        LibgdxFeatureExtension.log(this.GetType().Name, string.Format(
                "applyGameSaveData histories.size = {0}",
                histories.Count
                ));
    }

    public void currentSituationToSaveData(MyGameSaveData myGameSaveData)
    {
        myGameSaveData.matchFinishHistories = (histories);
    }
}
