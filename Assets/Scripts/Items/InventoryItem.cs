using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] public int bodyPart;
    [SerializeField] public int armorPiece;

    public int BodyPart
    {
        get =>  bodyPart;
        private set =>  bodyPart = value;
    }
    public int ArmorPiece
    {
        get =>  armorPiece;
        private set =>  armorPiece = value;
    }

}
