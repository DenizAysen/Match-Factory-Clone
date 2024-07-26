using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private List<Platform> platforms;
    [SerializeField] private CollectableDataSo[] collectableDatas;
    [SerializeField] private float repositionAnimationStandbyTime = 1f;
    #endregion

    #region Collections
    private Dictionary<int, int> collectableIDs = new Dictionary<int, int>();
    private Dictionary<int, List<Platform>> _platformMap = new Dictionary<int, List<Platform>>();
    private List<int> idResetList = new List<int>();
    private List<Platform> sortList = new List<Platform>();
    private List<Collectable> collactablesToReposition = new List<Collectable>();
    #endregion
    public static Action onGameOver;
    private void Start()
    {
        for (int i = 0; i < collectableDatas.Length; i++)
        {
            collectableIDs.Add(collectableDatas[i].id, 0);
        }
    }
    public void OnCollectableSelected(Collectable collectable)
    {
        foreach (Platform platform in platforms)
        {
            if (platform.IsEmpty)
            {
                platform.SetCollectable(collectable);
                CheckPlatforms();
                break;
            }
        }
    }
    private void CheckPlatforms()
    {
        foreach (Platform platform in platforms)
        {
            if (!platform.IsEmpty)
            {
                foreach (CollectableDataSo collectableData in collectableDatas)
                {
                    if (collectableData == platform.Collectable.CollectableDataSo)
                    {
                        int collectableId = collectableData.id;
                        collectableIDs[collectableId]++;
                        idResetList.Add(collectableId);

                        if (!_platformMap.TryAdd(collectableId, new List<Platform> { platform }))
                        {
                            _platformMap[collectableId].Add(platform);                            
                            FindCollectablestoSort(collectableId);
                        }
                    }
                }
            }
            else
            {
                break;
            }
        }
        if (AllPlatformsFilled())
        {
            StartCoroutine(LoseRoutine());
        }
        _platformMap.Clear();
        ResetIDList();
    }
    private IEnumerator LoseRoutine()
    {
        yield return new WaitForSeconds(1f);
        if (AllPlatformsFilled())
            onGameOver?.Invoke();
    }
    private bool AllPlatformsFilled()
    {
        int _notEmptyPlatformCount = 0;
        foreach (Platform platform in platforms)
        {
            if (!platform.IsEmpty)
            {
                _notEmptyPlatformCount++;
            }
        }
        if (_notEmptyPlatformCount == platforms.Count)
            return true;

        return false;
    }
    private void ResetIDList()
    {
        for (int i = 0; i < idResetList.Count; i++)
        {
            collectableIDs[idResetList[i]] = 0;
        }

        idResetList.Clear();
    }
    private void ReleasePlatforms(List<Platform> platforms)
    {
        foreach (Platform platform in platforms)
        {
            platform.RelaseCollectable(platforms);
        }
    }
    private void FindCollectablestoSort(int collectibleID)
    {       
        Platform secondPlatform = _platformMap[collectibleID][1];
        int index = 0;
        if (collectableIDs[collectibleID] == 2)
        {
            Platform firstPlatform = _platformMap[collectibleID][0];

            for (int i = 0; i < platforms.Count; i++)
            {
                if ((firstPlatform == platforms[i] && secondPlatform == platforms[i + 1]))
                {
                    return;
                }

                else if (firstPlatform == platforms[i])
                {
                    index = i+1; break;
                }
            }
            for (int i = index; i< platforms.Count; i++)
            {
                if (!platforms[i].IsEmpty)
                {
                    sortList.Add(platforms[i]);
                }
                else
                    break;
            }
            SortAndRelaseCollectables(collectibleID);
        }
        else if(collectableIDs[collectibleID] == 3)
        {
            Platform thirdPlatform = _platformMap[collectibleID][2];
            for (int i = 0; i < platforms.Count; i++)
            {
                if ((secondPlatform == platforms[i] && thirdPlatform == platforms[i + 1]))
                {
                    ReleasePlatforms(_platformMap[collectibleID]);
                    return;
                }

                else if (secondPlatform == platforms[i])
                {
                    index = i + 1; break;
                }
            }
            for (int i = index; i < platforms.Count; i++)
            {
                if (!platforms[i].IsEmpty)
                {
                    sortList.Add(platforms[i]);
                }
                else
                    break;
            }
            SortAndRelaseCollectables(collectibleID, true);
        }
        
        index = 0;
    }
    private void SortAndRelaseCollectables(int requestedID, bool canRelase = false)
    {
        int index = 0;
        Collectable collectable = null;
        for (int i = 0; i < sortList.Count; i++)
        {
            if (sortList[i].Collectable.CollectableDataSo.id == requestedID)
            {
                index = i; break;
            }
        }
        collectable = sortList[0].Collectable;

        _platformMap[collectable.CollectableDataSo.id].Remove(sortList[0]);
        Platform previousPlatform = sortList[index];
        _platformMap[requestedID].Remove(previousPlatform);

        sortList[0].SetCollectable(sortList[index].Collectable);
        _platformMap[requestedID].Add(sortList[0]);

        if(index == 1)
        {
            sortList[index].SetCollectable(collectable);
            _platformMap[collectable.CollectableDataSo.id].Add(sortList[index]);
        }
        else
        {
            for(int i = index; i > 0; i--)
            {
                if (i == 1)
                {
                    sortList[i].SetCollectable(collectable);
                    _platformMap[collectable.CollectableDataSo.id].Add(sortList[i]);
                }
                else
                {
                    Platform currentPlatform = sortList[i];
                    int currentPlatformsCollectibleID = currentPlatform.Collectable.CollectableDataSo.id;
                    Platform prePlatform = sortList[i-1];
                    Collectable prePlatformsCollectible = prePlatform.Collectable;

                    _platformMap[currentPlatformsCollectibleID].Remove(currentPlatform);
                    _platformMap[prePlatformsCollectible.CollectableDataSo.id].Remove(prePlatform);

                    currentPlatform.SetCollectable(prePlatformsCollectible);

                    _platformMap[prePlatformsCollectible.CollectableDataSo.id].Add(currentPlatform);
                }

            }
        }
        if (canRelase)
        {
            ReleasePlatforms(_platformMap[requestedID]);           
            foreach (Platform platform in platforms)
            {
                if (!platform.IsEmpty)
                {
                    collactablesToReposition.Add(platform.Collectable);
                    platform.onPlatformEmptied?.Invoke();
                }

            }     
            if(collactablesToReposition.Count > 0)
                StartCoroutine(RepositionCollectibles(collactablesToReposition));
        }
           
        _platformMap.Clear();
        sortList.Clear();
    }
    private IEnumerator RepositionCollectibles(List<Collectable> collectables)
    {
        yield return new WaitForSeconds(repositionAnimationStandbyTime);

        for (int i = 0; i < collectables.Count; i++)
        {
            int collectableID = collectables[i].CollectableDataSo.id;
            if (platforms[i].IsEmpty)
            {
                platforms[i].SetCollectable(collectables[i]);
            }
        }
        collactablesToReposition.Clear();
    }
}