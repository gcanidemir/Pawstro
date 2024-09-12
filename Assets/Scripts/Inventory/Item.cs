using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    [Header("Only gameplay")]
    public int id;
    public TileBase tile;
    public ItemType itemType;
    public ActionType actionType;
    public Vector2Int range = new Vector2Int(5,4);

    [Header("Only UI")]
    public bool stackable = true;

    [Header("Both")]
    public Sprite image;



    // Start is called before the first frame update
    public enum ItemType {
        Tool,
        Ore
    }

    public enum ActionType{
       Dig,
       Mine 
    }
}
