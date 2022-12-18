using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractAnimationVM<T_CALL_ARG> : AbstractAnimationVM
{
    public abstract void callShow(T_CALL_ARG arg);
}
public abstract class AbstractAnimationVM : MonoBehaviour
{
    private IAnimationCallback callback;
    
    protected QuizGdxGame game;
    public bool runningState { get; protected set; }
    // FIXME
    protected GdxAnimation<Sprite> myAnimation;
    private float stateTime;


    Image background;
    

    virtual protected void Awake()
    {
        this.background = this.transform.Find("_background").GetComponent<Image>();
        
    }

    public void postPrefabInitialization(QuizGdxGame game, IAnimationCallback callback)
    {
        this.game = game;
        this.callback = callback;
    }
    

    public void resetFrame()
    {
        // Instantiate a SpriteBatch for drawing and reset the elapsed animation
        // time to 0
        runningState = true;
        stateTime = 0f;
        
        Sprite currentFrame = myAnimation.getKeyFrame(stateTime);
        this.background.sprite = (currentFrame);
    }

    public void updateFrame(float delta)
    {
        stateTime += delta; // Accumulate elapsed animation time

        if (!myAnimation.isAnimationFinished(stateTime))
        {
            Sprite currentFrame = myAnimation.getKeyFrame(stateTime);
            //float rate = 1.0f * animation.getKeyFrameIndex(stateTime) / animation.getKeyFrames().length;
            this.background.sprite = (currentFrame);
            //this.setScale(1.0f + 1.0f * rate);
        }
        else
        {
            runningState = false;
            callback.onAnimationDone();
        }

    }



    public interface IAnimationCallback
    {
        void onAnimationDone();
    }

    public static GdxAnimation<Sprite> aminationFactory(String id, float frameDuration, PlayMode playMode)
    {
        List<Sprite> drawableArray = TextureConfig.getAnimationsTextureAtlas(id);
        return new GdxAnimation<Sprite>(frameDuration, drawableArray, playMode);
    }

    public static GdxAnimation<Sprite> skillAminationFactory(String id, float frameDuration, PlayMode playMode)
    {
        List<Sprite> drawableArray = TextureConfig.getSkillAnimationsTextureAtlas(id);
        return new GdxAnimation<Sprite>(frameDuration, drawableArray, playMode);
    }

    public GdxAnimation<Sprite> aminationFactoryBySumTime(String id,
            float second, PlayMode playMode)
    {
        List<Sprite> drawableArray = TextureConfig.getAnimationsTextureAtlas(id);
        float frameDuration = second / drawableArray.Count;
        return new GdxAnimation<Sprite>(frameDuration, drawableArray, playMode);
    }
}

