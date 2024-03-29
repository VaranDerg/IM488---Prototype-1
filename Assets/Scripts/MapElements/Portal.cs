using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] PortalParent _parent;


    private void OnTriggerStay(Collider collision)
    {
        ICanUsePortal teleportInterface = collision.GetComponent<ICanUsePortal>();
        if (teleportInterface == null || !_parent._canTeleportPlayer)
            return;

        _parent.StartCoroutine(_parent.TeleportCooldown());
        teleportInterface.TeleportTo(_parent.OutputPortalLocation(this));

        ManagerParent.Instance.Audio.PlaySoundEffect("Teleport");
    }
}
