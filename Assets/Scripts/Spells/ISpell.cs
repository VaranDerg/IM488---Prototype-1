using System.Collections;
using System.Collections.Generic;

public interface ISpell
{

    public float TickRate { get; protected set; }
    public float TickRateScalar { get; protected set; }

    public void Execute();

    public void Tick(float deltaTime);
}
