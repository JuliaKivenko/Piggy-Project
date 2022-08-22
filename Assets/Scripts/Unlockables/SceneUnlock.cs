using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneUnlock : MonoBehaviour, IUnlockableElement
{
    public void Unlock()
    {
        gameObject.SetActive(false);
    }
}
