using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//execute in editor and in play mode
[ExecuteAlways]
public class DisplayCoOrdinateSystem : MonoBehaviour
{

    //variables used for the co-ordinates
    [Header("Text Color Settings")]
    [SerializeField] Color defaultColor = Color.black;
    [SerializeField] Color blockedColor = Color.red;
    private Vector2Int getLocation = new Vector2Int();

    //references for objects
    private TextMeshPro coLabel;
    private Waypoint waypoint;

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
        waypoint = GetComponentInParent<Waypoint>();
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
        ChangeTextColorNonPlaceableTile();
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
    private void ChangeTextColorNonPlaceableTile()
    {
        if (Application.isPlaying)
        {
            bool isPlaceable = waypoint.CanPlace;

            if (isPlaceable)
            {
                coLabel.color = defaultColor;
            }
            else
            {
                coLabel.color = blockedColor;
            }
        }
    }
}
