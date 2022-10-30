using hundun.quizlib.view.question;
using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.UI;

public class QuestionStemVM : MonoBehaviour
{

    private static readonly int WORD_PER_LINE = 20;

    Text stemPart;

    void Awake()
    {

        this.GetComponent<Image>().sprite = TextureConfig.getPlayScreenUITextureAtlas_findRegion(TextureAtlasKeys.PLAYSCREEN_QUESTIONSTEMBACKGROUND);

        this.stemPart = this.transform.Find("_stemPart").gameObject.GetComponent<Text>();

    }

    public void updateQuestion(QuestionView questionView)
    {
        String originText = questionView.stem;
        List<String> lines = new List<String>();
        for (int i = 0; i < originText.Length; i += WORD_PER_LINE)
        {
            String line = originText.Substring(i, Math.Min(i + WORD_PER_LINE, originText.Length));
            lines.Add(line);
        }
        if (lines.Count > 1)
        {
            lines[0] = "  " + lines[0];
        }
        stemPart.text = String.Join("\n", lines.ToArray());
    }
}
