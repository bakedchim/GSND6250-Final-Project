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
    public bool objective3Complete = false;
    public bool hasReturnObjective3 = false;

    [SerializeField]
    private DialogController dialogController;
    
    [SerializeField]
    private PlayerMovement playerMovement;

    [SerializeField]
    private GameObject interactText;

    public void StartInteraction(GameObject interactableObject) {
        if (interactableObject.CompareTag("CowDung")) {
            if (hasGivenObjective1 == false) {
                dialogController.SetCurrentDialogs(dialogController.ponderPreQuest);
            } else if (objective1Complete == false) {
                GameObject[] gos = GameObject.FindGameObjectsWithTag("CowDung");
                if (gos.Length == 1) {
                    objective1Complete = true;
                }
                Destroy(interactableObject);
                playerMovement.interctableObject = null;
                interactText.SetActive(false);
            }
        } else if (interactableObject.CompareTag("NPC")) {
            if (hasGivenObjective1 == false) {
                dialogController.SetCurrentDialogs(dialogController.quest1Give);
                hasGivenObjective1 = true;
            }
        }

    }
}
