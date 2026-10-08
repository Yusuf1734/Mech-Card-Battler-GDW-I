using UnityEngine;
using UnityEngine.UI;

// attach this to each existing board tile.
// set GridPosition and assign either TileGraphic (UI board) or TileSprite (world-space board).
public class BoardAttackTile : MonoBehaviour
{
    [SerializeField] private Vector2Int gridPosition;
    [SerializeField] private Graphic tileGraphic;
    [SerializeField] private SpriteRenderer tileSprite;
    [SerializeField] private Color attackRangeColor = new Color(1f, 0.75f, 0.12f, 1f);

    private Color originalGraphicColor;
    private Color originalSpriteColor;
    private bool hasOriginalColor;
    private bool isHighlighted;

    public Vector2Int GridPosition
    {
        get { return gridPosition; }
        set { gridPosition = value; }
    }

    private void Awake()
    {
        if (tileGraphic == null)
            tileGraphic = GetComponent<Graphic>();
        if (tileSprite == null)
            tileSprite = GetComponent<SpriteRenderer>();
    }

    public void SetRangeHighlighted(bool highlighted)
    {
        if (highlighted && !isHighlighted)
            SaveOriginalColors();

        if (!highlighted && isHighlighted && hasOriginalColor)
        {
            if (tileGraphic != null)
                tileGraphic.color = originalGraphicColor;
            if (tileSprite != null)
                tileSprite.color = originalSpriteColor;
        }
        else if (highlighted)
        {
            if (tileGraphic != null)
                tileGraphic.color = attackRangeColor;
            if (tileSprite != null)
                tileSprite.color = attackRangeColor;
        }

        isHighlighted = highlighted;
    }

    private void SaveOriginalColors()
    {
        if (tileGraphic != null)
            originalGraphicColor = tileGraphic.color;
        if (tileSprite != null)
            originalSpriteColor = tileSprite.color;
        hasOriginalColor = tileGraphic != null || tileSprite != null;
    }
}
