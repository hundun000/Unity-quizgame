using hundun.quizlib.prototype;
using System.Collections.Generic;
using System;
using static HistoryScreen;

[Serializable]
public class QuizRootSaveData
{

    public MyGameSaveData gameSaveData;
    public SystemSetting systemSetting;

    public QuizRootSaveData()
    {
    }

    public QuizRootSaveData(MyGameSaveData gameSaveData, SystemSetting systemSetting)
    {
        this.gameSaveData = gameSaveData;
        this.systemSetting = systemSetting;
    }

    [Serializable]
    public class MyGameSaveData
    {
        public List<TeamPrototype> teamPrototypes;
        public List<MatchHistoryDTO> matchFinishHistories;
    }

    [Serializable]
    public class SystemSetting
    {
        public String env;
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
            systemSetting.env = TextureConfig.DEFAULT_ENV;
            result.systemSetting = systemSetting;

            return result;
        }
    }

}