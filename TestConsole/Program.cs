using System.Collections.Concurrent;
using RustSharp;

// 创建 Option

var none = Option<int>.None();
var some = Option<int>.Some(10);
var option = Option<int>.Create(10);


// 或者使用 Option 类提供的工具方法
// 在这里, 方法的返回值会自动转为对应的 Option 类型
// 当你在 return 语句或者某些能识别到返回值的地方, 使用这个可以直接省略泛型参数

Option<int> none2 = Option.None();
Option<int> some2 = Option.Some(123);
var option2 = Option.Create(3.14);

static Option<int> SomeMethod()
{
    // 此处省略了泛型参数 int
    return Option.Some(123);
}


// 从可能为空的值创建 Option
// 如果值不为空, 会得到 Some, 否则返回 None

var optionalString = Option.Create(Console.ReadLine());


// 判断 Option 包含的值

bool isSome = option.IsSome;
bool isNone = option.IsNone;


// 检查 Option 的值
// 当值为 Some 的时候, 传入的委托会被执行

option.Inspect(value => Console.WriteLine(value));


// 对 Option 的值进行匹配
// 当为 Some 时, 第一个委托会被执行, 当为 None 时, 第二个委托会被执行

option.Match(
    value =>
    {
        Console.WriteLine($"Some value: {value}");
    },
    () =>
    {
        Console.WriteLine("No value");
    });


// 使用 if 语句对其值进行匹配

if (option is SomeOption<int> _some)
{
    Console.WriteLine($"Some value: {_some.Value}");
}


// 使用 switch 语句对其值进行匹配

switch (option)
{
    case SomeOption<int> _some2:
        Console.WriteLine($"Some value: {_some2.Value}");
        break;

    case NoneOption<int> _:
        Console.WriteLine($"No value");
        break;
}


// 从 Option 中强行取值
// 但当 Option 是不包含值的 None 时, 会抛出 UnwrapException 异常

var valueOfOption = option.Unwrap();


// 从 Option 中取值, 若没有值则使用指定的默认值

var valueOfOption2 = option.UnwrapOr(123);


// 从 Option 中取值, 若没有值则使用指定委托的返回值
// 如果 Option 包含值, 指定委托不会被调用. 这适合某些使用 '懒加载' 的场景

var valueOfOption3 = option.UnwrapOrElse(() => DateTime.Now.Hour);

// 从 Option 中强行取值
// 和 Unwrap 类型, 但是需要传入一个文本作为异常消息内容

var valueOfOption4 = option.Expect("You must input a number");


// 将 Option 映射为新值

var mappedValue = option.Map(number => $"{number * 3}");


// 将 Option 映射为新值, 如果已有 Option 中不包含值, 则使用指定的默认新值

var mappedValue2 = option.MapOr("0", number => $"{number * 3}");


// 将 Option 映射为新值, 如果已有 Option 中不包含值, 则使用指定委托的调用返回值作为新值

var mappedValue3 = option.MapOrElse(() => Guid.NewGuid().ToString(), number => $"{number * 3}");


// 将两个 Option 压缩为一个 Option
// 如果任意一个 Option 为 None, 会返回 None

var zippedOption = option.Zip(option2);


// 将两个 Option 使用指定逻辑压缩为一个 Option
// 同上, 如果任意一个 Option 为 None, 会返回 None

var zippedOption2 = option.ZipWith(option2, (v1, v2) => $"{v1}, {v2}");










// 创建 Result

var ok = Result<int, string>.Ok(123);
var err = Result<int, string>.Err("Invalid input, not a number");


// 或者使用 Result 类提供的工具方法
// 在这里, 方法的返回值会自动转为对应的 Result 类型
// 当你在 return 语句或者某些能识别到返回值的地方, 使用这个可以直接省略泛型参数

Result<int, string> ok2 = Result.Ok(123);
Result<int, string> err2 = Result.Err("Invalid operation");
var result = SomeMethod2();

static Result<double, string> SomeMethod2()
{
    // 此处调用 Ok 时省略了泛型参数 double 和 string
    return Result.Ok(3.14);
}


// 判断 Result 包含的值

bool isOk = result.IsOk;
bool isErr = result.IsErr;


// 检查 Result 的值
// 当值时目标情况的时候, 传入的委托会被执行

result.Inspect(number => Console.WriteLine(number));
result.InspectErr(errorText => File.AppendAllText("log.txt", $"{errorText}\r\n"));


// 对 Result 的值进行匹配
// 当为 Ok 时, 第一个委托会被执行, 当为 Err 时, 第二个委托会被执行

result.Match(
    ok =>
    {
        Console.WriteLine(ok);
    },
    err =>
    {
        File.AppendAllText("log.txt", $"{err}\r\n");
    });


// 使用 if 语句对其值进行匹配

if (result is OkResult<double, string> _ok)
{
    Console.WriteLine(_ok.Value);
}


// 使用 switch 语句对其值进行匹配

switch (result)
{
    case OkResult<double, string> _ok2:
        Console.WriteLine(_ok2.Value);
        break;

    case ErrResult<double, string> _err2:
        File.AppendAllText("log.txt", $"{_err2.Value}\r\n");
        break;
}


// 从 Result 中强行取值
// 同理, 如果存储的值和期望值不一致, 会抛出异常

var valueOfResult = result.Unwrap();
var errorOfResult = result.UnwrapErr();
