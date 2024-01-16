using UnityEngine;

[CreateAssetMenu(fileName = "PanelsStorage", menuName = "ScriptableObjects/PanelsStorage", order = 1)]
public class PanelsStorage : ScriptableObject
{
    public PanelInStorage[] Panels;
}