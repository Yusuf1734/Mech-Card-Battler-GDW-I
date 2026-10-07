using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;



/* stuff you need to do:
take player x and y
for loop in each direction (N E S W, NE SE NW NS)
 check if player coords are valid in 1 unit in that direction
 keep looping to check for valid cords until they are blocked or out of movement
 place buttons on valid cords via instantiate
Movment should be handled with left mouse button
Use unity coordinate system
*/
public class PlayerMovment : MonoBehaviour
{
    //All the variables to set up the grids on the board and the steps the player can move
    [SerializeField] private int gridX = 20;
    [SerializeField] private int gridY = 20;
    [SerializeField] private float tile = 1f;
    [SerializeField] private Vector2 playerPos = new Vector2(10, 10);

    [SerializeField] private Vector2 newPos = new Vector2(0, 0);

    [SerializeField] private int stepsPlayerCanTake = 3;

    //TilePrefab is what I assume the actual tile will be called change it to whatever
    public GameObject buttonTilePrefab;
    public Transform buttonParent;

    public void setNewPos(Vector2 pos)
    {
        newPos = pos;
    }
    
    private Vector2[] directions = new Vector2[]
    {
        //Cardinal directions for movment
        new Vector2(0, 1), //N
        new Vector2(1, 0), //E
        new Vector2(0, -1), //S
        new Vector2(-1, 0), //W
        new Vector2(1, 1), //NE
        new Vector2(1, -1), //SE
        new Vector2(-1, 1), //NW
        new Vector2(-1, -1), //SW
    };

    private List<GameObject> buttons = new List<GameObject>();
    private List<Vector2> validPos = new List<Vector2>();

    private void MovePlayer()
    {
        //Something needs to go here with button spawning

        foreach (Vector2 direc in directions)
        {
            for (int i = 1; i <= stepsPlayerCanTake; i++)
            {
                Vector2 checkPosition = playerPos + (direc * i);
                //Checks position for player movement
                if (checkPosition.x >= 0 && checkPosition.x < gridX && checkPosition.y >= 0 && checkPosition.y < gridY)
                {
                    Vector3 generalPos = new Vector3(checkPosition.x * tile, checkPosition.y * tile, 0);
                    //Using Instantiate to create a new button for each tile
                    GameObject nButton = Instantiate(buttonTilePrefab, generalPos, Quaternion.identity, buttonParent);
                    buttons.Add(nButton);
                    nButton.name = "MoveButton" + i;
                }
                else
                {
                    break;
                    //Because we dont want to look for any more of the tile buttons
                }
            }
        }
    }




    void Start()
    {
        //0 because it is a 2d plain
        transform.position = new Vector3(playerPos.x * tile, playerPos.y * tile, 0);

        MovePlayer();



    }

    // Update is called once per frame
    void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
        }



    }

}

