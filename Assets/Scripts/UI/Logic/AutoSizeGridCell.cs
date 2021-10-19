using UnityEngine;
using UnityEngine.UI;

public class AutoSizeGridCell : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup grid;

    [SerializeField] private Vector2 borders;
    void Start()
    {
        float width = GameObject.Find("Canvas").GetComponent<RectTransform>().rect.width;
        float sizeWindow = width - (borders.x + borders.y) - (grid.spacing.x * (grid.constraintCount-1));
        grid.cellSize = new Vector2(sizeWindow/grid.constraintCount, grid.cellSize.y);
    }

}
