using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalParent : MonoBehaviour
{
    [SerializeField] List<Portal> portals;
    [SerializeField] float _playerPortalCooldown;
    internal bool _canTeleportPlayer = true;

    public Vector3 OutputPortalLocation(Portal inputPortal)
    {
        foreach(Portal portal in portals)
            if (portal != inputPortal) return portal.transform.position;
        return inputPortal.transform.position;
    }
    
    public IEnumerator TeleportCooldown()
    {
        _canTeleportPlayer = false;
        yield return new WaitForSeconds(_playerPortalCooldown);
        _canTeleportPlayer = true;
    }
}
