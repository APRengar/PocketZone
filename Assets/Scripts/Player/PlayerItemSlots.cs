using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemSlots : MonoBehaviour
{
    public List<SpriteRenderer> PlayerSlots;
    public List<Sprite> armorSprites;

    /*
    reminder:
    PlayerSlots[0] = head
    PlayerSlots[1] = torso
    PlayerSlots[2] = arm
    PlayerSlots[3] = hand
    PlayerSlots[4] = arm
    PlayerSlots[5] = hand
    PlayerSlots[6] = leg
    PlayerSlots[7] = leg
    */
    /*
    armorSprites[0] = head
    armorSprites[1] = torso
    armorSprites[2] = arms
    armorSprites[3] = hand
    armorSprites[4] = legs
    */

    public void EquipItem(int bodyPart, int armor)
    {
        if(bodyPart == 2 || bodyPart == 3)
        {
            PlayerSlots[bodyPart].sprite = armorSprites[armor];
            PlayerSlots[bodyPart+2].sprite = armorSprites[armor];
            return;
        }
        if(bodyPart == 6)
        {
            PlayerSlots[bodyPart].sprite = armorSprites[armor];
            PlayerSlots[bodyPart+1].sprite = armorSprites[armor];
            return;
        }

        PlayerSlots[bodyPart].sprite = armorSprites[armor];
    }
    public void UnequipItem(int bodyPart)
    {
        if (bodyPart == 2 || bodyPart == 4)
        {
            PlayerSlots[bodyPart].sprite = null;
            PlayerSlots[bodyPart+2].sprite = null;
            return;
        }
        if (bodyPart == 6)
        {
            PlayerSlots[bodyPart].sprite = null;
            PlayerSlots[bodyPart+1].sprite = null;
            return;
        }
        PlayerSlots[bodyPart].sprite = null;
        // Debug.Log("Unequiped "+ bodyPart);
    }

    public void UnequipAllItems()
    {
        foreach (SpriteRenderer armor in PlayerSlots)
        {
            armor.sprite = null;
        } 
    }

}
