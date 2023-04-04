using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisassemblableObject : MonoBehaviour
{
    [SerializeField] GameObject[] parts;
    [SerializeField] string partTag;
    
    public void Disassemble()
    {
        foreach (GameObject part in parts)
        {
            GameObject instantiatedPart = Instantiate(part, transform.localPosition, transform.localRotation);
            instantiatedPart.tag = partTag;
            Destroy(this.gameObject);
        }
    }
}
