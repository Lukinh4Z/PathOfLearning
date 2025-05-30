using System;
using UnityEngine;
using UnityEngine.InputSystem;

// Criamos um delegate customizado para passar mais informa��es no evento de clique
// Este delegate receber� a posi��o 3D do clique e o GameObject que foi atingido.
public delegate void OnLeftClickWithHitInfo(Vector3 clickPosition, GameObject hitGameObject);
// OU para simplicidade, se voc� n�o quer criar um novo delegate:
// public event Action<Vector3, GameObject> OnLeftClickWithHitInfo;

public delegate void OnHoverInteractable(InteractableObject interactableObject);

public class MouseInput : MonoBehaviour
{
    [HideInInspector]
    public Vector3 mouseInputPosition; // Posi��o 3D no mundo

    PlayerInput_Actions _actions;
    Camera _mainCamera;

    // EVENTO 2: Novo evento para o clique esquerdo, passando a posi��o e o GameObject atingido
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
            enabled = false; // Desativa o script se n�o houver c�mera
            return;
        }

        // Assina o evento para quando a a��o 'MousePosition' � realizada
        // O '.performed' significa que a a��o foi ativada (o mouse se moveu)
        _actions.Player.MousePos.performed += OnMousePositionPerformed;

        // Se voc� quiser lidar com cliques:
         _actions.Player.Interact.performed += OnInteractPerformed;
    }

    void OnEnable()
    {
        // Habilita o Action Map 'Player' quando o objeto � habilitado
        _actions.Player.Enable();
    }

    void OnDisable()
    {
        // Desabilita o Action Map 'Player' quando o objeto � desabilitado
        _actions.Player.Disable();
        _actions.Player.MousePos.performed -= OnMousePositionPerformed;
        _actions.Player.Interact.performed -= OnInteractPerformed;
    }

    // M�todo chamado quando a a��o 'MousePosition' � realizada
    private void OnMousePositionPerformed(InputAction.CallbackContext context)
    {
        // Obt�m a posi��o 2D do mouse na tela (Vector2)
        Vector2 mouseScreenPosition = context.ReadValue<Vector2>();

        // Converte a posi��o 2D da tela para um raio no mundo 3D
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
            // Opcional: o que fazer se o raio n�o atingir nada.
            // Para UI, voc� pode querer a posi��o do mouse no espa�o da tela
            // sem um raycast. Para isso, use mouseScreenPosition diretamente.
            // Debug.Log("Mouse not hitting any collider.");
        }
    }

    public void OnInteractPerformed(InputAction.CallbackContext context)
    {
        // 1.Pega a posi��o 2D do mouse no momento exato do clique
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();

        // 2. Cria um raio a partir da c�mera atrav�s da posi��o do clique
        Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);

        RaycastHit hit; // Vari�vel para armazenar as informa��es do acerto

        // 3. Executa o Raycast
        if (Physics.Raycast(ray, out hit, float.MaxValue))
        {
            // O clique ATINGIU um GameObject!
            Debug.Log($"Left Click detected on: {hit.collider.gameObject.name} at World Position: {hit.point}");

            // 4. Dispara o evento, passando a posi��o do clique e o GameObject atingido
            OnLeftClickWithGameObject?.Invoke(hit.point, hit.collider.gameObject);
        }
        else
        {
            // O clique N�O atingiu nenhum GameObject com colisor
            Debug.Log("Left Click in empty space (no collider hit).");
            // Se voc� quiser um evento para cliques no vazio:
            // public event Action<Vector3> OnLeftClickEmptySpace;
            // OnLeftClickEmptySpace?.Invoke(mouseScreenPosition); // Ou a mouseWorldPosition atualizada, se preferir
        }
    }

    void Update()
    {
    }


}
