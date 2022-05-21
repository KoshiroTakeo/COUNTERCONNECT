using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimeTrigger : MonoBehaviour
{
    Boss2Manager AnimeController;

    void Start()
    {
        AnimeController = this.transform.root.gameObject.GetComponent<Boss2Manager>();
    }

    void Update()
    {
        
    }

    void LateUpdate()
    {
        this.transform.localScale = Vector3.one * 100;
    }

        void CallAnimeEnd()
    {
        AnimeController.AnimeEnd();
    }

    void CallAnimeWait_TailShot()
    {
        AnimeController.AnimeWait_TailShot();
    }

    void WaistShot()
    { 
        AnimeController.WaistShot();
    }

    void TailShot()
    {
        AnimeController.TailShot();
    }

    void Scythe()
    {
        AnimeController.Scythe();
    }
}
