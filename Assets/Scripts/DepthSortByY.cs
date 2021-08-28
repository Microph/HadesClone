using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(SortingGroup))]
public class DepthSortByY : MonoBehaviour
{
    public int sortingOrderOffset = 1000;

    private const int sortingOrderValuePerYUnit = 100;

    private SortingGroup sortingGroup;
    
    private void Awake()
    {
        sortingGroup = GetComponent<SortingGroup>();
    }

    private void LateUpdate()
    {
        sortingGroup.sortingOrder = sortingOrderOffset - (int)(transform.position.y * sortingOrderValuePerYUnit);
    }
}