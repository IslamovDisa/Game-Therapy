using UnityEngine;
using UnityEngine.UI;

public class ActorOnSceneButton : MonoBehaviour
{
    public int DataIndex { get; set; }

    [SerializeField] private Image _thumbnail;
    
    public void Init()
    {
        _thumbnail.sprite =
            Resources.Load<Sprite>("Thumbnails/" +
                                    DataController.Current.AppData.ActorModels[DataIndex].ThumbnailPath);

        var shopItemDrag = gameObject.GetComponent<ShopItemDrag>();
        shopItemDrag.Prefab =
            Resources.Load<GameObject>("Prefabs/" + DataController.Current.AppData.ActorModels[DataIndex].PrefabPath);
    }
}
