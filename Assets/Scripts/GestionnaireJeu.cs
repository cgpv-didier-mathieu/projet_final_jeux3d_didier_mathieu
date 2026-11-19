using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Pour l'instant, provient entièrement des notes de cours. Va probablement changer, sinon changer danas emprunter...
/// </summary>
public class GestionnaireJeu : MonoBehaviour
{
    public static GestionnaireJeu Instance { get; private set; }

    [field: SerializeField, Tooltip("Référence au PlayerInput pour gérer les entrées")]
    public PlayerInput Controles { get; private set; }

    [field: SerializeField, Tooltip("Référence à la caméra principale")]
    public Camera CameraPrincipale { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}