using RustSharp;

// Wrap a method with "Result" return value

Result<int, string> ParseInt(string str)
{
    try
    {
        return Result.Ok(int.Parse(str));
    }
    catch (Exception ex)
    {
        return Result.Err(ex.Message);
    }
}

// Call that method

string input = Console.ReadLine()!;
var result = ParseInt(input);

// Check if value is "Ok" or "Err"

var isOk = result.IsOk;
var isErr = result.IsErr;

// Match the result values

result.Match(
    (ok) =>
    {
        Console.WriteLine($"The integer you typed is {ok}");
    },
    (err) =>
    {
        Console.WriteLine($"The string you typed is not a integer, {err}");
    });


// Write a simple method with Option return value

Option<float> Divide(float a, float b)
{
    if (b != 0.0)
    {
        return Option.Some(a / b);
    }
    else
    {
        return Option.None();
    }
}

// Call that method

var option = Divide(114, 514);

// Check if the Option has value

var isSome = option.IsSome;
var isNone = option.IsNone;

// Match the result values

option.Match(
    (value) =>
    {
        Console.WriteLine($"Result: {value}");
    },
    () =>
    {
        Console.WriteLine("No value");
    });

// Unzip an option with tuple value
Option<(int, int)> qwq = Option.Some((114, 514));
(var option1, var option2) = qwq.Unzip();


