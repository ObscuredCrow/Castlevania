using UnityEngine;

public class UIFlash : MonoBehaviour
{
    public bool Interactable = true;

    public void Flash() {
        if (!Interactable) return;

        foreach (Transform child in transform)
            child.gameObject.SetActive(!child.gameObject.activeSelf);
    }

    private void Start() {
        InvokeRepeating("Flash", 0.25f, 0.25f);
    }
}
