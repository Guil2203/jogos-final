using UnityEngine;

[CreateAssetMenu(fileName = "NewTarget", menuName = "ScriptableObjects/New Target")]
public class TargetSO : ScriptableObject
{
    public int Score;
    public float Duration = 1f;
    public int Chance = 100;
    public Vector3 Scale = new Vector3(0.5f, 1f, 0.5f);
    public Material Material;
    public Mesh Mesh;
    public ParticleSystem DestructionParticles;
}
