using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupOption : MonoBehaviour, ILaserOption
{
    [SerializeField]
    private MeshRenderer meshRenderer;
    [SerializeField]
    private Transform circleCanvas;
    [SerializeField]
    private Image circleFill;
    [SerializeField]
    private List<GameObject> otherOptions = new();
    public GameObject mainBox;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            circleCanvas.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            circleFill.fillAmount = 0;
            circleCanvas.gameObject.SetActive(false);
        }
    }

    void OnEnable ()
    {
        circleCanvas.gameObject.SetActive(false);
        circleFill.fillAmount = 0;
    }
    void Update()
    {
        if (circleCanvas.gameObject.activeSelf)
        {
            circleFill.fillAmount += Time.deltaTime;
        }
        if (circleFill.fillAmount >= 1)
        {
            mainBox.SetActive(true);
            circleFill.fillAmount = 0;
            foreach (var option in otherOptions)
            {
                option.GetComponent<PopupOption>().mainBox.SetActive(false);
                option.SetActive(false);
            }
            gameObject.SetActive(false);
        }
    }

    //Laser Functions
    public void LaserClick()
    {
        mainBox.SetActive(true);
        circleFill.fillAmount = 0;
        foreach (var option in otherOptions)
        {
            option.GetComponent<PopupOption>().mainBox.SetActive(false);
            option.SetActive(false);
        }
        gameObject.SetActive(false);
    }

    public void LaserEnter()
    {
        meshRenderer.material.EnableKeyword("_EMISSION");
        if (mainBox.name == "in2visible")
        {
            meshRenderer.material.SetColor("_EmissionColor", new Color(1, 0.8f, 0, 0.1f));
        }
        else
        {
            meshRenderer.material.SetColor("_EmissionColor", new Color(1, 0.8f, 0, 0.25f));
        }
    }
    public void LaserExit()
    {
        meshRenderer.material.DisableKeyword("_EMISSION");
    }
}
