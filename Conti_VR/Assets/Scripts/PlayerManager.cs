using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField]
    private Transform vrCam;
    private bool heightSet = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (vrCam.transform.localPosition != Vector3.zero && !heightSet)
        {
            float heightDiff = (1 - vrCam.transform.localPosition.y);
            transform.position = new Vector3(0, transform.position.y + heightDiff, 0);
            heightSet = true;
        }
    }

    //private void UpdateHeight()
    //{
    //    if (gameManager.GetHeight().magnitude < 0.1f)
    //    {
    //        return;
    //    }

    //    Vector3 translateVector = 0.5f * Time.deltaTime * new Vector3(0, 1, 0);
    //    if (gameManager.GetHeight().y > 0.2f)
    //    {
    //        transform.position += translateVector;
    //    }
    //    else if (gameManager.GetHeight().y < -0.2f)
    //    {
    //        transform.position -= translateVector;
    //    }
    //}
}
