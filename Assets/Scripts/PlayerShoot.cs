using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    private GameManager _gameManager;
    private Camera _camera;

    void Start()
    {
        _gameManager = FindFirstObjectByType<GameManager>();
        _camera = Camera.main;
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            var ray = _camera.ScreenPointToRay(Mouse.current.position.value);
            if (Physics.Raycast(ray, out var raycastHit))
            {
                if (raycastHit.collider.TryGetComponent<Target>(out var target))
                {
                    target.Hit();
                }
            }
        }
    }
}
