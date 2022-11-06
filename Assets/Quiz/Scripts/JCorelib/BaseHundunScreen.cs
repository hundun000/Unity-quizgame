using System;
using System.Collections;
using UnityEngine;

public class BaseHundunScreen : MonoBehaviour
{

    public LogicFrameHelper logicFrameHelper;

    // ------ unity adapter member ------
    public QuizGdxGame game;
    private GameObject _popoupRoot;
    private GameObject _uiRoot;
    private GameObject _templates;

    public GameObject PopoupRoot 
    {
        get
        {
            if (_popoupRoot == null)
            {
                _popoupRoot = this.transform.Find("_popupRoot").gameObject;
            }
            return _popoupRoot;
        }
    }
    public GameObject UiRoot
    {
        get
        {
            if (_uiRoot == null)
            {
                _uiRoot = this.transform.Find("_uiRoot").gameObject;
            }
            return _uiRoot;
        }
    }
    public GameObject Templates
    {
        get
        {
            if (_templates == null)
            {
                _templates = this.transform.Find("_templates").gameObject;
            }
            return _templates;
        }
    }

    virtual protected void Awake()
    {
        // base do nothing
    }


    virtual protected void Start()
    {
        this.game = QuizGdxGame.INSTANCE;
    }

    virtual protected void Update()
    {
        float delta = Time.deltaTime;

        if (logicFrameHelper != null)
        {
            bool isLogicFrame = logicFrameHelper.logicFrameCheck(delta);
            if (isLogicFrame)
            {
                onLogicFrame();
            }
        }

        renderPopupAnimations(delta);
    }

    virtual protected void onLogicFrame()
    {
        // base-class do nothing
    }

    virtual protected void renderPopupAnimations(float delta)
    {
        // base-class do nothing
    }
}