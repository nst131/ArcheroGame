using UnityEngine;

[CreateAssetMenu(menuName = "Clothes" , fileName = "ClothesName")]
public class UIClothesData : ScriptableObject
{
    [Tooltip("Damage")]
    [SerializeField] private float _damage;
    public float Damage
    {
        get { return _damage; }
        protected set { }
    }
    [Tooltip("Health")]
    [SerializeField] private float _health;
    public float Health
    {
        get { return _health; }
        protected set { }
    }
}
