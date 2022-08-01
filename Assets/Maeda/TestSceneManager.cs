using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestSceneManager : MonoBehaviour
{
    public void SceneChange(int a) 
    {
        SceneManager.LoadScene(a);
    }
}
