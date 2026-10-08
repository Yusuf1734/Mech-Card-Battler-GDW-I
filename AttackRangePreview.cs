using UnityEngine;

// highlights reachable tiles on the group's existing board.
public class AttackRangePreview : MonoBehaviour
{
    [SerializeField] private Transform boardRoot;
    [SerializeField] private BoardAttackTile[] boardTiles;

    private void Awake()
    {
        RefreshBoardTiles();
        ClearPreview();
    }

    // call this after the board creates its tiles at runtime, if applicable.
    public void RefreshBoardTiles()
    {
        if (boardRoot != null)
            boardTiles = boardRoot.GetComponentsInChildren<BoardAttackTile>(true);
        else if (boardTiles == null || boardTiles.Length == 0)
            boardTiles = GetComponentsInChildren<BoardAttackTile>(true);
    }

    public void ShowPreview(Vector2Int attackerCell, WeaponAttackData weapon)
    {
        if (boardTiles == null || boardTiles.Length == 0)
            RefreshBoardTiles();

        if (boardTiles == null)
            return;

        foreach (BoardAttackTile tile in boardTiles)
        {
            if (tile != null)
                tile.SetRangeHighlighted(weapon != null && weapon.IsInRange(attackerCell, tile.GridPosition));
        }
    }

    public void ClearPreview()
    {
        if (boardTiles == null)
            return;

        foreach (BoardAttackTile tile in boardTiles)
        {
            if (tile != null)
                tile.SetRangeHighlighted(false);
        }
    }
}
