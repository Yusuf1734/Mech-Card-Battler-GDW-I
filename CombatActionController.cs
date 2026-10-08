using UnityEngine;
using UnityEngine.Events;

// validates attacks, requests damage, and updates the range preview.
public class CombatActionController : MonoBehaviour
{
    [SerializeField] private AttackRangePreview rangePreview;
    [SerializeField] private UnityEvent<string> onStatusMessage = new UnityEvent<string>();

    private WeaponAttackData selectedWeapon;
    private Vector2Int attackerCell;

    public WeaponAttackData SelectedWeapon
    {
        get { return selectedWeapon; }
    }

    // the player's movement script should call this after the player changes tiles.
    public void SetAttackerCell(Vector2Int cell)
    {
        attackerCell = cell;
        if (selectedWeapon != null && rangePreview != null)
            rangePreview.ShowPreview(attackerCell, selectedWeapon);
    }

    // connect each existing card button to this through WeaponCardClick.
    public void SelectWeapon(WeaponAttackData weapon)
    {
        if (weapon == null)
        {
            SendStatus("This card has no weapon data assigned.");
            return;
        }

        selectedWeapon = weapon;
        if (rangePreview != null)
            rangePreview.ShowPreview(attackerCell, selectedWeapon);

        SendStatus(selectedWeapon.displayName + " selected. Highlighted tiles are in range.");
    }

    // call from the group's existing tile/enemy click code.
    // pass the target's board cell and the enemy GameObject that implements IDamageable.
    public bool TryAttack(Vector2Int targetCell, GameObject target)
    {
        if (selectedWeapon == null)
        {
            SendStatus("Select a weapon card first.");
            return false;
        }

        if (!selectedWeapon.IsInRange(attackerCell, targetCell))
        {
            SendStatus("That target is outside this weapon's range.");
            return false;
        }

        IDamageable damageable = FindDamageable(target);
        if (damageable == null)
        {
            SendStatus("The clicked target does not implement IDamageable.");
            return false;
        }

        damageable.TakeDamage(selectedWeapon.damage);
        SendStatus(selectedWeapon.displayName + " dealt " + selectedWeapon.damage + " damage.");
        return true;
    }

    private static IDamageable FindDamageable(GameObject target)
    {
        if (target == null)
            return null;

        MonoBehaviour[] behaviours = target.GetComponentsInParent<MonoBehaviour>(true);
        foreach (MonoBehaviour behaviour in behaviours)
        {
            IDamageable damageable = behaviour as IDamageable;
            if (damageable != null)
                return damageable;
        }

        return null;
    }

    private void SendStatus(string message)
    {
        if (onStatusMessage != null)
            onStatusMessage.Invoke(message);
    }
}
