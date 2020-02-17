using UnityEngine;

[CreateAssetMenu(menuName ="Levels/Player Level",fileName ="Level")]
public class UILevelUpData : ScriptableObject
{
    [Tooltip("Количество опыта на этом Уровне")]
    [SerializeField] private float _amountExperience;
    public float AmountExperience
    {
        get { return _amountExperience; }
        protected set { }
    }
}
