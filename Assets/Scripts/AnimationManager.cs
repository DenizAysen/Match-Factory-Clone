using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public static Action<List<Transform>> onCollectablesReleased;

    private Transform _collectibleTransform1, _collectibleTransform2, _collectibleTransform3;
    private List<Transform> _collectedTransformList;
    [SerializeField] private float collectibleScaleAmount;
    private void OnEnable()
    {
        onCollectablesReleased += OnCollectablesReleased;
    }
    private void OnCollectablesReleased(List<Transform> list)
    {
        _collectedTransformList = list;
        StartCoroutine(CollectibleExplosionAnimationRoutine(list));
    }
    private IEnumerator CollectibleExplosionAnimationRoutine(List<Transform> colTransforms)
    {
        //foreach (Transform colTransform in colTransforms)
        //{
        //    Debug.Log(colTransform.gameObject.name + " Object postion : " + colTransform.position);
        //}
        _collectibleTransform1 = colTransforms[0];
        _collectibleTransform2 = colTransforms[1];
        _collectibleTransform3 = colTransforms[2];       

        _collectibleTransform1.DOMove(_collectibleTransform2.position, .5f);
        _collectibleTransform3.DOMove(_collectibleTransform2.position, .5f);
        yield return new WaitForSeconds(.5f);
        _collectibleTransform2.DOScale(_collectibleTransform2.localScale * collectibleScaleAmount, .5f).OnComplete(OnAnimCompleted);

    }
    private void OnAnimCompleted()
    {
        _collectibleTransform1.gameObject.SetActive(false);
        _collectibleTransform2.gameObject.SetActive(false);
        _collectibleTransform3.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        onCollectablesReleased -= OnCollectablesReleased;
    }
}
