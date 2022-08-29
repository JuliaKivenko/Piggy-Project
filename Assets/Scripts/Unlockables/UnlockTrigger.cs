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

}

public class UnlockTrigger : MonoBehaviour
{
    [SerializeField] private GameObject unlockToTriggerObject;
    [SerializeField] private float yOffsetUI;
    public List<UnlockCondition> conditionsToUnlock;

    private IUnlockableElement unlockToTrigger;
    private bool isUnlocked = false;


    public delegate void OnUnlockableActive(Transform startPos, float yOffset, UnlockTrigger unlockTrigger);
    public static event OnUnlockableActive onUnlockableActive;

    public delegate void OnUnlockableInactive();
    public static event OnUnlockableInactive onUnlockableInactive;

    private void Start()
    {
        unlockToTrigger = unlockToTriggerObject.GetComponent<IUnlockableElement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isUnlocked)
                return;

            TakeResources();

            if (!CheckForUnlock())
                onUnlockableActive?.Invoke(transform, yOffsetUI, this);
        }
        //TODO: resource gets taken away one by one    
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isUnlocked)
                return;
            onUnlockableInactive?.Invoke();
        }
    }

    private void TakeResources()
    {
        foreach (var element in conditionsToUnlock)
        {
            if (ResourceManager.GetResourceAmount(element.type) >= element.requiredAmount - element.currentAmount)
            {
                ResourceManager.ChangeResourceAmount(element.type, -(element.requiredAmount - element.currentAmount));
                element.currentAmount += (element.requiredAmount - element.currentAmount);
            }
            else
            {
                int resourceToTake = ResourceManager.GetResourceAmount(element.type);
                ResourceManager.ChangeResourceAmount(element.type, -resourceToTake);
                element.currentAmount += resourceToTake;
            }

            if (element.currentAmount == element.requiredAmount)
                element.isConditionFullfilled = true;
        }

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
