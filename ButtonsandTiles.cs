using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ButtonsandTiles : MonoBehaviour
{

    [SerializeField] private GameObject Player;

    [SerializeField] private GameObject Enemy;

    PlayerMovment _playerMovment = new PlayerMovment();




    void Start()
    {

    }


    void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            _playerMovment.setNewPos(transform.position);
        }
    }

}
