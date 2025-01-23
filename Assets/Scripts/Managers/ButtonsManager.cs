using UnityEngine;

public class ButtonsManager : MonoBehaviour
{
    public void StartGame()
    {
        LevelManager.Instance.StartGame();
    }
    public void QuitToMenu()
    {
        LevelManager.Instance.GoToMenu();
    }
    public void QuitGame()
    {
        LevelManager.Instance.Quit();
    }
    public void SaveGame()
    {
        Inventory.Instance.SaveGame();
        Player.Instance.SavePlayerPosition();
    }
}
