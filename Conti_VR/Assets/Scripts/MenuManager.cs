using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private Transform player, playerCam;
    private bool heightSet = false;

    [SerializeField]
    private ToggleGroup languageChoice, clusterChoice, cidChoice;

    [SerializeField]
    private Image logoSprite, logoBackground;
    private float logoTimer = 6.0f;

    void Update() 
    {
        //Reset player direction to face menu selection
        if (playerCam.transform.localPosition != Vector3.zero && !heightSet)
        {
            float offsetAngle = playerCam.rotation.eulerAngles.y;
            player.Rotate(0f, -offsetAngle, 0f);
            heightSet = true;
        }

        //Fade in logo once it has been set active
        //Changes the image's alpha value from 0f to 1f overtime
        if (logoTimer > 0 && logoSprite.IsActive())
        {
            logoTimer -= Time.deltaTime;

            if (logoSprite.color.a < 1.0f && logoTimer <= 5.5f)
            {
                Color color = logoSprite.color;
                color.a += Time.deltaTime;
                logoSprite.color = color;
            }
        }
        else if (logoTimer <= 0)
        {
            SceneManager.LoadScene("CarScene");
        }
    }

    //Menu Functions
    public void SaveChoices()
    {
        foreach (var toggle in languageChoice.ActiveToggles())
        {
            if (toggle.isOn)
            {
                PlayerPrefsManager.Save("Language", toggle.name);
                break;
            }
        }

        foreach (var toggle in clusterChoice.ActiveToggles())
        {
            if (toggle.isOn)
            {
                PlayerPrefsManager.Save("Cluster", toggle.name);
                break;
            }
        }

        foreach (var toggle in cidChoice.ActiveToggles())
        {
            if (toggle.isOn)
            {
                PlayerPrefsManager.Save("CID", toggle.name);
                break;
            }
        }
    }
    public void LogoLoad()
    {
        logoBackground.gameObject.SetActive(true);
        logoSprite.gameObject.SetActive(true);
    }
}
