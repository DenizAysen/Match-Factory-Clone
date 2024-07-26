using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private CharacterModel _model;
    private CharacterView _view;

    public void Initialize(CharacterModel model, CharacterView view)
    {
        _model = model;
        _view = view;
    }

    public void Attack(float damage)
    {
        _model.TakeDamage(damage);
    }
}