using hundun.quizlib.prototype;
using System.Collections.Generic;
using System;
using static HistoryScreen;

public class QuizRootSaveData
{

    public MyGameSaveData gameSaveData;
    public SystemSetting systemSetting;

    public class MyGameSaveData
    {
        public List<TeamPrototype> teamPrototypes;
        public List<MatchHistoryDTO> matchFinishHistories;
    }

    public class SystemSetting
    {
        
    }

    public class Factory
    {
        public static QuizRootSaveData newGame()
        {
            var result = new QuizRootSaveData();

            var myGameSaveData = new MyGameSaveData();
            myGameSaveData.teamPrototypes = (null);
            myGameSaveData.matchFinishHistories = (new List<MatchHistoryDTO>());
            result.gameSaveData = myGameSaveData;
            var systemSetting = new SystemSetting();
            result.systemSetting = systemSetting;

            return result;
        }
    }

}