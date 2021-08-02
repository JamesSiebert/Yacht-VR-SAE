using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MaterialChangeData", menuName = "YachtMaterialChanger", order = 1)]
public class SoMaterialChanger : ScriptableObject
{
    public string objectName = "New name";
    public List<GameObject> woodObjects;
    public List<GameObject> woodMaterials;
}

