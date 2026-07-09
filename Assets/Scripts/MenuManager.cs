using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private int SceneToLoad = 1;
    private bool Pressed = false;
    private bool IsHovering = false;
    private bool Released = false;
    [SerializeField] private int StartDelay = 500;
    [SerializeField] private float LerpSpeed = 1;
    [SerializeField] private EventTrigger _playButton;
    [SerializeField] private UnityEvent _gameStarted;
    
    [Header("Audios")]
    [SerializeField] private AudioSource audioHoverIn;
    [SerializeField] private AudioSource audioHoverOut;
    [SerializeField] private AudioSource audioPointerDown;
    [SerializeField] private AudioSource audioPointerUp;
    
    

    private void Awake() {
        EventTrigger.Entry entry1 = new EventTrigger.Entry(); // criação de entrada para EventSystem
            entry1.eventID = EventTriggerType.PointerDown; // definição do tipo de entrada como aperto de Click
            entry1.callback.AddListener((eventData) => Pressed = true); // definição de função de entrada como StartGame
            entry1.callback.AddListener((eventData) => audioPointerDown.Play());

        EventTrigger.Entry entry2 = new EventTrigger.Entry(); // criação de entrada para EventSystem
            entry2.eventID = EventTriggerType.PointerEnter; // definição do tipo de entrada como o mouse entrando na área
            entry2.callback.AddListener((eventData) => IsHovering = true);
            entry2.callback.AddListener((eventData) => audioHoverIn.Play());

        EventTrigger.Entry entry3 = new EventTrigger.Entry(); // criação de entrada para EventSystem
            entry3.eventID = EventTriggerType.PointerExit; // definição do tipo de entrada como o mouse saindo da área
            entry3.callback.AddListener((eventData) => IsHovering = false);

        EventTrigger.Entry entry4 = new EventTrigger.Entry(); // criação de entrada para EventSystem
            entry4.eventID = EventTriggerType.Deselect; // definição do tipo de entrada como solto de Click
            entry4.callback.AddListener((eventData) => IsHovering = false);

        EventTrigger.Entry entry5 = new EventTrigger.Entry(); // criação de entrada para EventSystem
            entry5.eventID = EventTriggerType.PointerUp; // definição do tipo de entrada como solto de Click
            entry5.callback.AddListener((eventData) => Released =  true);
            entry5.callback.AddListener((eventData) => StartGame());
        
        _playButton.triggers.Add(entry1);
        _playButton.triggers.Add(entry2);
        _playButton.triggers.Add(entry3);
        _playButton.triggers.Add(entry4);
        _playButton.triggers.Add(entry5);
    }

    private async void StartGame()
    {
        await Task.Delay(StartDelay);
        _gameStarted.Invoke();
        if (SceneToLoad < 0)
        {
            
            return;
        }
        SceneManager.LoadScene(SceneToLoad);
    }

    private void Update()
    {
        if (Released)
        {
            _playButton.transform.localScale = Vector3.Lerp(_playButton.transform.localScale, (new Vector3(1,1,1)), (LerpSpeed * 4) * Time.deltaTime);
            return;
        }
        
        if (Pressed)
        {
            _playButton.transform.localScale = Vector3.Lerp(_playButton.transform.localScale, (new Vector3(0.9f, 0.9f, 0.9f)), (LerpSpeed * 4) * Time.deltaTime);
            return;
        }
        
        if (IsHovering)
        {
            _playButton.transform.localScale = Vector3.Lerp(_playButton.transform.localScale, (new Vector3(1.1f,1.1f,1.1f)), LerpSpeed * Time.deltaTime);
        }
        else
        {
            _playButton.transform.localScale = Vector3.Lerp(_playButton.transform.localScale, (new Vector3(1,1,1)), LerpSpeed * Time.deltaTime);
        }
    }
}
