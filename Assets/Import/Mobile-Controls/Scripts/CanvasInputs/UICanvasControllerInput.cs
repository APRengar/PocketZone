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

    public void VirtualShootInput()
    {
        inputs.isShoot = !inputs.isShoot;
    }

    public void ChangeTarget()
    {
        inputs.GetWeaponController().ChangeTarget();
    }
}