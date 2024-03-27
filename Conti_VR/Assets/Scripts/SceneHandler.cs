using UnityEngine;
using UnityEngine.UI;
using Valve.VR.Extras;

public class SceneHandler : MonoBehaviour
{
    [SerializeField]
    private SteamVR_LaserPointer laserPointer;

    private void Awake()
    {
        laserPointer.PointerClick += PointerClick;
        laserPointer.PointerIn += PointerInside;
        laserPointer.PointerOut += PointerOutside;
    }

    //Event functions
    public void PointerClick(object sender, PointerEventArgs e)
    {
        ClickTarget(e.target);
    }
    public void PointerInside(object sender, PointerEventArgs e)
    {
        InsideTarget(e.target);
    }
    public void PointerOutside(object sender, PointerEventArgs e)
    {
        OutsideTarget(e.target);
    }

    //Target Check + Functions
    private void ClickTarget(Transform target)
    {
        switch (target.tag)
        {
            case "Button":
                target.GetComponent<Button>().onClick.Invoke();
                break;
            case "Toggle":
                target.GetComponent<Toggle>().isOn = true;
                break;
            case "Mute":
                target.GetComponent<Toggle>().isOn = !target.GetComponent<Toggle>().isOn;
                break;
            case "Option":
                target.GetComponent<ILaserOption>().LaserClick();
                break;
            default:
                break;
        }
    }
    private void InsideTarget(Transform target)
    {
        switch (target.tag)
        {
            case "Button":
                ColorBlock buttonColorBlock = target.GetComponent<Button>().colors;
                buttonColorBlock.normalColor = Color.grey;
                target.GetComponent<Button>().colors = buttonColorBlock;
                break;
            case "Toggle":
                ColorBlock toggleColorBlock = target.GetComponent<Toggle>().colors;
                toggleColorBlock.normalColor = Color.grey;
                target.GetComponent<Toggle>().colors = toggleColorBlock;
                break;
            case "Mute":
                ColorBlock muteColorBlock = target.GetComponent<Toggle>().colors;
                muteColorBlock.normalColor = Color.grey;
                target.GetComponent<Toggle>().colors = muteColorBlock;
                break;
            case "Option":
                target.GetComponent<ILaserOption>().LaserEnter();
                break;
            default:
                break;
        }
    }
    private void OutsideTarget(Transform target)
    {
        switch (target.tag)
        {
            case "Button":
                ColorBlock buttonColorBlock = target.GetComponent<Button>().colors;
                buttonColorBlock.normalColor = Color.white;
                target.GetComponent<Button>().colors = buttonColorBlock;
                break;
            case "Toggle":
                ColorBlock toggleColorBlock = target.GetComponent<Toggle>().colors;
                toggleColorBlock.normalColor = Color.white;
                target.GetComponent<Toggle>().colors = toggleColorBlock;
                break;
            case "Mute":
                ColorBlock muteColorBlock = target.GetComponent<Toggle>().colors;
                muteColorBlock.normalColor = Color.white;
                target.GetComponent<Toggle>().colors = muteColorBlock;
                break;
            case "Option":
                target.GetComponent<ILaserOption>().LaserExit();
                break;
            default:
                break;
        }
    }
}
