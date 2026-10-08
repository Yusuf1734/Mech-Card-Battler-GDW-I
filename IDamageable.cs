// an enemy or other target implements this contract
// so the combat controller can ask it to take damage without depending on its class.
public interface IDamageable
{
    void TakeDamage(int amount);
}
