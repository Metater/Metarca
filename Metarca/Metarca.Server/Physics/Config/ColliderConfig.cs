﻿using Metarca.Server.Physics.Types;

namespace Metarca.Server.Physics.Config;

public struct ColliderConfig
{
    public bool enabled;
    public bool isTrigger;
    public Collider collider;

    public ColliderConfig()
    {
        enabled = false;
        isTrigger = false;
        collider = new();
    }

    public ColliderConfig(bool isTrigger, Collider collider)
    {
        enabled = true;
        this.isTrigger = isTrigger;
        this.collider = collider;
    }
}