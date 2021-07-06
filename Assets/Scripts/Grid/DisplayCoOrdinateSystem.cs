using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//execute in editor and in play mode
[ExecuteAlways]
public class DisplayCoOrdinateSystem : MonoBehaviour
{
    //references for objects
    TextMeshPro coLabel;

    //variables used for the co-ordinates
    Vector2Int getLocation = new Vector2Int();

    // Start is called before the first frame update
    void Awake()
    {
        AwakeFunctions();
    }

    void AwakeFunctions()
    {
        coLabel = GetComponent<TextMeshPro>();
        DisplayCoOrdinates();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoOrdinates();
            DisplayOBJName();
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

    void DisplayOBJName()
    {
        transform.parent.name = getLocation.ToString();
    }
}
