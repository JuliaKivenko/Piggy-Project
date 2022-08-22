using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEntrance : MonoBehaviour
{
    [SerializeField] string lastExitName;
    [SerializeField] Transform startPos;

    //public float playerEntranceLength = 0.1f;

    private void Start()
    {
        if (PlayerPrefs.GetString("LastExitName") == lastExitName)
        {
            PlayerController.instance.transform.position = startPos.position;
            //StartCoroutine(PlayerWalkInAnimation());
        }
    }

    /*IEnumerator PlayerWalkInAnimation()
    {
        float timeElapsed = 0;
        while (timeElapsed < playerEntranceLength)
        {
            DontDestroyPlayerOnLoad.instance.transform.position = Vector2.Lerp(startPositionGameObject.transform.position, transform.position, timeElapsed / playerEntranceLength);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        PlayerMovement.instance.transform.position = transform.position;
        PlayerMovement.instance.ManageTransitionState(false);
    }*/
}
