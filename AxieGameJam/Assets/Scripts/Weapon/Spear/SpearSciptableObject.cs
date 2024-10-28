using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearSciptableObject : ScriptableObject    
{

    [SerializeField]
    public GameObject prefab;
    public GameObject Prefab {  get =>  prefab; private set => prefab = value; }
    [SerializeField]
    public float coolDown;
    public float CoolDown { get => coolDown; private set => coolDown = value; }

}
