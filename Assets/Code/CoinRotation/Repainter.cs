using System.Collections.Generic;
using UnityEngine;

public class Repainter : MonoBehaviour
{
    public List<Material> materials;

    private int currentMaterial = 0;

    private void OnMouseDown()
    {
        Repaint();
    }

    public void Repaint()
    {
        foreach (Transform C in transform)
        {
            C.GetComponent<MeshRenderer>().material = materials[currentMaterial];
        }

        if (currentMaterial >= materials.Count - 1)
            currentMaterial = 0;
        else currentMaterial++;
    }

    
}
