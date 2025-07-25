using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BobTween : MonoBehaviour
{

    [SerializeField] private float amplitude = 0.15f;
    [SerializeField] private float period = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        float startY = transform.localPosition.y;

        transform.DOLocalMoveY(startY + amplitude, period * 0.5f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }
}
