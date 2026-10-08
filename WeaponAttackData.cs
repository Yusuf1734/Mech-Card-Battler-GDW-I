using UnityEngine;

public enum AttackRangeShape
{
    Square,  // counts diagonal tiles as one step.
    Diamond  // counts only horizontal and vertical distance.
}

// put each weapon's damage and range in a reusable asset.
[CreateAssetMenu(fileName = "NewWeaponAttack", menuName = "Game/Weapon Attack Data")]
public class WeaponAttackData : ScriptableObject
{
    public string displayName = "Weapon";
    [Min(0)] public int damage = 10;
    [Min(1)] public int range = 1;
    public AttackRangeShape rangeShape = AttackRangeShape.Square;

    public bool IsInRange(Vector2Int origin, Vector2Int target)
    {
        int dx = Mathf.Abs(target.x - origin.x);
        int dy = Mathf.Abs(target.y - origin.y);

        // a weapon cannot attack the tile its user currently occupies.
        if (dx == 0 && dy == 0)
            return false;

        int distance = rangeShape == AttackRangeShape.Diamond
            ? dx + dy
            : Mathf.Max(dx, dy);

        return distance <= range;
    }
}
