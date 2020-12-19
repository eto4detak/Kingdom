using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    public List<Battle> battles = new List<Battle>();
    public Battle item;

    public void Registration(Vector3 pos, GameUnit self, GameUnit target)
    {
        Battle bat = Instantiate(item, pos, Quaternion.identity);
        bat.Registration(self, target);
        
    }

}
