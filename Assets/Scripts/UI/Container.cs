using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Container : MonoBehaviour
{
    [SerializeField] private Image progImage;
    [SerializeField] private TMP_Text countText;

    private CollectableDataSo _collectableDataSo;
    private int _releaseCount;
    private int _count;
    public void Initialize(CollectableDataSo collectable , int requiredCount)
    {
        _collectableDataSo = collectable;
        _count = requiredCount;

        progImage.sprite = collectable.sprite;
        countText.text = requiredCount.ToString();

        _collectableDataSo.OnReleaseEvent += OnReleaseCollectable;
    }

    private void OnReleaseCollectable()
    {
        _releaseCount++;
        if(_releaseCount % 3 == 0)
        {
            DecreaseCount();
        }
    }
    private void DecreaseCount()
    {
        _count--;
        if(_count == 0)
        {
            Destroy(gameObject);
            return;
        }
        countText.text = _count.ToString();
    }
}
