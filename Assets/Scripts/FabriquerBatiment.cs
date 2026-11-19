using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Gère le placement d'un bâtiment sur la carte.
/// </summary>
/// <remarks>
/// Classe inspirée de la classe FabriqueTour des notes de cours d'Alexandre Ouellet, adaptée aux bâtiments
/// du simulateur de ville.
/// Source : https://cours-alexandre-ouellet.github.io/jeux-3d/abc/placer-tour/
/// </remarks>
public class FabriquerBatiment : MonoBehaviour
{
    private GameObject prefabBatiment;

    private GameObject batimentEnPlacement;

    private GestionnaireJeu gestionnaireJeu;

    private bool placementValide;

    [SerializeField, Tooltip("")]
    private MenuConstruction menuConstruction;

    private ValidateurPlacement validation;

    private void Awake()
    {
        prefabBatiment = null;
    }

    private void Start()
    {
        gestionnaireJeu = GestionnaireJeu.Instance;
    }

    /// <summary>
    /// Choisit le bâtiment à construire et commence son placement.
    /// </summary>
    /// <param name="prefab">Le prefab du bâtiment choisi par le joueur.</param>
    public void AssignerBatimentPrefab(GameObject prefab)
    {
        menuConstruction.DeselectionnerBouton();
        AnnulerPlacementEnCours();
        prefabBatiment = prefab;
        CommencerPlacement();
    }

    /// <summary>
    /// Instancie le bâtiment et abonne les méthodes aux actions de la souris (déplacement, clic gauche et clic droit).
    /// </summary>
    /// <remarks>
    /// Inspiré des notes de cours d'Alexandre Ouellet.
    /// Source : https://cours-alexandre-ouellet.github.io/jeux-3d/abc/placer-tour/
    /// </remarks>
    public void CommencerPlacement()
    {
        batimentEnPlacement = Instantiate(prefabBatiment);
        validation = batimentEnPlacement.GetComponent<ValidateurPlacement>();

        InputActionAsset actions = gestionnaireJeu.Controles.actions;
        actions["DeplacerSouris"].performed += SuivreSouris;
        actions["ClicGauche"].performed += PlacerBatiment;
        actions["ClicDroit"].performed += AnnulerPlacement;
    }

    /// <summary>
    /// Se désabonne des événements liés au placement du bâtiment.
    /// </summary>
    /// <remarks>
    /// Auteur : Alexandre Ouellet.
    /// Source : https://cours-alexandre-ouellet.github.io/jeux-3d/abc/placer-tour/
    /// </remarks>
    private void DesabonnerEvenements()
    {
        if (gestionnaireJeu.Controles == null)
            return;

        InputActionAsset actions = gestionnaireJeu.Controles.actions;
        actions["DeplacerSouris"].performed -= SuivreSouris;
        actions["ClicGauche"].performed -= PlacerBatiment;
        actions["ClicDroit"].performed -= AnnulerPlacement;
    }

    /// <summary>
    /// Déplace le bâtiment en cours de placement à la position de la souris sur la carte.
    /// Ne fait rien si la souris est au-dessus de l'interface.
    /// </summary>
    /// <remarks>
    /// Inspiré des notes de cours d'Alexandre Ouellet.
    /// Source : https://cours-alexandre-ouellet.github.io/jeux-3d/abc/placer-tour/
    /// </remarks>
    /// <param name="contexte">Le contexte de l'action de déplacement de la souris.</param>
    public void SuivreSouris(InputAction.CallbackContext contexte)
    {
        if (SourisSurUI())
            return;

        Vector2 positionSouris = contexte.ReadValue<Vector2>();
        Ray rayon = gestionnaireJeu.CameraPrincipale.ScreenPointToRay(positionSouris);

        if (Physics.Raycast(rayon, out RaycastHit infoRaycast, float.MaxValue, LayerMask.GetMask("Map")))
        {
            batimentEnPlacement.transform.position = infoRaycast.point;
        }
    }

    /// <summary>
    /// Place le bâtiment à la position pointée par la souris et termine le placement.
    /// </summary>
    /// <remarks>
    /// Inspiré des notes de cours d'Alexandre Ouellet.
    /// Source : https://cours-alexandre-ouellet.github.io/jeux-3d/abc/placer-tour/
    /// </remarks>
    /// <param name="contexte">Le contexte de l'action du clic gauche.</param>
    public void PlacerBatiment(InputAction.CallbackContext contexte)
    {
        if (SourisSurUI())
            return;

        if (!validation.PeutEtrePlace())
            return;

        Vector2 positionSouris = Mouse.current.position.ReadValue();
        Ray rayon = gestionnaireJeu.CameraPrincipale.ScreenPointToRay(positionSouris);

        if (Physics.Raycast(rayon, out RaycastHit infoRaycast, Mathf.Infinity, LayerMask.GetMask("Map")))
        {

            batimentEnPlacement.transform.position = infoRaycast.point;

            validation.enabled = false;
            batimentEnPlacement = null;
            menuConstruction.DeselectionnerBouton();
            DesabonnerEvenements();
        }
    }

    /// <summary>
    /// Détruit le bâtiment en cours de placement, s'il y en a un, et désélectionne le bouton du menu.
    /// </summary>
    private void AnnulerPlacementEnCours()
    {
        if (batimentEnPlacement != null)
        {
            Destroy(batimentEnPlacement);
            batimentEnPlacement = null;
            menuConstruction.DeselectionnerBouton();
        }
    }

    /// <summary>
    /// Annule le placement du bâtiment lors du clic droit et se désabonne des événements.
    /// </summary>
    /// <remarks>
    /// Inspiré des notes de cours d'Alexandre Ouellet.
    /// Source : https://cours-alexandre-ouellet.github.io/jeux-3d/abc/placer-tour/
    /// </remarks>
    /// <param name="contexte">Le contexte de l'action du clic droit.</param>
    public void AnnulerPlacement(InputAction.CallbackContext contexte)
    {
        AnnulerPlacementEnCours();
        DesabonnerEvenements();
    }

    /// <summary>
    /// Vérifie si la souris se trouve au-dessus d'un élément de l'interface.
    /// Cette fonction a été générée par IA, car les exemples trouvés en ligne étaient désuets et ne fonctionnaient pas.
    /// </summary>
    /// <remarks>
    /// Code généré par : Anthropic. (2026). Claude Sonnet 5.5 (version du 18 novembre 2026) [Modèle massif
    /// de langage]. https://claude.ai
    /// </remarks>
    /// <returns>True si la souris est au-dessus de l'interface, sinon false.</returns>
    private bool SourisSurUI()
    {
        PointerEventData donnees = new PointerEventData(EventSystem.current);
        donnees.position = Mouse.current.position.ReadValue();

        List<RaycastResult> resultats = new List<RaycastResult>();
        EventSystem.current.RaycastAll(donnees, resultats);

        return resultats.Count > 0;
    }
}
