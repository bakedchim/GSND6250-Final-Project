using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerColliderTrigger : MonoBehaviour
{

    [SerializeField]
    private PlayerMovement playerMovement;

    [SerializeField]
    private TMP_Text interactText;

    // private void Update() {
    //     Debug.Log(playerMovement.interctableObjectTag);
    // }

    private void OnTriggerEnter(Collider other) {
        // Check the layer of the object that the player is colliding with
        if (other.gameObject.layer == LayerMask.NameToLayer("Interactable")) {
            playerMovement.interctableObjectTag = other.tag;
            interactText.gameObject.SetActive(true);
        }   
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Interactable")) {
            playerMovement.interctableObjectTag = other.tag;
            interactText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Interactable")) {
            playerMovement.interctableObjectTag = "";
            interactText.gameObject.SetActive(false);
        }
    }
}
