using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class ButtonInteractScene : MonoBehaviour
{
    public Material selectMaterial = null;
    public string sceneName;

    private MeshRenderer meshRenderer = null;
    private XRBaseInteractable interactable = null;
    private Material originalMaterial = null;

    [SerializeField] Text text;

    public Animator transition;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originalMaterial = meshRenderer.material;

        interactable = GetComponent<XRBaseInteractable>();
        interactable.onHoverEntered.AddListener(ChangeButtonColor);
        interactable.onHoverExited.AddListener(RevertButtonColor);

    }

    private void OnDestroy()
    {
        interactable.onHoverEntered.RemoveListener(ChangeButtonColor);
        interactable.onHoverExited.RemoveListener(RevertButtonColor);
    }

    private void ChangeButtonColor(XRBaseInteractor interactor)
    {
        meshRenderer.material = selectMaterial;
        SceneManager.LoadScene(sceneName);
    }

    private void RevertButtonColor(XRBaseInteractor interactor)
    {
        meshRenderer.material = originalMaterial;
    }


}
