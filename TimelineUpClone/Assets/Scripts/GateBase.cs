using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class GateBase : MonoBehaviour
{
    [SerializeField] protected int defaultValueCount;
    protected int valueCount;
    protected bool _bIsTaken = false;
    [SerializeField] protected GameObject redPlane, greenPlane;
    [SerializeField] protected TextMeshProUGUI valueCountText;
    protected bool bIsGreen = false;
    [SerializeField] protected int minValue, maxValue;
    private bool _bIsBouncing = false;

    protected void Start()
    {
        SetGateProperties();
        GameEventManager.Instance.OnLevelRestart += GameRestart;
    }

    protected void SetGateProperties()
    {
        valueCount = Mathf.Clamp(defaultValueCount, minValue, maxValue);
        if (valueCount < 0)
        {
            valueCountText.text = valueCount.ToString();
            bIsGreen = false;
            greenPlane.SetActive(false);
            redPlane.SetActive(true);
        }
        else
        {
            valueCountText.text = "+" + valueCount;
            bIsGreen = true;
            greenPlane.SetActive(true);
            redPlane.SetActive(false);
        }
    }

    protected void CloseAnimation()
    {
        transform.DOKill();
        transform.DOScale(Vector3.zero, 0.5f) // 0.5 saniyede sıfıra küçül
            .SetEase(Ease.InBack) // Geriye çekilerek küçülsün
            .OnComplete(() =>
            {
                gameObject.SetActive(false); // Objeyi kapat veya başka bir işlem yap
            });
    }

    public void TakeDamageEffect()
    {
        if (_bIsBouncing)
        {
            return;
        }

        _bIsBouncing = true;
        transform.DOScale(1.2f, 0.1f) // Obje hafif büyüsün
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                transform.DOScale(1f, 0.1f) // Eski haline dönsün
                    .SetEase(Ease.InQuad).OnComplete(() => { _bIsBouncing = false; });
            });
    }

    protected void GameRestart()
    {
        valueCount = defaultValueCount;
        transform.localScale = Vector3.one;
        transform.gameObject.SetActive(true);
        _bIsTaken = false;

        SetGateProperties();
    }
}