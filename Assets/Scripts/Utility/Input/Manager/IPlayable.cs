// Author(s): Paul Calande
// Interface for receiving input.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayable
{
    void ReceiveInput(InputReader inputReader);
}