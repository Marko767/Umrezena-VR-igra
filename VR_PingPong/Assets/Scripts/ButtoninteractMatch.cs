using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtoninteractMatch : MonoBehaviour
{
    public Material selectMaterial = null;
    public string sceneName;

    private MeshRenderer meshRenderer = null;
    private XRBaseInteractable interactable = null;
    private Material originalMaterial = null;

    [SerializeField] Text text;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originalMaterial = meshRenderer.material;

        interactable = GetComponent<XRBaseInteractable>();
        interactable.onHoverEntered.AddListener(ChangeButtonColor);
        interactable.onHoverExited.AddListener(RevertButtonColor);

        text.text = "Match not in progress";
    }

    private void OnDestroy()
    {
        interactable.onHoverEntered.RemoveListener(ChangeButtonColor);
        interactable.onHoverExited.RemoveListener(RevertButtonColor);
    }

    private void ChangeButtonColor(XRBaseInteractor interactor)
    {
        meshRenderer.material = selectMaterial;
        StartCoroutine(StartMatch());
    }

    private void RevertButtonColor(XRBaseInteractor interactor)
    {
        meshRenderer.material = originalMaterial;
    }

    IEnumerator StartMatch()
    {
        text.text = "Match in progress";
        Ball2.matchInProgress = true;
        yield return new WaitForSeconds(0.5f);
    }
}
