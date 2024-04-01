using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdvanceNo : MonoBehaviour, ILaserOption
{
    [SerializeField]
    private GameObject questionCanvas;
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LaserClick()
    {
        questionCanvas.SetActive(false);
    }

    public void LaserEnter()
    {
        image.color = new Color(1, 1, 1, 0.5f);
    }

    public void LaserExit()
    {
        image.color = new Color(1, 1, 1, 1.0f);
    }
}
