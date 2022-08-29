using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConditionsToUnlockDisplay : MonoBehaviour
{
    [SerializeField] ResourceVisualSettings settings;
    [SerializeField] List<TextMeshProUGUI> conditionsTexts;

    private UnlockTrigger unlockTrigger;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        UnlockTrigger.onUnlockableActive -= SetUpPosition;
        UnlockTrigger.onUnlockableInactive += HideUI;
    }
    private void OnDisable()
    {
        UnlockTrigger.onUnlockableActive += SetUpPosition;
        UnlockTrigger.onUnlockableInactive -= HideUI;
    }

    private void SetUpPosition(Transform pos, float yOffset, UnlockTrigger trigger)
    {
        unlockTrigger = trigger;
        transform.position = new Vector3(pos.position.x, pos.position.y + yOffset, pos.position.z);
        gameObject.SetActive(true);
        DisplayConditions();
    }

    private void HideUI() => gameObject.SetActive(false);

    private void DisplayConditions()
    {
        for (int i = 0; i < conditionsTexts.Count; i++)
        {
            conditionsTexts[i].gameObject.SetActive(false);
        }
        
        for (int i = 0; i < unlockTrigger.conditionsToUnlock.Count; i++)
        {
            UnlockCondition condition = unlockTrigger.conditionsToUnlock[i];
            string icon = settings.GetResourceVisualParameter<string>(condition.type, "iconName");
            conditionsTexts[i].text = $"{icon} {condition.currentAmount}/{condition.requiredAmount}";
            conditionsTexts[i].gameObject.SetActive(true);
        }
    }
}
