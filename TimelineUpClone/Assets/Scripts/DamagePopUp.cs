using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Lean.Pool;
using TMPro;
using UnityEngine;

public class DamagePopUp : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI valueText;

    public void SetPopUp(int value)
    {
        valueText.text = value.ToString();
        transform.localScale = Vector3.one;
        valueText.alpha = 1;
        MovePopUp();
    }

    private void MovePopUp()
    {
        Vector3 targetPosition = transform.position + new Vector3(Random.Range(-.25f, .25f), Random.Range(.5f, 1f), 0f);

        transform.DOMove(targetPosition, .75f)
            .SetEase(Ease.OutQuad) // Yumuşak bir hareket
            .OnComplete(() =>
            {
                valueText.DOFade(0f, 0.3f);
                // Küçülerek kaybolma
                transform.DOScale(Vector3.zero, 0.3f)
                    .SetEase(Ease.InQuad)
                    .OnComplete(() => { LeanPool.Despawn(this); });
            });
    }
}