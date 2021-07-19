using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//execute in editor and in play mode
[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class DisplayCoOrdinateSystem : MonoBehaviour
{

    //variables used for the co-ordinates
    [Header("Text Color Settings")]
    [SerializeField] Color defaultColor = Color.black;
    [SerializeField] Color blockedColor = Color.red;
    [SerializeField] Color exploredColor = Color.blue;
    [SerializeField] Color pathColor = Color.white;

    private Vector2Int getLocation = new Vector2Int();

    //references for objects
    private TextMeshPro coLabel;
    private Gridmanager gridmanager;

    //states of the class
    [SerializeField] private bool canShow = false;

    // Start is called before the first frame update
    void Awake()
    {
        AwakeFunctions();
    }

    //Awake Function functionality
    void AwakeFunctions()
    {
        coLabel = GetComponent<TextMeshPro>();
        gridmanager = FindObjectOfType<Gridmanager>();
        DisplayCoOrdinates();
        if (Application.isPlaying)
        {
            coLabel.enabled = canShow;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfCoordinatesCanShow();
        ChangeColorForLabel();
    }

    //Sees if the co-ordiantes can show
    private void CheckIfCoordinatesCanShow()
    {
        if (!Application.isPlaying)
        {
            DisplayCoOrdinates();
            DisplayOBJName();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                canShow = !canShow;
                CanShowInPlayMode();            
            }
        }
    }

    //display the co-ordinate system on the tiles
    void DisplayCoOrdinates()
    {
        //get the 2d vector to two display it, mnote, 3d uses the z position as the y position
        getLocation.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        getLocation.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);

        coLabel.text = getLocation.x + "," + getLocation.y;
    }

    //display the name of the object
    void DisplayOBJName()
    {
        transform.parent.name = getLocation.ToString();
    }

    //enables or disable the co-ordinates during playtime
    private void CanShowInPlayMode()
    {
        if (canShow)
        {
            coLabel.enabled = true;
            DisplayCoOrdinates();
            DisplayOBJName();
        }
        else
        {
            coLabel.enabled = false;
        }
    }

    //chamges the color of the text if the object can not be used
    private void ChangeColorForLabel()
    {
        if (Application.isPlaying)
        {
            if (gridmanager == null)
            {
                return;
            }

            CheckToSeeWhatColor();
        }
    }

    //see what the color of the text will be
    private void CheckToSeeWhatColor()
    {
        Node node = gridmanager.GetNode(getLocation);

        if(node == null) { return; }

        if (!node.isWalkable)
        {
            SetColor(blockedColor);
        }
        else if (node.isPath)
        {
            SetColor(pathColor);
        }
        else if (node.isExplored)
        {
            SetColor(exploredColor);
        }
        else
        {
            SetColor(defaultColor);
        }
    }

    //changes the color of the text
    private void SetColor(Color color)
    {
        coLabel.color = color;
    }
}
