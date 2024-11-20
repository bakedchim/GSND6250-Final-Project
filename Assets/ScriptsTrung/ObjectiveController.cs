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

    public void StartInteraction(GameObject interactableObject) {
        if (interactableObject.CompareTag("CowDung")) {
            if (hasGivenObjective1 == false) {
                dialogController.SetCurrentDialogs(dialogController.ponderPreQuest);
            }
        }
    }
}
