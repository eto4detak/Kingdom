using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEvasionTactics : IAITactics
{
    public float need;

    private float leftDirection = 0;
    private float turnTime = 2f;
    private float evasionNeed = 10f;
    private float currentTime;
    private bool closeBorder;
    private Unit owner;
    //private IMagic dangerSpell;
    private Vector3 evasionDirect;
    private bool needNewDirect;
    //private IMagic oldDangerSpell;

    public SpellEvasionTactics()
    {
    }

    public void Control()
    {
        if (needNewDirect)
        {
            needNewDirect = false;
            //evasionDirect = Vector3.Cross(dangerSpell.direction, Vector3.up);

            if (evasionDirect == Vector3.zero)
            {
                evasionDirect = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
            }
            leftDirection = Random.Range(-1, 1);
            if (leftDirection > -1) leftDirection = 1;
            evasionDirect = evasionDirect * leftDirection;

        }
        //owner.Move(evasionDirect);
    }

    public float CheckNeed()
    {
        //dangerSpell = null;
        need = -1;
        //float dangerAngle = 30f;
        //float dangerDistance = 8f;
        //float defenceDistance = 2f;
        //List<IMagic> magics = MagicManager.instance.magicsInScene;
        
        //for (int i = 0; i < magics.Count; i++)
        //{
        //    float magicDistance = (magics[i].transform.position - owner.transform.position).magnitude;
        //    if (magicDistance > dangerDistance) continue;
        //    switch (magics[i].type)
        //    {
        //        case MagicType.attack:
        //            {
        //                Vector3 dangerDirection = owner.transform.position - magics[i].transform.position;
        //                dangerDirection.y = magics[i].direction.y;
        //                float attackAngle = Vector3.Angle(dangerDirection, magics[i].direction);
        //                if (attackAngle < dangerAngle)
        //                {
        //                    dangerSpell = magics[i];
        //                    need = magics[i].damage;
        //                }
        //            }
        //            break;
        //        case MagicType.protection:
        //            {
        //                float dist = (owner.transform.position - magics[i].transform.position).magnitude;
        //                if (dist < defenceDistance)
        //                {
        //                    dangerSpell = magics[i];
        //                    need = magics[i].damage;
        //                }
        //            }
        //            break;
        //        default:
        //            break;
        //    }
        //}
        //if(dangerSpell != null && oldDangerSpell != dangerSpell)
        //{
        //    needNewDirect = true;
        //}
        //oldDangerSpell = dangerSpell;
        return need;
    }
}
