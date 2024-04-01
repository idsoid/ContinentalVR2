using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdvanceYes : MonoBehaviour, ILaserOption
{
    [SerializeField]
    private GameObject questionCanvas, cidAdvance1, cidAdvance2;
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
        AudioManager.Instance.PlayAudio("ECIDAdvanceAnswer");
        cidAdvance1.SetActive(false);
        cidAdvance2.SetActive(true);
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
