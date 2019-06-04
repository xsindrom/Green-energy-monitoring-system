using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class UIWindowTweenAnimation : MonoBehaviour
{
    [SerializeField]
    private List<DOTweenAnimation> tweens = new List<DOTweenAnimation>();

    public bool IsPlaying
    {
        get { return tweens.Any(x => x.tween.IsPlaying()); }
    }

    public virtual void PlayAnimation(string id, UnityAction onComplete = null)
    {
        for (int i = 0; i < tweens.Count; i++)
        {
            var allTweenAnimation = tweens[i].GetTweens();
            var neededTweenAnimations = allTweenAnimation.FindAll(x => (string)x.id == id);

            var maxDuration = 0.0f;
            neededTweenAnimations.ForEach(x =>
            {
                var duration = x.Duration();
                if (duration > maxDuration)
                {
                    maxDuration = duration;
                }
            });


            var longestAnimation = neededTweenAnimations.Find(x => x.Duration() == maxDuration);
            longestAnimation.OnComplete(new TweenCallback(() =>
            {
                EventHelper.SafeCall(onComplete);
            }));

            neededTweenAnimations.ForEach(x =>
            {
                x.Restart();
                x.Play();
            });
        }
    }
    public virtual void CompletePlaying()
    {
        for(int i =0; i < tweens.Count; i++)
        {
            var tween = tweens[i];
            var playingTweens = tween.GetTweens().FindAll(x => x.IsPlaying());
            playingTweens.ForEach(x => x.Complete(true));
        }
    }

    public virtual void StopAnimation()
    {
        for(int i = 0; i < tweens.Count; i++)
        {
            var tween = tweens[i];
            DOTween.Kill(tween);
        }
    }

    public virtual void PauseAnimation()
    {
        for(int i = 0; i < tweens.Count; i++)
        {
            var tween = tweens[i];
            DOTween.Pause(tween);
        }
    }
}
