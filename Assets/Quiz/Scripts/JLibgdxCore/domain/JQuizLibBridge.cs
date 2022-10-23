

using hundun.quizlib;
using hundun.quizlib.context;
using hundun.quizlib.exception;
using hundun.quizlib.service;
using System;
using System.Text.RegularExpressions;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using static JQuizRootSaveData;
using static JQuizSaveHandler;

public class QuizLibBridge : IFrontEnd, ISubGameSaveHandler
{

    public QuizComponentContext quizComponentContext { get; private set; }
    public JLibDataConfiguration libDataConfiguration { get; private set; }
    public QuizLibBridge(JQuizGdxGame game) {

        try {
            this.quizComponentContext = QuizComponentContext.Factory.create(this);
            this.libDataConfiguration = new JLibDataConfiguration(quizComponentContext);
        } catch (QuizgameException e) {
            Debug.LogErrorFormat("[{0}] {1} {2}", this.GetType().Name, "QuizgameException", e);
        }
        
        game.saveHandler.registerSubHandler(this);
    }




    string[] IFrontEnd.fileGetChilePathNames(string folder)
    {
        TextAsset txt = (TextAsset)Resources.Load(folder + QuestionLoaderService.FOLDER_CHILD_HINT_FILE_NAME, typeof(TextAsset));
        String listContent = txt.text;
        Regex regex = new Regex("\r?\n|\r");
        String[] result = regex.Split(listContent);
        Debug.LogFormat("[{0}] {1}", this.GetType().Name, "fileGetChilePathNames result = " + JavaFeatureExtension.ArraysAsList(result));
        return result;
    }

    string IFrontEnd.fileGetContent(string filePath)
    {
        TextAsset txt = (TextAsset)Resources.Load(filePath, typeof(TextAsset));
        string content = txt.text;
        return content;
    }

    void ISubGameSaveHandler.applyGameSaveData(MyGameSaveData myGameSaveData)
    {
        try
        {
            libDataConfiguration.registerForSaveData(myGameSaveData.teamPrototypes);
            Debug.LogFormat("[{0}] {1}", this.GetType().Name, String.Format(
                    "applyGameSaveData TeamPrototypes.size = %s",
                    myGameSaveData.teamPrototypes != null ? myGameSaveData.teamPrototypes.Count : null
                    ));
        }
        catch (QuizgameException e)
        {
            Debug.LogErrorFormat("[{0}] {1} {2}", this.GetType().Name, "QuizgameException", e);
        }
    }




    void ISubGameSaveHandler.currentSituationToSaveData(MyGameSaveData myGameSaveData)
    {
        myGameSaveData.teamPrototypes = (quizComponentContext.teamService.listTeams());
    }
}