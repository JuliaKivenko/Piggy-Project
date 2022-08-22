using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectPlayer : MonoBehaviour
{
    [SerializeField] ResourceVisualSettings resourceSettings;
    private ParticleSystemRenderer particleSystemRenderer;
    private ParticleSystem particleSystem;

    private void OnEnable()
    {
        ShopBuildingTrigger.onExchangeResources += PlayPurchaseEffect;
    }

    private void OnDisable()
    {
        ShopBuildingTrigger.onExchangeResources -= PlayPurchaseEffect;
    }

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        particleSystemRenderer = particleSystem.GetComponent<ParticleSystemRenderer>();
    }
    public void PlayPurchaseEffect(ResourceType resourceType)
    {
        particleSystemRenderer.mesh = resourceSettings.GetResourceVisualParameter<Mesh>(resourceType, "mesh");
        particleSystemRenderer.material = resourceSettings.GetResourceVisualParameter<Material>(resourceType, "material");
        particleSystem.Play();
    }
}
