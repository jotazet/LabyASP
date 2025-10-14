namespace Lab0.Models;

public class CalculatorModel
{
    public double? x {
        get;
        set;
    }
    public double y{
        get;
        set;
    }

    public Operators Operator
    {
        get;
        set;
    }
    
    public string Result
    {
        get
        {
            switch (Operator)
            {
                case Operators.Add:
                    return $"{x} + {y} = {x + y}";
                case Operators.Sub:
                    return $"{x} - {y} = {x - y}";
                case Operators.Mul:
                    return $"{x} * {y} = {x * y}";
                case Operators.Div:
                    return y == 0 ? "Dzielenie przez zero!" : $"{x} / {y} = {x / y}";
                default:
                    return "Nieznany operator";
            }
        }
    }
}