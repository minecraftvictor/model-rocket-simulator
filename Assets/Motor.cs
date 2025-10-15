using UnityEngine;

[CreateAssetMenu(fileName = "Motor", menuName = "Scriptable Objects/Motor")]
public class Motor : ScriptableObject
{
    public string motorName;
    public int minumumMaxrange; 
    public int maxrange;
}
