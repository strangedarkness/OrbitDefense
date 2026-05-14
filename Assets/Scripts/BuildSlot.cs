using UnityEngine;
using UnityEngine.InputSystem;

public class BuildSlot : MonoBehaviour
{
    public BuildMenuUI buildMenu;

    private Camera cam;
    private bool built = false;

    void Start()
    {
        cam = Camera.main;

        if (buildMenu == null)
        {
            buildMenu = FindObjectOfType<BuildMenuUI>(true);
        }
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Ray ray = cam.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out RaycastHit hit, 1000f))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    if (!built && buildMenu != null)
                    {
                        buildMenu.Show(this);
                    }
                }
            }
        }
    }

    public void BuildTower(GameObject towerPrefab)
    {
        if (built) return;

        Instantiate(
            towerPrefab,
            transform.position + Vector3.up * 1f,
            Quaternion.identity
        );

        built = true;

        gameObject.SetActive(false);
    }
}