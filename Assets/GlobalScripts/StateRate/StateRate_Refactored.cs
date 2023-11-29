using System;
using System.Collections.Generic;

/// <summary>
/// The Pragmatic Programmer: From Journeyman to Master
/// Andrew Hunt and David Thomas
/// pg. 188, Excercise 38
/// </summary>
public class StateRate_Refactored
{
    private enum State
    {
        Texas,
        Ohio,
        Maine,
        Other
    }

    private readonly Dictionary<State, float> _rateByState = new Dictionary<State, float>()
    {
        { State.Texas, 0.2f },
        { State.Ohio, 0.3f },
        { State.Maine, 0.4f },
        { State.Other, 1f }
    };
    private readonly Dictionary<State, int> _pointsByState = new Dictionary<State, int>()
    {
        { State.Texas, 0 },
        { State.Ohio, 2 },
        { State.Maine, 0 },
        { State.Other, 0 }
    };

    private State _state;
    private float _base;

    public float GetResult()
    {
        float amount = GetAmount();
        return 2 * Basis(amount) + Extra(amount) * 1.05f;
    }
    public float GetAmount()
    {
        return _base * GetRate();
    }
    private float Basis(float amount)
    {
        return amount * 0.9f;
    }
    private float Extra(float amount)
    {
        return amount * 0.1f;
    }

    public float GetRate()
    {
        return GetValue(_rateByState);
    }
    public int GetPoints()
    {
        return GetValue(_pointsByState);
    }
    private TValue GetValue<TValue>(IReadOnlyDictionary<State, TValue> dictionary)
    {
        if (!dictionary.ContainsKey(_state))
        {
            throw new InvalidOperationException($"State '{_state}' has no value assigned in the given dictionary");
        }
        return dictionary[_state];
    }
}
