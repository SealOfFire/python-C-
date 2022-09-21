public class Calculator
{
    //Our addition function
    public double add(double num_one, double num_two)
    {
        return num_one + num_two;
    }
    //Our subtraction function
    public double subtract(double num_one, double num_two)
    {
        return num_one - num_two;
    }
    //Our multiplication function
    public double multiply(double num_one, double num_two)
    {
        return num_one * num_two;
    }
    //Our division function
    public double divide(double num_one, double num_two)
    {
        return num_one / num_two;
    }

    public object[] test1()
    {
        return new object[] { 1, "2", 2.3 };
    }

    public object test2()
    {
        return new { Name = "aaa", Age = 10 };
    }
}