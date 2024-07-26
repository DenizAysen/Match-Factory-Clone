using TMPro;
using UnityEngine;

public class CharacterView : MonoBehaviour
{
    [SerializeField] private TMP_Text healthBarTxt;

    private CharacterModel _model;
    
    public void Initialize(CharacterModel model)
    {
        _model = model;
        healthBarTxt.text = _model.Health.ToString();
    }

    private void Update()
    {
        healthBarTxt.text = _model.Health.ToString();
    }
}
