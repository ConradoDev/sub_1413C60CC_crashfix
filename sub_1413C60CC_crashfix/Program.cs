using CashSpawnEvent_CrashFix;

Console.WriteLine("[/] Trying to fix REPORT_CASH_SPAWN_EVENT...");

if (Memory.IsFiveOpen(out nint handle))
{
    if (Memory.ApplyFix(handle)) Console.WriteLine("[/] Successful.");
    else Console.WriteLine("[/] An error has ocurred. Maybe you're not using build 2372.");
}
else Console.WriteLine("[/] FiveM doesn't seem to be open at the moment.");

Console.ReadKey();