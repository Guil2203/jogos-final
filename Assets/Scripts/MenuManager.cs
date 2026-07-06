using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button _playButton;

    private void Awake() {
        _playButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
