using UnityEngine;

public class BuildMenuUI : MonoBehaviour
{
    public GameObject basicTowerPrefab;
    public GameObject rapidTowerPrefab;
    public GameObject heavyTowerPrefab;

    private BuildSlot currentSlot;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Show(BuildSlot slot)
    {
        currentSlot = slot;
        gameObject.SetActive(true);
    }

    public void BuildBasic()
    {
        Build(basicTowerPrefab);
    }

    public void BuildRapid()
    {
        Build(rapidTowerPrefab);
    }

    public void BuildHeavy()
    {
        Build(heavyTowerPrefab);
    }

    private void Build(GameObject towerPrefab)
    {
        if (currentSlot == null) return;

        currentSlot.BuildTower(towerPrefab);
        gameObject.SetActive(false);
    }
}