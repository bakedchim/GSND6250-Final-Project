using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class TriggerScript1 : MonoBehaviour
{
    public Light directionalLight;
    private float lightIntensity;
    public DialogController dialogController;

    private void Start()
    {
        lightIntensity = directionalLight.intensity - 0.8f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(DimLight());
            dialogController.SetCurrentDialogs(dialogController.magicDetected);
        } 
    }

    private IEnumerator DimLight()
    {
        // Dim the light to the lightIntensity value over 2 seconds
        while (directionalLight.intensity > lightIntensity)
        {
            directionalLight.intensity -= Time.deltaTime / 2;
            yield return null;
        }
    }
}
