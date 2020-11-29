using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameManager gameManager;

    public void OnPauseGame()
    {
        gameManager.PauseGame();
    }
}
