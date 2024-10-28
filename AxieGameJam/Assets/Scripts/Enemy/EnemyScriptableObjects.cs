using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyScriptalbeObject", menuName = "ScriptableObjects/Enemy")]
public class EnemyScriptableObjects : ScriptableObject
{
    //Base stats for enemies
    public float groundDist;

    [SerializeField]
    public float moveSpeed;
    public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value; }
    [SerializeField]
    public float maxHealth;
    public float MaxHealth { get => maxHealth; private set => maxHealth = value; }
    [SerializeField]
    public float damage;
    public float Damage { get => damage; private set => damage = value; }





}
