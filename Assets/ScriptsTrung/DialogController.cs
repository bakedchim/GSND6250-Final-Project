using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogController : MonoBehaviour
{
    [SerializeField]
    private Dialog[] ponder;
   
    [SerializeField]
    private GameObject dialogPanel;

    [SerializeField]
    private TMP_Text dialogText;

    private Dialog[] currentDialogs;
    private int currentDialogIndex = 0;

    [SerializeField]
    private ObjectiveController objectiveController;

    [SerializeField]
    private GameControllerTrung gameController;

    [SerializeField]
    private PlayerMovement playerController;

    public void StartInteraction(string tag)
    {
        playerController.canMove = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SetCurrentDialogs(ponder);
    }

    public void AdvanceDialog()
    {
        if (currentDialogIndex == currentDialogs.Length - 1) {
            // if (CompareDialogs(currentDialogs, greetingsAndQuestion1)) {
            //     if (objectiveController.objective1Complete) {
            //         SetCurrentDialogs(question2);
            //         objectiveController.hasReturnObjective1 = true;
            //     } else {
            //         SetCurrentDialogs(ponder);
            //     }
            // } else {
            //     EndDialog();
            // }
            // return;
        }

        currentDialogIndex++;
        SetDialog(currentDialogs[currentDialogIndex]);
    }

    private bool CompareDialogs(Dialog[] dialog1, Dialog[] dialog2)
    {
        if (dialog1.Length != dialog2.Length) {
            return false;
        }

        for (int i = 0; i < dialog1.Length; i++) {
            if (dialog1[i].text != dialog2[i].text) {
                return false;
            }
        }

        return true;
    }

    public void SetCurrentDialogs(Dialog[] dialogs)
    {
        currentDialogs = dialogs;
        currentDialogIndex = 0;
        SetDialog(currentDialogs[0]);
    }

    public void EndDialog()
    {
        playerController.canMove = true;
        dialogPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    

    public void SetDialog(Dialog dialog)
    {
        dialogText.text = dialog.text;

        if (dialog.isSelfDialogue) {
            // italicize the text
            dialogText.fontStyle = FontStyles.Italic;
        } else {
            // remove italicize
            dialogText.fontStyle = FontStyles.Normal;
        }

        dialogText.alignment = dialog.isLeft ? TextAlignmentOptions.Left : TextAlignmentOptions.Right;
        
        dialogPanel.SetActive(true);
    }
}
