using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Contrôle les éléments interactifs UI du menu du bas.
/// </summary>
public class MenuConstruction : MonoBehaviour
{
    [SerializeField, Tooltip("Barre du bas principale.")]
    private GameObject panneauPrincipal;

    // La barre d'en bas active (ex: service).
    private GameObject panneauActif;

    // Le composant Outline d'un bouton de la barre active.
    private Outline outlineActif;

    private void Start()
    {
        panneauPrincipal.SetActive(true);
    }

    /// <summary>
    /// Ouvre un sous-menu et masque le panneau principal. Le sous-menu ouvert devient le panneau actif.
    /// </summary>
    /// <param name="sousMenu">Le sous-menu à afficher.</param>
    public void Ouvrir(GameObject sousMenu)
    {
        panneauPrincipal.SetActive(false);
        sousMenu.SetActive(true);
        panneauActif = sousMenu;
    }

    /// <summary>
    /// Masque le sous-menu actif et affiche le panneau principal.
    /// </summary>
    public void Retour()
    {
        if (panneauActif != null)
        {
            panneauActif.SetActive(false);
            panneauActif = null;
        }
        panneauPrincipal.SetActive(true);
    }

    /// <summary>
    /// Sélectionne un bouton en activant son contour.
    /// </summary>
    /// <param name="bouton">Le bouton à mettre en évidence.</param>
    public void SelectionnerBouton(GameObject bouton)
    {
        DeselectionnerBouton();

        outlineActif = bouton.GetComponent<Outline>();

        if (outlineActif != null)
        {
            outlineActif.enabled = true;
        }
    }

    /// <summary>
    /// Désélectionne le bouton actif en désactivant son contour, s'il y en a un.
    /// </summary>
    public void DeselectionnerBouton()
    {
        if (outlineActif != null)
        {
            outlineActif.enabled = false;
            outlineActif = null;
        }
    }
}
