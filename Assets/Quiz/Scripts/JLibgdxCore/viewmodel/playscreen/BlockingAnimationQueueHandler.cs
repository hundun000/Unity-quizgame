using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class BlockingAnimationQueueHandler
{
    private List<Action<object>> blockingAnimationTaskQueue = new List<Action<object>>();
    public Action<object> afterAllAnimationDoneTask { private get; set; }
    public AbstractAnimationVM currentAnimationVM { private get; set; }

    public void addAnimationTask(Action<object> animationTask)
    {
        this.blockingAnimationTaskQueue.Add(animationTask);
    }

    public void checkNextAnimation()
    {
        if (blockingAnimationTaskQueue.Count > 0)
        {
            Action<object> task = blockingAnimationTaskQueue[0];
            blockingAnimationTaskQueue.RemoveAt(0);
            task.Invoke(null);
        }
        else if (afterAllAnimationDoneTask != null)
        {
            Action<object> temp = afterAllAnimationDoneTask;
            afterAllAnimationDoneTask = null;
            temp.Invoke(null);
        }
    }

    public void clear()
    {
        blockingAnimationTaskQueue.Clear();
        afterAllAnimationDoneTask = null;
    }

    public void render(float delta)
    {
        if (currentAnimationVM != null)
        {
            currentAnimationVM.updateFrame(delta);
        }
    }
}
