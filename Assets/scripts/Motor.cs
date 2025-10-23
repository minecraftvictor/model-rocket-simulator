using UnityEngine;

[CreateAssetMenu(fileName = "Motor", menuName = "Scriptable Objects/Motor")]
public class Motor : ScriptableObject
{
    public string motorName;
    public float minumumForce; 
    public float maxForce;
}
