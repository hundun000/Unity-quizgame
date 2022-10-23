using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JPrepareScreen : MonoBehaviour
{
    private const String ownerName = "PrepareScene";

    private GameObject _JToPlayScreenButtonVM;

    

    private void ChangedActiveScene(Scene current, Scene next)
    {
        if (current.name == ownerName)
        {
            Jshow();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _JToPlayScreenButtonVM = GameObject.Find("_JToPlayScreenButtonVM");

        SceneManager.activeSceneChanged += ChangedActiveScene;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Jshow()
    {
        _JToPlayScreenButtonVM.GetComponent<JToPlayScreenButtonVM>().JsetTouchable(true);
    }
}


