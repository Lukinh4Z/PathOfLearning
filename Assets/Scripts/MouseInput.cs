using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseInput : MonoBehaviour
{
    [HideInInspector]
    public Vector3 mouseInputPosition; // Posi��o 3D no mundo
    public Action<Vector3> OnInteract;

    PlayerInput_Actions _actions;
    Camera _mainCamera;

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
            //Debug.Log($"Mouse World Position: {mouseInputPosition}");
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
        OnInteract?.Invoke(mouseInputPosition);
        Debug.Log($"Mouse clicked on: {mouseInputPosition}");
    }

    void Update()
    {
    }


}
