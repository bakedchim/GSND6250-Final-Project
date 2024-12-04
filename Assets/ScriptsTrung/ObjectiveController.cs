using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveController : MonoBehaviour
{
    public bool hasGivenObjective1 = false;
    public bool objective1Complete = false;
    public bool hasReturnObjective1 = false;
    public bool objective2Complete = false;
    public bool hasReturnObjective2 = false;

    [SerializeField]
    private DialogController dialogController;
    
    [SerializeField]
    private PlayerMovement playerMovement;

    [SerializeField]
    private GameObject interactText;

    public void StartInteraction(GameObject interactableObject) {
        if (interactableObject.CompareTag("Rock")) {
            if (hasGivenObjective1 == false) {
                dialogController.SetCurrentDialogs(dialogController.ponderPreQuest);
            } else if (objective1Complete == false) {
                GameObject[] gos = GameObject.FindGameObjectsWithTag("Rock");
                if (gos.Length == 1) {
                    objective1Complete = true;
                }
                Destroy(interactableObject);
                playerMovement.interctableObject = null;
                interactText.SetActive(false);
            }
        } else if (interactableObject.CompareTag("Weed")) {
            if (hasGivenObjective1 == false || hasReturnObjective1 == false) {
                dialogController.SetCurrentDialogs(dialogController.ponderPreQuest);
            } else if (objective2Complete == false) {
                GameObject[] gos = GameObject.FindGameObjectsWithTag("Weed");
                if (gos.Length == 1) {
                    objective2Complete = true;
                }
                Destroy(interactableObject);
                playerMovement.interctableObject = null;
                interactText.SetActive(false);
            }
        } else if (interactableObject.CompareTag("NPC")) {
            if (hasGivenObjective1 == false) {
                dialogController.SetCurrentDialogs(dialogController.quest1Give);
                hasGivenObjective1 = true;
            } else if (objective1Complete == true && hasReturnObjective1 == false) {
                dialogController.SetCurrentDialogs(dialogController.quest2Give);
                hasReturnObjective1 = true;
            } else if (objective2Complete == true && hasReturnObjective2 == false) {
                dialogController.SetCurrentDialogs(dialogController.quest3Give);
                hasReturnObjective2 = true;
            } else {
                dialogController.SetCurrentDialogs(dialogController.ponderNPCBeforeFinalQuest);
            }
        } else if (interactableObject.CompareTag("Gate")) {
            if (!hasReturnObjective2) {
                dialogController.SetCurrentDialogs(dialogController.ponderBeforeFinalQuest);
            } else {
                Destroy(interactableObject);
                playerMovement.interctableObject = null;
                interactText.SetActive(false);
            }
        } else if (interactableObject.CompareTag("StoneSlab")) {
            dialogController.SetCurrentDialogs(dialogController.stoneSlabDetected);
        }
    }
}
