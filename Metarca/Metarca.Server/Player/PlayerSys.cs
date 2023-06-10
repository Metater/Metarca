using Metarca.Server.Ecs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metarca.Server.Player;

public class PlayerSys : DepsSys
{
    public PlayerSys(Deps deps, Sys? parent = null) : base(deps, parent)
    {

    }
}