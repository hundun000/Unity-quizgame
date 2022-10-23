using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JQuizGdxGame : MonoBehaviour
{
    public static JQuizGdxGame INSTANCE { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        JQuizGdxGame.INSTANCE = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
