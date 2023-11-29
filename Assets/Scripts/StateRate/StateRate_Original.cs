/// <summary>
/// The Pragmatic Programmer: From Journeyman to Master
/// Andrew Hunt and David Thomas
/// pg. 188, Excercise 38
/// </summary>
public class StateRate_Original
{
    private enum State
    {
        Texas,
        Ohio, 
        Maine,
        Other
    }

    private const float TexasRate = 0.2f;
    private const float OhioRate = 0.3f;
    private const float MaineRate = 0.4f;

    private State _state;
    private float _rate;
    private float _base;
    private float _amount;
    private float _result;
    private int _points;

    public void Calculate()
    {
        if (_state == State.Texas)
        {
            _rate = TexasRate;
            _amount = _base * TexasRate;
            _result = 2 * Basis(_amount) + Extra(_amount) * 1.05f; 
        }
        else if (_state == State.Ohio || _state == State.Maine)
        {
            _rate = _state == State.Ohio ? OhioRate : MaineRate;
            _amount = _base * _rate;
            _result = 2 * Basis(_amount) + Extra(_amount) * 1.05f;
            if (_state == State.Ohio)
                _points = 2;
        }
        else
        {
            _rate = 1;
            _amount = _base;
            _result = 2 * Basis(_amount) + Extra(_amount) * 1.05f;
        }
    }
    private float Basis(float amount)
    {
        return amount * 0.9f;
    }
    private float Extra(float amount)
    {
        return amount * 0.1f;
    }
}
