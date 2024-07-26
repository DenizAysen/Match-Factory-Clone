using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;
public class Collectable : MonoBehaviour
{
    public float JumpForce => data.jumpForce;
    public CollectableDataSo CollectableDataSo => data;
    public bool IsAnimating { get; private set; }

    //[SerializeField] private float jumpForce = 5;
    [SerializeField] private CollectableDataSo data;
    [SerializeField] private DestroyAnimationSettings destroyAnimationSettings;

    private Collider _collider;
    private Rigidbody _rigidbody;

    private PlatformManager _platformManager;

    private List<Transform> _collectableTransforms = new List<Transform>();
    private void Awake()
    {
        _platformManager = FindObjectOfType<PlatformManager>();

        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        if (!IsAnimating && !IsPointerOverUIElement())
        {
            _platformManager.OnCollectableSelected(this);
        }
    }

    private bool IsPointerOverUIElement()
    {
        var eventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }

    public void Release(List<Collectable> collectables)
    {
        StartCoroutine(ReleaseCor(collectables));
    }
    public IEnumerator ReleaseCor(List<Collectable> collectables)
    {
        yield return WaitForAnimation(collectables);
        yield return PlayDestroyAnimation(collectables[1]);
        GameObject particle = Instantiate(destroyAnimationSettings.ParticlePrefab);
        particle.transform.SetPositionAndRotation(collectables[1].transform.position, Quaternion.Euler(90f,0,0));
        SFXManager.Instance.Play(CollectableDataSo.mergeSFX);
        CollectableDataSo.OnRelease();
        Destroy(gameObject);
    }
    public void OnReachPlatform()
    {
        _rigidbody.isKinematic = true;
        _collider.enabled = false;
        IsAnimating = false;
    }
    public void SetPlatform(Platform platform)
    {
        IsAnimating = true;
        transform.DOJump(platform.PlaceHolder.position, JumpForce, 1, 1)
            .OnComplete(OnReachPlatform);
    }
    private IEnumerator WaitForAnimation(List<Collectable> collectables)
    {
        yield return new WaitWhile(() => WaitForCollectableAnims(collectables));      
        //foreach (Collectable collectable in collectables)
        //{
        //    _collectableTransforms.Add(collectable.transform);
        //}
        //transform.DOMove(animPos, .8f).SetEase(Ease.InCubic, 0).OnComplete(OnReachedRisePoint);
    }
    private IEnumerator PlayDestroyAnimation(Collectable midCollectable)
    {
        Sequence sequence = DOTween.Sequence();

        Vector3 offset = new Vector3(0, 2f, -.75f);

        Vector3 midPosition = midCollectable.transform.position + destroyAnimationSettings.AnimationOffset;

        sequence.Join(transform.DOScale(transform.localScale * .75f, destroyAnimationSettings.OffsetMoveDuration));
        sequence.Join(transform.DOLocalMove(transform.localPosition + destroyAnimationSettings.AnimationOffset, destroyAnimationSettings.OffsetMoveDuration));

        if (midCollectable == this)
        {
            sequence.AppendInterval(destroyAnimationSettings.MidMoveDuration);
        }
        else
        {
            sequence.Append(transform.DOMove(midPosition, destroyAnimationSettings.MidMoveDuration));
        }
        //sequence.AppendCallback(() => Destroy(gameObject));
        sequence.Play();

        yield return sequence.WaitForCompletion();
    }
    private bool WaitForCollectableAnims(List<Collectable> collectables)
    {
        foreach (Collectable collectable in collectables)
        {
            if (collectable.IsAnimating)
            {
                return true;
            }
        }

        return false;
    }
    private void OnDestroy() => data.ClearEvents();
}