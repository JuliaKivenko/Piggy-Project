using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionTrigger : MonoBehaviour
{
    [SerializeField] private int sceneToLoadIndex;
    [SerializeField] private string exitName;
    //public float playerExitLength = 0.1f;

    //public GameObject exitPositionGameObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerPrefs.SetString("LastExitName", exitName);
            SceneLoader.LoadLevel(sceneToLoadIndex);
            //StartCoroutine(PlayerWalkOutAnimation());
        }
    }

    /*IEnumerator PlayerWalkOutAnimation()
    {
        float timeElapsed = 0;
        Vector2 startPosition = DontDestroyPlayerOnLoad.instance.transform.position;
        while (timeElapsed < playerExitLength)
        {
            DontDestroyPlayerOnLoad.instance.transform.position = Vector2.Lerp(startPosition, exitPositionGameObject.transform.position, timeElapsed / playerExitLength);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }*/
}
