using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterSpell : MonoBehaviour
{

    bool hasFire = false;
    bool hasIce = false;
    bool hasLightning = false;

    delegate void OnProjectileLaunch(object sender);
    event OnProjectileLaunch Launched;

    public void ApplyElement(Elements.SpellElement element)
    {
        switch (element)
        {
            case Elements.SpellElement.Fire:
                //Launched += FireMod;
                hasFire = true;
                return;
            case Elements.SpellElement.Ice:
                //Launched += IceMod;
                hasIce = true;
                return;
            case Elements.SpellElement.Lightning:
                //Launched += LightningMod;
                hasLightning = true;
                return;
            default:
                return;
        }
    }

    public void Launch()
    {
        Launched.Invoke(this);
    }

    public void OnProjectileInstantiated(GameObject projectileObj)
    {
        if (hasFire)
            FireMod(projectileObj);

        if (hasIce)
            IceMod(projectileObj);

        if (hasLightning)
            LightningMod(projectileObj);
    }

    public void FireMod(GameObject projectileObj)
    {
        projectileObj.GetComponent<Spawner>().SetActive(true);
    }

    public void IceMod(GameObject projectileObj)
    {
        projectileObj.transform.GetChild(0).gameObject.SetActive(true);
        projectileObj.GetComponent<IcePulse>().enabled = true;
    }

    public void LightningMod(GameObject projectileObj)
    {
        projectileObj.transform.GetChild(1).gameObject.SetActive(true);
        projectileObj.transform.GetChild(2).gameObject.SetActive(true);
    }

    /*public void FireMod(object sender)
    {

    }

    public void IceMod(object sender)
    {

    }

    public void LightningMod(object sender)
    {

    }*/
}
