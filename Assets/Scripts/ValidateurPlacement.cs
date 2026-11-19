using UnityEngine;

public class ValidateurPlacement : MonoBehaviour
{
    private int nbContacts = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Batiment") || other.CompareTag("Route"))
            nbContacts++;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Batiment") || other.CompareTag("Route")) 
            nbContacts--;
    }

    public bool PeutEtrePlace()
    {
        return nbContacts == 0;
    }
}
