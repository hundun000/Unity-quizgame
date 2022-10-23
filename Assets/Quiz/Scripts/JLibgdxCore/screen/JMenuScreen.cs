using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JMenuScreen : BaseHundunScreen
{
    int BUTTON_WIDTH = 100;
    int BUTTON_BIG_HEIGHT = 100;
    int BUTTON_SMALL_HEIGHT = 75;

    GameObject _title;
    GameObject _buttonContinueGamePrefab;
    GameObject _buttonNewGamePrefab;
    GameObject _buttonHistorySreenPrefab;

    // ------ unity adapter member ------
    protected GameObject _buttonAreaRoot;


    override protected void Awake()
    {
        base.Awake();

        this._buttonAreaRoot = _uiRoot.transform.Find("_buttonAreaRoot").gameObject;
        this._title = _uiRoot.transform.Find("_title").gameObject;
        this._buttonContinueGamePrefab = _templates.transform.Find("_buttonAreaRoot").gameObject;
        this._buttonNewGamePrefab = _templates.transform.Find("_buttonAreaRoot").gameObject;
        this._buttonHistorySreenPrefab = _templates.transform.Find("_buttonAreaRoot").gameObject;


        _buttonContinueGamePrefab.GetComponent<Button>().onClick.AddListener(() => {
            game.gameLoadOrNew(true);
            SceneManager.LoadScene("PrepareScene");
        });

        _buttonNewGamePrefab.GetComponent<Button>().onClick.AddListener(() => {
            game.gameLoadOrNew(false);
            SceneManager.LoadScene("PrepareScene");
        });

        _buttonHistorySreenPrefab.GetComponent<Button>().onClick.AddListener(() => {
            game.gameLoadOrNew(false);
            SceneManager.LoadScene("HistoryScene");
        });
    }

    private void initScene2d()
    {

        _buttonAreaRoot.transform.AsTableClear();

        if (game.gameHasSave())
        {
            _buttonAreaRoot.transform.AsTableAdd<GameObject>(_buttonContinueGamePrefab);
        }
        _buttonAreaRoot.transform.AsTableAdd<GameObject>(_buttonNewGamePrefab);
        _buttonAreaRoot.transform.AsTableAdd<GameObject>(_buttonHistorySreenPrefab);
    }

    override protected void Start()
    {
        base.Start();

        initScene2d();
    }
}