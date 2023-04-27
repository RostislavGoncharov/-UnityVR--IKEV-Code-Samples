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
            part.GetComponent<Rigidbody>().AddExplosionForce(100.0f, transform.localPosition, 10.0f);
            Destroy(this.gameObject);
        }
    }
}
