// Author(s): Paul Calande
// Saves and loads the state data of an Animator.
// This includes the state of each layer and the state of each parameter.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorStateTracker
{
    // Maps layer number to state info.
    Dictionary<int, AnimatorStateInfo> stateInfoByLayer = new Dictionary<int, AnimatorStateInfo>();
    // Only save Triggers that are enabled.
    List<int> parametersTrigger = new List<int>();
    // These Dictionaries map a parameter hash to a parameter value.
    Dictionary<int, bool> parametersBool = new Dictionary<int, bool>();
    Dictionary<int, float> parametersFloat = new Dictionary<int, float>();
    Dictionary<int, int> parametersInt = new Dictionary<int, int>();

    // Clears all tracked data.
    private void Clear()
    {
        stateInfoByLayer.Clear();
        parametersTrigger.Clear();
        parametersBool.Clear();
        parametersFloat.Clear();
        parametersInt.Clear();
    }

    // Saves the given Animator's data.
    public void ReadFrom(Animator animator)
    {
        Clear();
        for (int i = 0; i < animator.layerCount; ++i)
        {
            stateInfoByLayer[i] = animator.GetCurrentAnimatorStateInfo(i);
        }
        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            int nameHash = Animator.StringToHash(parameter.name);
            switch (parameter.type)
            {
                case AnimatorControllerParameterType.Trigger:
                    if (animator.GetBool(nameHash))
                    {
                        parametersTrigger.Add(nameHash);
                    }
                    break;
                case AnimatorControllerParameterType.Bool:
                    parametersBool.Add(nameHash, animator.GetBool(nameHash));
                    break;
                case AnimatorControllerParameterType.Float:
                    parametersFloat.Add(nameHash, animator.GetFloat(nameHash));
                    break;
                case AnimatorControllerParameterType.Int:
                    parametersInt.Add(nameHash, animator.GetInteger(nameHash));
                    break;
                default:
                    continue;
            }
        }
    }

    // Modifies the given Animator using the saved data.
    public void WriteTo(Animator animator)
    {
        foreach (KeyValuePair<int, AnimatorStateInfo> pair in stateInfoByLayer)
        {
            int layer = pair.Key;
            AnimatorStateInfo stateInfo = pair.Value;
            animator.Play(stateInfo.shortNameHash, layer, stateInfo.normalizedTime);
        }
        foreach (int hash in parametersTrigger)
        {
            animator.SetTrigger(hash);
        }
        foreach (KeyValuePair<int, bool> pair in parametersBool)
        {
            animator.SetBool(pair.Key, pair.Value);
        }
        foreach (KeyValuePair<int, float> pair in parametersFloat)
        {
            animator.SetFloat(pair.Key, pair.Value);
        }
        foreach (KeyValuePair<int, int> pair in parametersInt)
        {
            animator.SetInteger(pair.Key, pair.Value);
        }
    }
}