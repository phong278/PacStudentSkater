using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public void LoadLevel1() => SceneManager.LoadScene("MainScene");
    public void LoadLevel2() => SceneManager.LoadScene("DesignIterationScene");


}
