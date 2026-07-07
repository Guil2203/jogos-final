using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private bool IsHovering = false;
    [SerializeField] private float LerpSpeed = 1;
    [SerializeField] private EventTrigger _playButton;

    private void Awake() {
        EventTrigger.Entry entry1 = new EventTrigger.Entry(); // criação de entrada para EventSystem
            entry1.eventID = EventTriggerType.PointerDown; // definição do tipo de entrada como aperto de Click
            entry1.callback.AddListener((eventData) => StartGame()); // definição de função de entrada como StartGame

        EventTrigger.Entry entry2 = new EventTrigger.Entry(); // criação de entrada para EventSystem
            entry2.eventID = EventTriggerType.PointerEnter; // definição do tipo de entrada como o mouse entrando na área
            entry2.callback.AddListener((eventData) => IsHovering = true);

        EventTrigger.Entry entry3 = new EventTrigger.Entry(); // criação de entrada para EventSystem
            entry3.eventID = EventTriggerType.PointerExit; // definição do tipo de entrada como o mouse saindo da área
            entry3.callback.AddListener((eventData) => IsHovering = false);

        EventTrigger.Entry entry4 = new EventTrigger.Entry(); // criação de entrada para EventSystem
            entry4.eventID = EventTriggerType.Deselect; // definição do tipo de entrada como solto de Click
            entry4.callback.AddListener((eventData) => IsHovering = false);

        _playButton.triggers.Add(entry1);
        _playButton.triggers.Add(entry2);
        _playButton.triggers.Add(entry3);
        _playButton.triggers.Add(entry4);
    }

    private void StartGame()
    {
        
        SceneManager.LoadScene(1);
    }

    private void Update()
    {
        if (IsHovering)
        {
            _playButton.transform.localScale = Vector3.Lerp(_playButton.transform.localScale, (new Vector3(1.5f,1.5f,1.5f)), LerpSpeed * Time.deltaTime);
        }
        else
        {
            _playButton.transform.localScale = Vector3.Lerp(_playButton.transform.localScale, (new Vector3(1,1,1)), LerpSpeed * Time.deltaTime);
        }
    }
}
