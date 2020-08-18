using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailManager : Singleton<TrailManager>
{
    [SerializeField] private TrailRenderer prefabTrail;

    private TrailRenderer trail;

    public void SpeedUpCurrentTrail()
    {
        if (trail != null)
        {
            trail.time = trail.time / 2;
            Destroy(trail.gameObject, trail.time);
        }
    }

    public void StepTrail(RaycastHit mouseHit)
    {
        if (trail != null)
        {
            trail.transform.position = mouseHit.point + new Vector3(0, 0.2f, 0);
        }
    }

    public void CreateTrail(RaycastHit mouseHit)
    {
        if (trail != null)
        {
            trail.time = trail.time / 2;
            Destroy(trail.gameObject, trail.time);
        }
        trail = Instantiate(prefabTrail, mouseHit.point + new Vector3(0, 0.2f, 0), Quaternion.identity);
    }

}
