using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static QuestionResourceAreaVM;

public class AudioResourceNode : MonoBehaviour
{
    private AudioSource music;
    Action<object> onCompletionListener;
    int playCount;
    Text timeLabel;
    Button playButton;
    Text playButtonText;
    Image animationImage;
    List<Sprite> animations;

    private NumberFormat format;

    bool fakeOnCompletionListenerFlag;
    IAudioCallback callback;

    private AudioClip getFile(String key)
    {
        int extendDotIndex = key.LastIndexOf('.');
        if (extendDotIndex > 0)
        {
            key = key.Substring(0, extendDotIndex);
        } 
        String path = "quiz/audios/" + key;
        return (AudioClip)Resources.Load(path);
    }

    public void stopAudio()
    {
        if (music != null)
        {
            music.Stop();
            //music.dispose();
        }
        playCount = 0;
    }

    void Awake()
    {
        this.music = this.GetComponent<AudioSource>();
        this.timeLabel = this.transform.Find("_timeLabel").GetComponent<Text>();
        this.playButton = this.transform.Find("_playButton").GetComponent<Button>();
        this.playButtonText = this.transform.Find("_playButton/_text").GetComponent<Text>();
        this.animationImage = this.transform.Find("_animationImage").GetComponent<Image>();
    }

    public void postPrefabInitialization(
        QuizGdxGame game,
        IAudioCallback callback
        )
    {
        this.callback = callback;
    }

    public void postParentCallActive()
    {

        this.format = NumberFormat.getFormat(1, 1);
        this.playButtonText.text = "²¥·Å";
        Sprite[] animationAtlasRegions = TextureConfig.getPlayScreenUITextureAtlas_findRegions(TextureAtlasKeys.PLAYSCREEN_AUDIO);
        this.animations = animationAtlasRegions
                .Where(it => it != null)
                .ToList();

        playButton.onClick.AddListener(() => 
        {
            if (!music.isPlaying)
            {
                music.Play();
                playCount++;
                LibgdxFeatureExtension.log(this.GetType().Name, "playCount change to " + playCount);
                fakeOnCompletionListenerFlag = false;
            }
        });

        onCompletionListener = (voidIt) => {
            if (this.playCount >= 1) {
                callback.onFirstPlayDone();
            }
        };


    }
        
    public void updateTimer()
    {
        String text;
        if (!music.isPlaying)
        {
            text = "";
            animationImage.sprite = (animations[0]);
        }
        else
        {
            text = format.format(music.time);
            int imageIndex = ((int)music.time) % animations.Count;
            animationImage.sprite = (animations[imageIndex]);
        }
        timeLabel.text = (text);
    }


    void Update()
    {
        updateTimer();
        fakeOnCompletionListener();
    }

    private void fakeOnCompletionListener()
    {
        if (fakeOnCompletionListenerFlag == false && music != null && music.clip != null && music.clip.length - music.time < 0.001)
        {
            onCompletionListener.Invoke(null);
            fakeOnCompletionListenerFlag = true;
        }
    }

    public void updateResource(String data)
    {
        stopAudio();
        AudioClip clip = getFile(data);
        music.clip = clip;
    } 

    
}
