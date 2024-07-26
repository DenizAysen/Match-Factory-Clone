using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class ButtonBase : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private AudioDataSo clickedSFX;
    private void OnEnable()
    {
        button.onClick.AddListener(OnClicked);
    }
    private void OnDisable()
    {
        button.onClick.RemoveListener(OnClicked);
    }
    protected virtual void OnClicked()
    {
        SFXManager.Instance.Play(clickedSFX);
    }
}
