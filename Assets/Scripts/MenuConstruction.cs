using UnityEngine;

public class MenuConstruction : MonoBehaviour
{
    [SerializeField] private GameObject panneauPrincipal;

    private GameObject panneauActif;

    private void Start()
    {
        panneauPrincipal.SetActive(true);
    }

    public void Ouvrir(GameObject sousMenu)
    {
        panneauPrincipal.SetActive(false);
        sousMenu.SetActive(true);
        panneauActif = sousMenu;
    }

    public void Retour()
    {
        if (panneauActif != null)
        {
            panneauActif.SetActive(false);
            panneauActif = null;
        }
        panneauPrincipal.SetActive(true);
    }
}
