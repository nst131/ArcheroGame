using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public abstract void Tactic();
    public abstract void Move();
    public abstract void RotateToPlayer();
    public abstract void ShootAttack();
    public abstract void LevelUp();
    public abstract void DropThings();
}
