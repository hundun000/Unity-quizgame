using System.Collections;
using UnityEngine;

public class BaseHundunScreen : MonoBehaviour
{



    // ------ unity adapter member ------
    protected JQuizGdxGame game;
    protected GameObject _popoupRoot;
    protected GameObject _uiRoot;

    virtual protected void Awake()
    {
        this._popoupRoot = GameObject.Find("_popupRoot");
        this._uiRoot = GameObject.Find("_uiRoot");
    }

    virtual protected void Start()
    {
        this.game = JQuizGdxGame.INSTANCE;
    }
}