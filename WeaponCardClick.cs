using UnityEngine;
using UnityEngine.UI;

// connects an existing clickable weapon card to combat selection.
[RequireComponent(typeof(Button))]
public class WeaponCardClick : MonoBehaviour
{
    [SerializeField] private Button cardButton;
    [SerializeField] private WeaponAttackData weapon;
    [SerializeField] private CombatActionController combatController;

    private void Awake()
    {
        if (cardButton == null)
            cardButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        if (cardButton == null)
            cardButton = GetComponent<Button>();
        if (cardButton != null)
            cardButton.onClick.AddListener(HandleClick);
    }

    private void OnDisable()
    {
        if (cardButton != null)
            cardButton.onClick.RemoveListener(HandleClick);
    }

    private void HandleClick()
    {
        if (combatController != null)
            combatController.SelectWeapon(weapon);
        else
            Debug.LogWarning("Assign a CombatActionController to this weapon card.", this);
    }
}
