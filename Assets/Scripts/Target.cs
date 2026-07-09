using System;
using UnityEngine;

public class Target : MonoBehaviour
{
    private int _score;
    private float _duration;
    private float _timer;
    public static Action<int> OnHit;

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _duration)
        {
            Destroy(gameObject);
        }
    }

    public void Hit()
    {
        OnHit?.Invoke(_score);
        Destroy(gameObject);
    }

    public void SetTarget(TargetSO targetSO)
    {
        _score = targetSO.Score;
        _duration = targetSO.Duration;
        gameObject.transform.localScale = targetSO.Scale;
        GetComponent<MeshRenderer>().materials = new Material[2]{targetSO.Material, GetComponent<MeshRenderer>().materials[1]};
        GetComponent<MeshFilter>().mesh = targetSO.Mesh;
    }
}
