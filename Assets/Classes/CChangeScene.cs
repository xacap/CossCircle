using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CChangeScene : MonoBehaviour
{
    public void ManageScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

}
