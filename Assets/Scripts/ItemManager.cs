using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public enum ITEM { GUN, BOMB, ORBIT, LAZER, HP }

    [SerializeField] public ITEM _myItem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //print(other.gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {
            if(_myItem == ITEM.HP)
                other.GetComponent<PlayerManager>().RestoreHP();
            if(_myItem == ITEM.GUN)
                other.GetComponent<PlayerManager>().GunLevelUp();
            if(_myItem == ITEM.ORBIT)
                other.GetComponent<PlayerManager>().OrbitLevelUp();
            if(_myItem == ITEM.BOMB)
                other.GetComponent<PlayerManager>().BombLevelUp();
            Destroy(gameObject);
            
        }
    }
}
