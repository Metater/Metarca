using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metarca.Server;

public class Server
{
    private readonly ServerManager serverManager = new();

    public Server()
    {
        
    }

    public void PollEvents()
    {
        serverManager.netManager.PollEvents();
    }

    public void Tick(ulong tickId)
    {

    }
}
