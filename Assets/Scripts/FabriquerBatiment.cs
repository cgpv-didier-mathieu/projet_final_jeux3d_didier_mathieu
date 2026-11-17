using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// Provient des notes de cours
/// Pas oublier de déplacer dans emprunt
/// </summary>
public class FabriquerBatiment : MonoBehaviour
{
    [SerializeField, Tooltip("Le prefab du batiment à instancier")]
    private GameObject prefabBatiment;

    private GameObject batimentEnPlacement;

    private GestionnaireJeu gestionnaireJeu;

    private void Start()
    {
        gestionnaireJeu = GestionnaireJeu.Instance;
    }


    public void CommencerPlacement()
    {
        batimentEnPlacement = Instantiate(prefabBatiment);

        InputActionAsset actions = gestionnaireJeu.Controles.actions;
        actions["DeplacerSouris"].performed += SuivreSouris;
        actions["ClicGauche"].performed += PlacerBatiment;
        actions["ClicDroit"].performed += AnnulerPlacement;
    }

    private void DesabonnerEvenements()
    {
        if (gestionnaireJeu.Controles == null)
            return;

        InputActionAsset actions = gestionnaireJeu.Controles.actions;
        actions["DeplacerSouris"].performed -= SuivreSouris;
        actions["ClicGauche"].performed -= PlacerBatiment;
        actions["ClicDroit"].performed -= AnnulerPlacement;
    }

    public void SuivreSouris(InputAction.CallbackContext contexte)
    {
        Vector2 positionSouris = contexte.ReadValue<Vector2>();
        Ray rayon = gestionnaireJeu.CameraPrincipale.ScreenPointToRay(positionSouris);

        if (Physics.Raycast(rayon, out RaycastHit infoRaycast, float.MaxValue, LayerMask.GetMask("Map")))
        {
            batimentEnPlacement.transform.position = infoRaycast.point;
        }
    }

    public void PlacerBatiment(InputAction.CallbackContext contexte)
    {
        Vector2 positionSouris = Mouse.current.position.ReadValue();
        Ray rayon = gestionnaireJeu.CameraPrincipale.ScreenPointToRay(positionSouris);

        if (Physics.Raycast(rayon, out RaycastHit infoRaycast, Mathf.Infinity, LayerMask.GetMask("Map")))
        {

            GameObject zone = infoRaycast.collider.gameObject;

            batimentEnPlacement.transform.position = zone.transform.position;

            batimentEnPlacement = null;

            DesabonnerEvenements();
        }
    }

    public void AnnulerPlacement(InputAction.CallbackContext contexte)
    {
        if (batimentEnPlacement != null)
        {
            Destroy(batimentEnPlacement);
            batimentEnPlacement = null;
        }

        DesabonnerEvenements();
    }
}
