using System.Collections.Generic;
using UnityEngine;

public class DisassemblableObject : MonoBehaviour
{
    [SerializeField] List<GameObject> parts = new List<GameObject>();
    
    public void Disassemble()
    {
        foreach (GameObject part in parts)
        {
            Instantiate(part, transform.localPosition, transform.localRotation);
            Destroy(this.gameObject);
        }
    }
}
