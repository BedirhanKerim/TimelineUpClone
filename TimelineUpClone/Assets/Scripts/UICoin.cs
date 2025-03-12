using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;

public class UICoin : MonoBehaviour
{
    private Tween _moveTween;
    private Tween _moveTweenX;
    private Tween _moveTweenY;

    private Vector3 _initPos;

    private CancellationTokenSource _cancellationTokenSource;
    private CancellationToken _cancellationToken;

    private void Awake()
    {
        _initPos = transform.position;
        _cancellationTokenSource = new();
        _cancellationToken = _cancellationTokenSource.Token;
    }

    public void Move(Vector3 origin, Vector3 currentPos, Vector3 targetPos, float duration, Action callback,
        int coinAmount)
    {
        _moveTween?.Kill();
        _moveTweenX?.Kill();
        _moveTweenY?.Kill();

        transform.position = origin;
        float deltaX = (currentPos - origin).x;
        deltaX *= -1;

        _moveTween = transform.DOMove(currentPos, duration)
            .OnComplete(() => { _ = MoveToTheEndPoint(currentPos, targetPos, deltaX, callback, coinAmount); });
    }


    private async UniTaskVoid MoveToTheEndPoint(Vector3 startPos, Vector3 endPos, float height, Action callback,
        int coinAmount)
    {
        var _timerForParabola = 0f;

        try
        {
            while (transform != null)
            {
                if (Vector3.Distance(transform.position, endPos) > 20)
                {
                    _timerForParabola += Time.deltaTime * 1f;
                    transform.position = GetParabolaPosition(startPos, endPos, height, _timerForParabola);
                    await UniTask.Yield(_cancellationToken);
                }
                else
                {
                    if (callback != null)
                    {
                        callback();
                    }

                    GameEventManager.Instance.CoinEarned(1);
                    Destroy(this.gameObject);
                    break;
                }
            }
        }
        catch
        {
            //ignored
        }
    }


    private Vector3 GetParabolaPosition(Vector3 startPos, Vector3 endPos, float height, float time)
    {
        float parabolicTime = time * 2 - 1;

        if (Mathf.Abs(startPos.y - endPos.y) < 0.1f) //SAME LEVEL
        {
            Vector3 travelDirection = endPos - startPos;
            Vector3 result = startPos + time * travelDirection;
            result.y += (-parabolicTime * parabolicTime + 1) * height;
            return result;
        }
        else //DIFFERENT LEVEL
        {
            Vector3 travelDirection = endPos - startPos;
            Vector3 levelDirecteion = endPos - new Vector3(startPos.x, endPos.y, startPos.z);
            Vector3 right = Vector3.Cross(travelDirection, levelDirecteion);
            Vector3 up = Vector3.Cross(right, travelDirection);
            if (endPos.y > startPos.y) up = -up;
            Vector3 result = startPos + time * travelDirection;
            result += ((-parabolicTime * parabolicTime + 1) * height) * up.normalized;
            return result;
        }
    }
}