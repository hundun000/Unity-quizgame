using hundun.quizlib.prototype;
using System.Collections.Generic;
using System;

public class JQuizRootSaveData
{

    public MyGameSaveData gameSaveData;
    public SystemSetting systemSetting;

    public class MyGameSaveData
    {
        public List<TeamPrototype> teamPrototypes;
        public List<JMatchHistoryDTO> matchFinishHistories;
    }

    public class SystemSetting
    {
        
    }

    public class Factory
    {
        public static JQuizRootSaveData newGame()
        {
            var result = new JQuizRootSaveData();

            var myGameSaveData = new MyGameSaveData();
            myGameSaveData.teamPrototypes = (null);
            myGameSaveData.matchFinishHistories = (new List<JMatchHistoryDTO>());
            result.gameSaveData = myGameSaveData;
            var systemSetting = new SystemSetting();
            result.systemSetting = systemSetting;

            return result;
        }
    }

}