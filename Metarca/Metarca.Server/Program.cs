using Metarca.Server;
using System.Diagnostics;

CancellationTokenSource cts = new();
Console.CancelKeyPress += (s, e) =>
{
    Console.WriteLine("Cancelling...");
    cts.Cancel();
    e.Cancel = true;
};

Console.WriteLine("Hello, World!");

Server server = new();

const double TicksPerSecond = 200000;
ulong tickId = 0;
Stopwatch stopwatch = Stopwatch.StartNew();
while (true)
{
	server.PollEvents();

	while ((ulong)(stopwatch.Elapsed.TotalSeconds * TicksPerSecond) > tickId)
	{
		server.Tick(tickId++);
	}

	Console.Title = $"Tick Id: {tickId}, TPS: {TicksPerSecond}";
	Thread.Sleep(15);
}

/*
Metarca, mine Stat points, the more effort that goes into a certain player, the more powerful  it should be, handicap low computational power bots
attack types:
	10 sec vote on attack type, random if cant agree:
		rock paper scissors
		hash war
		most money
		most attack tokens
		most something
prestige mining:
	get mining percentage buff when you prestige
set velocity
	checks of course
lnl? key
hash provider rust app
disappear from world when in battle, then spawn back in where left
encryption for stuff?
orbits
planets
all 2d movement
placing machines
interacting with machines
ore areas on planets
square regions around circle planet
all circle colliders those move, squares stand still
maybe circles made of tiles
use the cpu intensive hash alg
statistics of game
each game is a simulation
you have to agree to fight
walls you cant go past, or have to break somehow
maybe it is a scanning area, not rays, so its just a circle you see things inside of
everything is a object, scanner says it exists, so it is set back to client
use spacial hashing, maybe it is a square region
create and join factions somehow, faction leader
players can be human controlled or computer, anything goes!!!!!!!!!!!!!! nothing is bannable, anything is allowed, no rules
explosives
if data doesnt fit into the packet too bad!, randomize order of things sent maybe?

 */