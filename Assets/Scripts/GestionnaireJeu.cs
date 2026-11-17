using UnityEngine;
using UnityEngine.InputSystem;

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