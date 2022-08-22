using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }
}
