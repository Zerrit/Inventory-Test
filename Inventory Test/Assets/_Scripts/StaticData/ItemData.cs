using UnityEngine;

namespace _Scripts.StaticData
{
    [CreateAssetMenu(fileName = "NewItem", menuName = "Items/New Item")]
    public class ItemData : ScriptableObject
    {
        [field:SerializeField] public string Name { get; private set; }
        [field:SerializeField, Range(0,1)] public float Status { get; private set; }
        [field:SerializeField] public Sprite Icon { get; private set; }
        [field:SerializeField] public Vector2 Size { get; private set; }
    }
}
