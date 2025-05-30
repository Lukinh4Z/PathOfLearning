using System;
using UnityEngine;
using UnityEngine.InputSystem;

// Criamos um delegate customizado para passar mais informações no evento de clique
// Este delegate receberá a posição 3D do clique e o GameObject que foi atingido.
public delegate void OnLeftClickWithHitInfo(Vector3 clickPosition, GameObject hitGameObject);
// OU para simplicidade, se você não quer criar um novo delegate:
// public event Action<Vector3, GameObject> OnLeftClickWithHitInfo;

public delegate void OnHoverInteractable(InteractableObject interactableObject);

public class MouseInput : MonoBehaviour
{
    [HideInInspector]
    public Vector3 mouseInputPosition; // Posição 3D no mundo

    PlayerInput_Actions _actions;
    Camera _mainCamera;

    // EVENTO 2: Novo evento para o clique esquerdo, passando a posição e o GameObject atingido
    // public event Action<Vector3, GameObject> OnLeftClickWithGameObject; // Usando Action<T1, T2>
    public event OnLeftClickWithHitInfo OnLeftClickWithGameObject; // Usando delegate customizado
    public event OnHoverInteractable OnHoverInteractable;

    private void Awake()
    {
        _actions = new PlayerInput_Actions();
        _mainCamera = Camera.main;

        if(_mainCamera == null)
        {
            Debug.LogError("Main Camera not found! Please ensure your camera has the 'MainCamera' tag.");
            enabled = false; // Desativa o script se não houver câmera
            return;
        }

        // Assina o evento para quando a ação 'MousePosition' é realizada
        // O '.performed' significa que a ação foi ativada (o mouse se moveu)
        _actions.Player.MousePos.performed += OnMousePositionPerformed;

        // Se você quiser lidar com cliques:
         _actions.Player.Interact.performed += OnInteractPerformed;
    }

    void OnEnable()
    {
        // Habilita o Action Map 'Player' quando o objeto é habilitado
        _actions.Player.Enable();
    }

    void OnDisable()
    {
        // Desabilita o Action Map 'Player' quando o objeto é desabilitado
        _actions.Player.Disable();
        _actions.Player.MousePos.performed -= OnMousePositionPerformed;
        _actions.Player.Interact.performed -= OnInteractPerformed;
    }

    // Método chamado quando a ação 'MousePosition' é realizada
    private void OnMousePositionPerformed(InputAction.CallbackContext context)
    {
        // Obtém a posição 2D do mouse na tela (Vector2)
        Vector2 mouseScreenPosition = context.ReadValue<Vector2>();

        // Converte a posição 2D da tela para um raio no mundo 3D
        Ray ray = _mainCamera.ScreenPointToRay(mouseScreenPosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue))
        {
            mouseInputPosition = hit.point;

            InteractableObject interactable = hit.transform.GetComponent<InteractableObject>();
            OnHoverInteractable?.Invoke(interactable);
        }
        else
        {
            // Opcional: o que fazer se o raio não atingir nada.
            // Para UI, você pode querer a posição do mouse no espaço da tela
            // sem um raycast. Para isso, use mouseScreenPosition diretamente.
            // Debug.Log("Mouse not hitting any collider.");
        }
    }

    public void OnInteractPerformed(InputAction.CallbackContext context)
    {
        // 1.Pega a posição 2D do mouse no momento exato do clique
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();

        // 2. Cria um raio a partir da câmera através da posição do clique
        Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);

        RaycastHit hit; // Variável para armazenar as informações do acerto

        // 3. Executa o Raycast
        if (Physics.Raycast(ray, out hit, float.MaxValue))
        {
            // O clique ATINGIU um GameObject!
            Debug.Log($"Left Click detected on: {hit.collider.gameObject.name} at World Position: {hit.point}");

            // 4. Dispara o evento, passando a posição do clique e o GameObject atingido
            OnLeftClickWithGameObject?.Invoke(hit.point, hit.collider.gameObject);
        }
        else
        {
            // O clique NÃO atingiu nenhum GameObject com colisor
            Debug.Log("Left Click in empty space (no collider hit).");
            // Se você quiser um evento para cliques no vazio:
            // public event Action<Vector3> OnLeftClickEmptySpace;
            // OnLeftClickEmptySpace?.Invoke(mouseScreenPosition); // Ou a mouseWorldPosition atualizada, se preferir
        }
    }

    void Update()
    {
    }


}
