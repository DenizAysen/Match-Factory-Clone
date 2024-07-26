using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    #region Properties
    public bool IsEmpty { get; private set; } = true;

    public Collectable Collectable => _collectable;
    public Transform PlaceHolder => placeHolder;
    #endregion

    #region Serialized Fiedls

    [Header("Place Holders")]

    [SerializeField] private Transform placeHolder;

    [SerializeField] private Transform animPlaceHolder; 
    #endregion

    private Collectable _collectable;

    public Action onPlatformEmptied;
    public void SetCollectable(Collectable collectable)
    {
        IsEmpty = false;
        _collectable = collectable;
        _collectable.SetPlatform(this);
    }

    private void OnCollactableReleased()
    {
        //Debug.Log("Collectable Released"); 
    }
    private void OnEnable()
    {
        onPlatformEmptied += OnPlatformEmtied;
    }

    private void OnPlatformEmtied() => IsEmpty = true;

    public void RelaseCollectable(List<Platform> platforms)
    {
        if (IsEmpty)
            return;

        List<Collectable> collectables = new List<Collectable>();
        foreach (Platform platform in platforms)
        {
            collectables.Add(platform.Collectable);
        }
        _collectable.Release(collectables);
        
        IsEmpty = true;
    }
    private void OnDisable()
    {
        onPlatformEmptied -= OnPlatformEmtied;
    }
}