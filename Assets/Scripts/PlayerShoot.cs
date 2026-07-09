using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    private GameManager _gameManager;
    private Camera _camera;
    [SerializeField] private AudioSource shoot;
    [SerializeField] private AudioSource targetHit;
    [SerializeField] private AudioSource comboSound;
    [SerializeField] private AudioSource comboLoseSound;
    [SerializeField] private AnimationCurve _curve;
    float comboProgress = 0f;
    float progress = 0f;
    [SerializeField] public ParticleSystem shootParticles;
    [SerializeField] private float comboDuration = 3f;
    private int comboScore;
    [SerializeField] private Slider _slider;
    private bool isComboLost = true;

    void Start()
    {
        _gameManager = FindFirstObjectByType<GameManager>();
        _camera = Camera.main;
        
    }

    async void Combo()
    {
        if (comboProgress > 0)
        {
            comboProgress = 1;
            comboScore++;
            
            return;
        }
        comboScore = 1;
        comboProgress = 1;
        
        while (comboProgress > 0)
        {
            comboProgress -= Time.deltaTime / comboDuration;
            _slider.value = comboProgress;
            await Task.Yield();
        }
        comboScore = 0;
    }
    
    async void Recoil()
    {
        if (progress > 0f)
        {
            progress = 1f;
            return;
        }
        
        progress = 1f;
        
        while (progress > 0f)
        {
            _camera.fieldOfView = 60;
            _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, _camera.fieldOfView + 2f, _curve.Evaluate(progress));
            progress -= Time.deltaTime * 5;
            await Task.Yield();
        }
    }
    
    void Update()
    {
        if (comboScore == 0 && isComboLost == false)
        {
            comboLoseSound.Play();
            isComboLost = true;
        }
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            var ray = _camera.ScreenPointToRay(Mouse.current.position.value);
            if (Physics.Raycast(ray, out var raycastHit))
            {
                if (_gameManager._gameStarted)
                {
                    shoot.Play();
                    Recoil();
                    shootParticles.transform.position = raycastHit.point;
                    ParticleSystem.Instantiate(shootParticles).Play();
                    
                }
                
                if (raycastHit.collider.TryGetComponent<Target>(out var target))
                {
                    target.Hit();
                    targetHit.Play();
                    Combo();
                    isComboLost = false;
                    if (comboScore == 1)
                    {
                        comboSound.pitch = 1.0f;
                        comboSound.Play();
                        if (comboScore == 2)
                        {
                            comboSound.pitch = 1.2f;
                            comboSound.Play();
                        }
                    }
                    
                }
                else
                {
                    comboScore = 0;
                    comboProgress = 0;
                    _slider.value = 0;
                }
            }
        }
    }
}
