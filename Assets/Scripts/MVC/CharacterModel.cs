
public class CharacterModel
{
    public float Health
    {
        get => health;
        set => health = value;
    }

    private float health;

    public CharacterModel(float initialHealth)
    {
        health = initialHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health < 0)
            health = 0;
    }
}
