using System.Numerics;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    private GameManager _gameManager;
    private Camera _camera;
    [SerializeField] private AudioSource shoot;
    [SerializeField] private AudioSource targetHit;
    [SerializeField] private AnimationCurve _curve;
    float progress = 0f;

    void Start()
    {
        _gameManager = FindFirstObjectByType<GameManager>();
        _camera = Camera.main;
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
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            var ray = _camera.ScreenPointToRay(Mouse.current.position.value);
            if (Physics.Raycast(ray, out var raycastHit))
            {
                shoot.Play();
                Recoil();
                if (raycastHit.collider.TryGetComponent<Target>(out var target))
                {
                    target.Hit();
                    targetHit.Play();
                }
            }
        }
    }
}
