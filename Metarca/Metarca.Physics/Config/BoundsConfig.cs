using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metarca.Physics.Config;

public struct BoundsConfig
{
    public bool enabled;
    public Collider collider;

    public BoundsConfig()
    {
        enabled = false;
        collider = new();
    }

    public BoundsConfig(Collider collider)
    {
        enabled = true;
        this.collider = collider;
    }
}