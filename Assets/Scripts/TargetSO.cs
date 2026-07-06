using UnityEngine;

[CreateAssetMenu(fileName = "NewTarget", menuName = "ScriptableObjects/New Target")]
public class TargetSO : ScriptableObject
{
    public int Score;
    public float Duration = 1f;
    
}
