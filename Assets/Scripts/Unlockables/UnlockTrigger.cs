using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnlockCondition
{
    public ResourceType type;
    public int currentAmount;
    public int requiredAmount;
    public bool isConditionFullfilled;

    public delegate void OnCurrentAmountChange();
    public static event OnCurrentAmountChange onCurrentAmountChange;

    public void IncreaseCurrentAmount()
    {
        currentAmount += 1;
        onCurrentAmountChange?.Invoke();
    }

}

public class UnlockTrigger : MonoBehaviour
{
    [SerializeField] private GameObject unlockToTriggerObject;
    [SerializeField] private float yOffsetUI;

    public List<UnlockCondition> conditionsToUnlock;

    private IUnlockableElement unlockToTrigger;
    private Coroutine resourcesDepleteRoutine;

    private bool isUnlocked = false;
    private bool isInTriggerArea = false;

    public delegate void OnUnlockableActive(Transform startPos, float yOffset, UnlockTrigger unlockTrigger);
    public static event OnUnlockableActive onUnlockableActive;

    public delegate void OnUnlockableInactive();
    public static event OnUnlockableInactive onUnlockableInactive;

    private void Start()
    {
        unlockToTrigger = unlockToTriggerObject.GetComponent<IUnlockableElement>();
    }

    private void Update()
    {
        if (CheckForUnlock() && resourcesDepleteRoutine != null)
            StopAllCoroutines();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isUnlocked)
                return;

            if (!CheckForUnlock())
                onUnlockableActive?.Invoke(transform, yOffsetUI, this);

            foreach (var element in conditionsToUnlock)
            {
                if (ResourceManager.GetResourceAmount(element.type) > 0)
                    StartCoroutine(TakeResources(element));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isUnlocked)
                return;

            StopAllCoroutines();
            onUnlockableInactive?.Invoke();
        }
    }

    private IEnumerator TakeResources(UnlockCondition element)
    {
        while (element.currentAmount < element.requiredAmount && ResourceManager.GetResourceAmount(element.type) > 0)
        {
            ResourceManager.ChangeResourceAmount(element.type, -1);
            element.IncreaseCurrentAmount();
            yield return new WaitForEndOfFrame();
        }
        element.isConditionFullfilled = true;
    }

    private bool CheckForUnlock()
    {
        foreach (var element in conditionsToUnlock)
        {
            if (element.isConditionFullfilled == false)
                return false;
        }

        unlockToTrigger.Unlock();
        isUnlocked = true;
        return true;
    }
}
