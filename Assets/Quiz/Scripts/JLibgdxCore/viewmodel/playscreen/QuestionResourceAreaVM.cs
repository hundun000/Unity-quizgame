using hundun.quizlib;
using hundun.quizlib.prototype.question;
using hundun.quizlib.view.question;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.UI;

public class QuestionResourceAreaVM : MonoBehaviour
{
    QuizGdxGame game;
    IAudioCallback callback;

    Dictionary<ResourceType, Sprite> backgroundMap = new Dictionary<ResourceType, Sprite>();
    ImageResourceNode imageResourceNode;
    AudioResourceNode audioResourceNode;
    Image background;

    void Awake()
    {
        audioResourceNode = this.transform.Find("_audioResourceNode").GetComponent<AudioResourceNode>();
        imageResourceNode = this.transform.Find("_imageResourceNode").GetComponent<ImageResourceNode>();
        background = this.GetComponent<Image>();
    }

    public void postPrefabInitialization(
        QuizGdxGame game,
        IAudioCallback callback
        )
    {
        audioResourceNode.postPrefabInitialization(game, callback);

        this.game = game;
        this.callback = callback;

        Sprite background = TextureConfig.getPlayScreenUITextureAtlas_findRegion(
                TextureAtlasKeys.PLAYSCREEN_ZACAMUSUME_Q
                );
        backgroundMap.put(ResourceType.NONE, background);
        backgroundMap.put(ResourceType.IMAGE, background);
        backgroundMap.put(ResourceType.VOICE, background);

        

    }

    public void stopAudio()
    {
        audioResourceNode.stopAudio();
        audioResourceNode.gameObject.SetActive(false);
    }

    public void updateQuestion(QuestionView questionView)
    {
        Sprite newBackground = backgroundMap.get(questionView.resource.type);
        this.background.sprite = (newBackground);

        imageResourceNode.gameObject.SetActive(false);
        audioResourceNode.gameObject.SetActive(false);

        if (questionView.resource.type == ResourceType.IMAGE)
        {
            imageResourceNode.gameObject.SetActive(true);
            imageResourceNode.postParentCallActive();

            imageResourceNode.updateResource(questionView.resource.data);
        }
        else if (questionView.resource.type == ResourceType.VOICE)
        {
            audioResourceNode.gameObject.SetActive(true);
            audioResourceNode.postParentCallActive();

            audioResourceNode.updateResource(questionView.resource.data);
            callback.onPlayReady();
        }
    }

    public interface IAudioCallback
    {
        void onFirstPlayDone();
        void onPlayReady();
    }
}
