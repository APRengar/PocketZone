using UnityEngine;

public class UICanvasControllerInput : MonoBehaviour
{
    [SerializeField] Canvas inventory;

    [Header("Output")]
    public PlayerController inputs;
    

    public void VirtualMoveInput(Vector2 virtualMoveDirection)
    {
        inputs.move = virtualMoveDirection;
        // Debug.Log(virtualMoveDirection);
    }

    public void VirtualOpenInventory()
    {
        inventory.GetComponent<Inventory>().ToggleVisibility();
    }

    public void VirtualShootInput(bool virtualShootState)
    {
        inputs.isShoot = virtualShootState;
    }

    public void ChangeTarget()
    {
        inputs.GetWeaponController().ChangeTarget();
    }
}