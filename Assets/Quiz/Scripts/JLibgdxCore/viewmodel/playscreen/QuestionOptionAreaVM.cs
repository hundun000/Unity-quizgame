using hundun.quizlib.view.question;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static QuestionOptionAreaVM;
using System.Linq;
using hundun.quizlib;
using UnityEngine.UI;

public class QuestionOptionAreaVM : MonoBehaviour
{

    QuizGdxGame game;
    CallerAndCallback callerAndCallback;
    
    List<OptionNode> nodes;

    static readonly int SIZE = 4;

    public static int NODE_WIDTH = 700;
    public static int NODE_HEIGHT = 100;

    GameObject _nodesRoot;
    GameObject optionButtonPrefab;
    void Awake()
    {
        PlayScreen playScreen = this.GetComponentInParent<PlayScreen>();
        this.game = playScreen.game;
        this.callerAndCallback = this.GetComponentInParent<QuizInputHandler>();

        this._nodesRoot = this.transform.Find("_nodesRoot").gameObject;
        this.optionButtonPrefab = this.transform.Find("_templates/optionButtonPrefab").gameObject;

        //setBackground(background);
        this.GetComponent<Image>().enabled = false;

        nodes = new List<OptionNode>();
        for (int i = 0; i < SIZE; i++)
        {
            OptionNode vm = _nodesRoot.transform.AsTableAdd<OptionNode>(optionButtonPrefab);
            vm.postPrefabInitialization(game, i, callerAndCallback);
            nodes.Add(vm);

        }

    }

    public void updateQuestion(QuestionView questionView)
    {
        for (int i = 0; i < SIZE; i++)
        {
            String optiontext = questionView.options[i];
            nodes[i].updateForNewQuestion(optiontext, i == questionView.answer);
        }
    }

    public enum OptionButtonShowState
    {
        SHOW_CORRECT,
        SHOW_WRONG,
        HIDE_CORRECT,
        HIDE_WRONG
        
    }

    
    public interface CallerAndCallback
    {
        void onChooseOption(int index);
    }

    public void showRandomOption(int showOptionAmout)
    {
        List<int> showIndexs = Enumerable.Range(0, SIZE).ToList()
            .Where(index => nodes[index].showState == OptionButtonShowState.HIDE_WRONG)
            .ToList()
            ;
        if (showIndexs.Count < showOptionAmout)
        {
            return;
        }
        showIndexs.Shuffle();
        showIndexs = showIndexs.GetRange(0, showOptionAmout);

        showIndexs.ForEach(showIndex => {
            OptionNode node = nodes.get(showIndex);
            node.updateShowStateToShow();
        });
    }
    
    public void showAllOption()
    {
        nodes.ForEach(node => {
            node.updateShowStateToShow();
        });
    }


}
