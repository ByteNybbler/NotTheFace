// Author(s): Paul Calande
// MonoPeriodicVar for integers.

public class MonoPeriodicInt : MonoPeriodicVar<int>
{
    public void AddVar(int amount)
    {
        pvar.SetVar(pvar.GetVar() + amount);
    }
}