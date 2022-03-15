using Assets.Scripts;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnimationsManager : Singleton<AnimationsManager>
{

    public void FadeOut(float time, CanvasGroup canvasGroup, float endValue = 0)
    {
        StartCoroutine(FadeOutCoroutine(time, canvasGroup, endValue));
    }

    public void AlphaLerpeCanvasGroup(float time, CanvasGroup canvasGroup, float startValue, float endValue)
    {
        StartCoroutine(AlphaLerpeCanvasGroupCoroutine(time, canvasGroup, startValue, endValue));
    }

    public IEnumerator GoToDestination(Transform objectToMove, float time, Vector3 positionTarget, bool isLocal = true, AnimationCurve curve = null)
    {
        var startPosition = isLocal ? objectToMove.localPosition : objectToMove.position;

        var startTime = Time.time;
        float fractionOfJourney;
        yield return null; // allow use to not have 0 as distCovered
        do
        {
            fractionOfJourney = (Time.time - startTime) / time;
            var newPosition = Vector3.Lerp(startPosition, positionTarget, curve?.Evaluate(fractionOfJourney) ?? fractionOfJourney);

            if (isLocal)
                objectToMove.localPosition = newPosition;
            else
                objectToMove.position = newPosition;
            
            yield return null;
        }
        while (fractionOfJourney < 0.99f);
    }

    public IEnumerator FadeOutCoroutine(float time, CanvasGroup canvasGroup, float endValue = 0)
    {
        float startTime = Time.time;
        float fractionOfJourney;
        do
        {
            fractionOfJourney = (Time.time - startTime) / time;

            canvasGroup.alpha = 1 - fractionOfJourney * (1 - endValue);

            yield return null;
        }
        while (fractionOfJourney < 0.99f);
        canvasGroup.alpha = endValue;
    }

    public IEnumerator HeartBit2DCoroutine(Transform target, float time, float valueAdd, int countHeartBit)
    {
        float startTime = Time.time;
        float fractionOfJourney;
        float direction = 1;
        Vector3 startValue = target.localScale;
        for (int i = 0; i < countHeartBit * 2; i++)
        {
            Vector3 endValue = new Vector3(startValue.x + valueAdd * direction, startValue.y + valueAdd * direction, 1);
            do
            {
                fractionOfJourney = (Time.time - startTime) / time;

                target.localScale = Vector3.Lerp(startValue, endValue, fractionOfJourney);
                yield return null;
            }
            while (fractionOfJourney < 0.99f);
            direction *= -1;
            startValue = endValue;
            startTime = Time.time;
        }
    }

    public IEnumerator AlphaLerpeCanvasGroupCoroutine(float time, CanvasGroup canvasGroup, float startValue, float endValue)
    {
        float startTime = Time.time;
        float fractionOfJourney;
        do
        {
            fractionOfJourney = (Time.time - startTime) / time;

            canvasGroup.alpha = startValue - fractionOfJourney * (startValue - endValue);

            yield return null;
        }
        while (fractionOfJourney < 0.99f);

        if (canvasGroup != null)
            canvasGroup.alpha = endValue;
    }

    public IEnumerator AlphaLerpeGraphicsCoroutine(float time, Graphic graphic, float startValue, float endValue)
    {
        float startTime = Time.time;
        float fractionOfJourney;
        do
        {
            fractionOfJourney = (Time.time - startTime) / time;

            graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, startValue - fractionOfJourney * (startValue - endValue));

            yield return null;
        }
        while (fractionOfJourney < 0.99f);
        graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, endValue);
    }
    public IEnumerator ColorLerpeGraphicsCoroutine(float time, Graphic graphic, Color startValue, Color endValue)
    {
        float startTime = Time.time;
        float fractionOfJourney;
        do
        {
            fractionOfJourney = (Time.time - startTime) / time;

            graphic.color = Color.Lerp(startValue, endValue , fractionOfJourney);

            yield return null;
        }
        while (fractionOfJourney < 0.99f);
        graphic.color = endValue;
    }

    public IEnumerator ScaleCoroutine(float time, Transform transform, Vector3 startValue, Vector3 endValue)
    {
        float startTime = Time.time;
        float fractionOfJourney;
        do
        {
            fractionOfJourney = (Time.time - startTime) / time;

            transform.localScale = Vector3.Lerp(startValue, endValue, fractionOfJourney);

            yield return null;
        }
        while (fractionOfJourney < 0.99f);
        transform.localScale = endValue;
    }
}