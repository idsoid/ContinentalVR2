using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupOption : MonoBehaviour, ILaserOption
{
    [SerializeField]
    private ParticleSystem laserParticle;
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
        laserParticle.Play();
    }
    public void LaserExit()
    {
        laserParticle.Stop();
    }
}
