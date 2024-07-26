using System;
using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Serialized Field
    [SerializeField] private RectTransform containerParent;
    [SerializeField] LevelManagerSo levelManagerSo;
    [SerializeField] PanelAnimationSettings panelAnimationSettings;
    [SerializeField] private GameObject containerPrefab;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    #endregion

    private CollectableDataSo _collectableDataSo;
    private void Awake()
    {
        LevelDataSo currentLevel = levelManagerSo.GetCurrentLevelData();
        foreach (LevelData levelData in currentLevel.levelData)
        {
            _collectableDataSo = levelData.collectableDataSo;
            Debug.Log(_collectableDataSo.collectableName);
            GameObject container = Instantiate(containerPrefab, containerParent);
            container.GetComponent<Container>().Initialize(_collectableDataSo, levelData.count);
            _collectableDataSo.OnReleaseEvent += OnReleaseCollectable;
        }
    }
    private void OnEnable()
    {
        PlatformManager.onGameOver += OnGameOver;
    }

    private void OnGameOver() => StartCoroutine(OpenLosePanel());

    private IEnumerator OpenLosePanel()
    {
        yield return new WaitForSeconds(panelAnimationSettings.LosePanelActivationDelay);
        losePanel.SetActive(true);
    }

    private void OnReleaseCollectable() => StartCoroutine(CheckContainerCount());
    private IEnumerator CheckContainerCount()
    {
        yield return new WaitForSeconds(panelAnimationSettings.WinPanelActivationDelay);
        if (containerParent.childCount == 0)
        {
            winPanel.SetActive(true);
        }
    }
    private void OnDisable()
    {
        PlatformManager.onGameOver -= OnGameOver;
    }
}
