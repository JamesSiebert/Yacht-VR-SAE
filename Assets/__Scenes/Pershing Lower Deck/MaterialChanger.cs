using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    public GameObject[] woodObjects;
    public Material[] woodMaterials;
    public int woodCurrentId;
    
    public GameObject[] bedSheetObjects;
    public Material[] bedSheetMaterials;
    public int bedSheetCurrentId;

    private void Start()
    {
        woodObjects = GameObject.FindGameObjectsWithTag("ChangeWood");
        bedSheetObjects = GameObject.FindGameObjectsWithTag("BedSheet");
    }

    
    // Change wood materials
    public void ChangeWood(int materialId)
    {
        woodCurrentId = materialId;
        ChangeMaterial(materialId, woodMaterials, woodObjects);
    }
    
    public void ChangeWoodNext()
    {
        woodCurrentId += 1;
        if (woodCurrentId > woodMaterials.Length - 1)
            woodCurrentId = 0;
        ChangeMaterial(woodCurrentId, woodMaterials, woodObjects);
    }
    
    
    // Change bed sheet materials
    public void ChangeBedSheets(int materialId)
    {
        bedSheetCurrentId = materialId;
        ChangeMaterial(materialId, bedSheetMaterials, bedSheetObjects);
    }
    
    public void ChangeBedSheetNext()
    {
        bedSheetCurrentId += 1;
        if (bedSheetCurrentId > bedSheetMaterials.Length - 1)
            bedSheetCurrentId = 0;
        ChangeMaterial(bedSheetCurrentId, bedSheetMaterials, bedSheetObjects);
    }
    
    
    // Change all array object's materials
    public void ChangeMaterial(int materialId, Material[] materials, GameObject[] items)
    {
        Debug.Log("Material Change");
        foreach (var item in items)
        {
            item.GetComponent<MeshRenderer> ().material = materials[materialId];
        }
    }
}
