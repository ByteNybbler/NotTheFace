// Author(s): Paul Calande
// MonoTimedVar for integers.

public class MonoTimedInt : MonoTimedVar<int>
{
    public void AddVar(int amount)
    {
        pvar.SetVar(pvar.GetVar() + amount);
    }
}