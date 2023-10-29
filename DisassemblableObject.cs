using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/* This class handles the functionality of a disassemblable object:
 * it spawns the parts from the list, then destroys the object itself.
 */

public class DisassemblableObject : XRBaseInteractable
{
    [SerializeField] List<GameObject> parts = new List<GameObject>();
    
    public void Disassemble()
    {
        foreach (GameObject part in parts)
        {
            float rotationX = Random.Range(-100.0f, 100.0f);
            float rotationY = Random.Range(-100.0f, 100.0f);
            float rotationZ = Random.Range(-100.0f, 100.0f);

            Instantiate(part, transform.position + new Vector3(0, 1, 0), Quaternion.Euler(rotationX, rotationY, rotationZ));
            part.GetComponent<Rigidbody>().AddExplosionForce(100.0f, transform.localPosition, 10.0f);            
        }

        Destroy(this.gameObject);
    }
}
