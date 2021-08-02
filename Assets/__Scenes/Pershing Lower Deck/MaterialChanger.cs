using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    public GameObject[] woodObjects;
    public Material[] woodMaterials;
    
    public GameObject[] bedSheetObjects;
    public Material[] bedSheetMaterials;


    private void Start()
    {
        woodObjects = GameObject.FindGameObjectsWithTag("ChangeWood");
        bedSheetObjects = GameObject.FindGameObjectsWithTag("BedSheet");
    }

    // Change wood materials
    public void ChangeWood(int materialId)
    {
        ChangeMaterial(materialId, woodMaterials, woodObjects);
    }
    
    // Change bed sheet materials
    public void ChangeBedSheets(int materialId)
    {
        ChangeMaterial(materialId, bedSheetMaterials, bedSheetObjects);
    }
    
    // Change all array object's materials
    public void ChangeMaterial(int materialId, Material[] materials, GameObject[] items)
    {
        foreach (var item in items)
        {
            item.GetComponent<MeshRenderer> ().material = materials[materialId];
        }
    }
}
