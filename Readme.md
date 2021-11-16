# NashTech C# Coding Convention

[[_TOC_]]

## 1. Introduction

This document requires or recommends certain practices for developing programs in the C#
language. The objective of this coding standard is to have a positive effect on:
- Avoidance of errors/bugs, especially the hard-to-find ones
- Maintainability, by promoting some proven design principles
- Maintainability, by requiring or recommending a certain unity of style
- Performance, by dissuading wasteful practices

**[⬆ back to top](#nashtech-c#-coding-convention)**

## 2. Naming convention

Naming conventions make programs more understandable by making them easier to read. This
section lists the convention to be used in naming

**[⬆ back to top](#nashtech-c#-coding-convention)**

#### 2.1 Capitalisation

We will use the three following conventions for capitalising identifiers

| Identifiers | Capitalisation Style | Example |
|----------|----------|-----------|
| Class | Pascal | AppDomain |
| Enum Type | Pascal | ErrorLevel |
| Enum Element | Pascal | FaltalError |
| Event | Pascal | WebException. Note: Always ends with the suffix Exception |
|Interface | Pascal | IDisposable. Note: Always begins with the prefix I |
| Method | Pascal | ToString |
| Namespace | Pascal | System.Web |
| Property | Pascal | CreatedOn |
| Paremeter | camel | salesAmount |
| Local variable | camel | currentUser |
| Field | camel | _userRepository. Note: with _ prefix |
| Read-only static field | Pascal | SystemUserId |
| Constant | Pascal | public const int Months = 12; |

**[⬆ back to top](#nashtech-c#-coding-convention)**

#### 2.2 Avoid Hungarian notation

Hungarian Notation restates the type which is already present in the declaration. This is pointless since modern IDEs will identify the type.

```csharp
// Bad
int iCounter;
string strFullName;
DateTime dModifiedDate;

// Good
int counter;
string fullName;
DateTime modifiedDate;
```

**[⬆ back to top](#nashtech-c#-coding-convention)**

#### 2.3 Abbreviations

Avoid using abbreviations, except commonly used: Id, Xml, Ftp, Uri, IO 

Use PascalCasing for abbreviations 3 characters or more (2 chars are both uppercase)

```csharp
HtmlHelper htmlHelper;
FtpTransfer ftpTransfer;
UIControl uiControl;
```

**[⬆ back to top](#nashtech-c#-coding-convention)**

#### 2.4 Use meaningful names

Basically, don't name a variable, class, or method which needs some explanation in comments

```csharp
// Bad names
int d; // elapsed time in days  
int s; // elapsed time in seconds

// Good names
int elapsedTimeInDays;  
int daysSinceCreation; 
int daysSinceModification;  
int fileAgeInDays;
```

Only use i, j, k for loop “FOR”

**[⬆ back to top](#nashtech-c#-coding-convention)**

## 3. Layout conventions

Good layout uses formatting to emphasize the structure of your code and to make the code easier to read

#### 3.1 Use the Code Editor settings

Use the default Code Editor settings smart indenting, four-character indents, tabs saved as spaces
#### 3.2 Write only one statement per line

#### 3.3 Write only one declaration per line

#### 3.4 One blank line between method definitions and between property definitions.

Also avoid more than one empty line at any time. 

#### 3.5 One class per file

#### 3.6 Keep the class small

Big classes make the code harder to manage. Generally a class shouldn't have more than 1000 lines of code.

#### 3.7 Keep method short

The more lines of code in a method the harder it is to understand. Generally a method shouldn't have more than 70 lines of code.

#### 3.8 Avoid too many parameters

Declare a class instead of too many parameters

❌ **BAD** 

```csharp
public void Checkout(string shippingName, string shippingCity, 
       string shippingSate, string shippingZip, string billingName, 
       string billingCity, string billingSate, string billingZip)
{

}
```

:white_check_mark: **GOOD**

```csharp
public void Checkout(ShippingAddress shippingAddress, BillingAddress billingAddress)
{
}
```

**[⬆ back to top](#nashtech-c#-coding-convention)**

#### 3.9 Avoid `this.` unless absolutely necessary 

## 4. Commenting Conventions

Place the comment on a separate line, not at the end of a line of code. Begin comment text with an uppercase letter. End comment text with a period.

Insert one space between the comment delimiter (//) and the comment text, as shown in the following example.

```csharp
// The following declaration creates a query. It does not run
// the query.
```

**[⬆ back to top](#nashtech-c#-coding-convention)**

#### 4.1 Avoid positional markers

They usually just add noise. Let the functions and variable names along with the proper indentation and formatting give the visual structure to your code.

❌ **BAD** 

```csharp
////////////////////////////////////////////////////////////////////////////////
// Scope Model Instantiation
////////////////////////////////////////////////////////////////////////////////
var model = new Menu
{
    Name = "foo",
    Link = "bar"
};

////////////////////////////////////////////////////////////////////////////////
// Render Menu
////////////////////////////////////////////////////////////////////////////////
private void RenderMenu()
{
    // ...
};
```

❌ **BAD** 

```csharp
#region Scope Model Instantiation

var model = new Menu
{
    Name = "foo",
    Link = "bar"
};

#endregion

#region Render Menu

private void RenderMenu()
{
    // ...
};

#endregion
```

:white_check_mark: **GOOD**

```csharp
var model = new Menu
{
    Name = "foo",
    Link = "bar"
};

private void RenderMenu()
{
    // ...
};
```

**[⬆ back to top](#nashtech-c#-coding-convention)**

#### 4.2 Don't use journal comments

Remember, use version control! There's no need for dead code, commented code, and especially journal comments. Use git log to get history!

❌ **BAD** 

```csharp
/**
 * 2018-12-20: Removed monads, didn't understand them (RM)
 * 2017-10-01: Improved using special monads (JP)
 * 2016-02-03: Removed type-checking (LI)
 * 2015-03-14: Added combine with type-checking (JR)
 */
public int Combine(int a,int b)
{
    return a + b;
}
```

:white_check_mark: **GOOD**

```csharp
public int Combine(int a,int b)
{
    return a + b;
}
```

**[⬆ back to top](#nashtech-c#-coding-convention)**

#### 4.3 Only comment things that have business logic complexity

Comments are an apology, not a requirement. Good code mostly documents itself.

❌ **BAD** 

```csharp
public int HashIt(string data)
{
    // The hash
    var hash = 0;

    // Length of string
    var length = data.length;

    // Loop through every character in data
    for (var i = 0; i < length; i++)
    {
        // Get character code.
        const char = data.charCodeAt(i);
        // Make the hash
        hash = ((hash << 5) - hash) + char;
        // Convert to 32-bit integer
        hash &= hash;
    }
}
```

**Better but still ❌  BAD**

```csharp
public int HashIt(string data)
{
    var hash = 0;
    var length = data.length;
    for (var i = 0; i < length; i++)
    {
        const char = data.charCodeAt(i);
        hash = ((hash << 5) - hash) + char;

        // Convert to 32-bit integer
        hash &= hash;
    }
}
```
If a comment explain WHAT the code is doing, it is probably a useless comment and can be implemented with a well named variable or function. The comment in the previous code could be replaced with a function named ConvertTo32bitInt so this comment is still useless. However it would be hard to express by code WHY the developer choose djb2 hash algorithm instead of sha-1 or another hash function. In that case a comment is acceptable.

:white_check_mark: **GOOD**

```csharp
public int Hash(string data)
{
    var hash = 0;
    var length = data.length;

    for (var i = 0; i < length; i++)
    {
        var character = data[i];
        // Use of djb2 hash algorithm as it has a good compromise
        // between speed and low collision with a very simple implementation.
        hash = ((hash << 5) - hash) + character;

        hash = ConvertTo32BitInt(hash);
    }
    return hash;
}

private int ConvertTo32BitInt(int value)
{
    return value & value;
}
```

**[⬆ back to top](#nashtech-c#-coding-convention)**

## 5. Language Guidelines

#### 5.1 Use string interpolation

Use string interpolation to concatenate short strings, as shown in the following code.

```csharp
string displayName = $"{nameList[n].LastName}, {nameList[n].FirstName}";
```

**[⬆ back to top](#nashtech-c#-coding-convention)**

#### 5.2 Use StringBuilder

To append strings in loops, especially when you are working with large amounts of text, use a StringBuilder object

```csharp
var phrase = "lalalalalalalalalalalalalalalalalalalalalalalalalalalalalala";
var manyPhrases = new StringBuilder();
for (var i = 0; i < 10000; i++)
{
    manyPhrases.Append(phrase);
}
//Console.WriteLine("tra" + manyPhrases);
```

**[⬆ back to top](#nashtech-c#-coding-convention)**

#### 5.3 Implicitly Typed Local Variables

Use implicit typing for local variables when the type of the variable is obvious from the right side of the assignment, or when the precise type is not important.

```csharp
// When the type of a variable is clear from the context, use var
// in the declaration.
var var1 = "This is clearly a string.";
var var2 = 27;
var var3 = Convert.ToInt32(Console.ReadLine());
```

Do not use var when the type is not apparent from the right side of the assignment.
```csharp
// When the type of a variable is not clear from the context, use an
// explicit type.
int var4 = ExampleClass.ResultSoFar();
```

**[⬆ back to top](#nashtech-c#-coding-convention)**

#### 5.4 Use nameof(...) whenever possible

Use `nameof(...)` instead of "..." whenever possible and relevant

**[⬆ back to top](#nashtech-c#-coding-convention)**

#### 5.5 File path separators

Windows uses \ and OS X and Linux use / to separate directories. Instead of hard-coding either type of slash, use `Path.Combine()` or `Path.DirectorySeparatorChar`.

If this is not possible (such as in scripting), use a forward slash. Windows is more forgiving than Linux in this regard.

**[⬆ back to top](#nashtech-c#-coding-convention)**

## 6 Exception Handling

Exception handling is one of the most important indicators to show the quality of code. Poor exception handling can have serious impact on the maintainability of a system. Proper exception handling isn't very difficult. It all comes down to grasping the fundamentals.

**[⬆ back to top](#nashtech-c#-coding-convention)**

#### 6.1 Don't catch exceptions unless you actually handle them

Only put the code in try/catch block when you need to handle the exception that might happen in that code. Logging the error message normally is not considered as handling exception.

❌ **BAD** 

```csharp
public async Task<ActionResult<IEnumerable<CategoryVm>>> GetCategories()
{
    try
    {
        return await _context.Categories
            .Select(x => new CategoryVm { Id = x.Id, Name = x.Name })
            .ToListAsync();
    }
    catch(Exception ex)
    {
        _logger.LogError(ex, "Cannot read database");
        throw;
    }
}
```

:white_check_mark: **GOOD**

```csharp
public async Task<ActionResult<IEnumerable<CategoryVm>>> GetCategories()
{
    return await _context.Categories
        .Select(x => new CategoryVm { Id = x.Id, Name = x.Name })
        .ToListAsync();
}
```

**[⬆ back to top](#nashtech-c#-coding-convention)**

#### 6.2 Don't swallow exceptions

❌ **BAD** 

```csharp
try
{
    // Do something might throw exception;
}
catch (Exception ex)
{
    // silent exception
}
```

The error message and the stack trace of the exception is the clue to identify what the problem is. Without these information it is almost impossible to know what wrong happened except debugging to the source code which is hard on production and time consuming

❌ **BAD** 

```csharp
try
{
    // Do something might throw exception.
}
catch (Exception ex)
{
    // Any action something like roll-back or logging etc.
    throw ex;
}
```

`throw ex;` throws the exception but resets the stack trace, destroying all stack trace information until your catch block.

:white_check_mark: **GOOD**

```csharp
try
{
    // Do something might throw exception.
}
catch (Exception ex)
{
    // Any action something like roll-back or logging etc.
    throw;
}
```

`throw;` re-throws the original exception and preserves its original stack trace.

:white_check_mark: **GOOD**

```csharp
try
{
    // Do something might throw exception.
}
catch (Exception ex)
{
    // Any action something like roll-back or logging etc.
    throw new YourBusinessException("Your error message.", ex);
}
```

`throw new YourBusinessException("Your error message.", ex);` wrap the Exception to add more information

**[⬆ back to top](#nashtech-c#-coding-convention)**

#### 6.3 Use multiple catch block instead of if conditions.

If you need to take action according to type of the exception, you better use multiple catch block for exception handling. In in this case the most specific Exception should be catch first then to the more general Exception

❌ **BAD** 

```csharp
try
{
    // Do something..
}
catch (Exception ex)
{

    if (ex is TaskCanceledException)
    {
        // Take action for TaskCanceledException
    }
    else if (ex is TaskSchedulerException)
    {
        // Take action for TaskSchedulerException
    }
}
```
:white_check_mark: **GOOD**

```csharp
try
{
    // Do something..
}
catch (TaskCanceledException ex)
{
    // Take action for TaskCanceledException
}
catch (TaskSchedulerException ex)
{
    // Take action for TaskSchedulerException
}
```

**[⬆ back to top](#nashtech-c#-coding-convention)**

## 7 Asynchronous Programming

Asynchronous programming has been around for several years on the .NET platform but has historically been very difficult to do well. Since the introduction of async/await
in C# 5 asynchronous programming has become mainstream. Modern frameworks (like ASP.NET Core) are fully asynchronous and it's very hard to avoid the async keyword when writing
web services. As a result, there's been lots of confusion on the best practices for async and how to use it properly. This section will try to lay out some guidance with examples of bad and good patterns of how to write asynchronous code.

**[⬆ back to top](#nashtech-c#-coding-convention)**

#### 7.1 Asynchrony is viral 

Once you go async, all of your callers **SHOULD** be async, since efforts to be async amount to nothing unless the entire callstack is async. In many cases, being partially async can be worse than being entirely synchronous. Therefore it is best to go all in, and make everything async at once.

❌ **BAD** This example uses the `Task.Result` and as a result blocks the current thread to wait for the result. This is an example of [sync over async](#avoid-using-taskresult-and-taskwait).

```csharp
public int DoSomethingAsync()
{
    var result = CallDependencyAsync().Result;
    return result + 1;
}
```

:white_check_mark: **GOOD** This example uses the await keyword to get the result from `CallDependencyAsync`.

```csharp
public async Task<int> DoSomethingAsync()
{
    var result = await CallDependencyAsync();
    return result + 1;
}
```

**[⬆ back to top](#nashtech-c#-coding-convention)**

#### 7.2 Async void

Use of async void in ASP.NET Core applications is **ALWAYS** bad. Avoid it, never do it. Typically, it's used when developers are trying to implement fire and forget patterns triggered by a controller action. Async void methods will crash the process if an exception is thrown. We'll look at more of the patterns that cause developers to do this in ASP.NET Core applications but here's a simple example:

❌ **BAD** Async void methods can't be tracked and therefore unhandled exceptions can result in application crashes.

```csharp
public class MyController : Controller
{
    [HttpPost("/start")]
    public IActionResult Post()
    {
        BackgroundOperationAsync();
        return Accepted();
    }
    
    public async void BackgroundOperationAsync()
    {
        var result = await CallDependencyAsync();
        DoSomething(result);
    }
}
```

:white_check_mark: **GOOD** `Task`-returning methods are better since unhandled exceptions trigger the [`TaskScheduler.UnobservedTaskException`](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.taskscheduler.unobservedtaskexception?view=netframework-4.7.2).

```csharp
public class MyController : Controller
{
    [HttpPost("/start")]
    public IActionResult Post()
    {
        Task.Run(BackgroundOperationAsync);
        return Accepted();
    }
    
    public async Task BackgroundOperationAsync()
    {
        var result = await CallDependencyAsync();
        DoSomething(result);
    }
}
```

**[⬆ back to top](#nashtech-c#-coding-convention)**

#### 7.3 Prefer `Task.FromResult` over `Task.Run` for pre-computed or trivially computed data

For pre-computed results, there's no need to call `Task.Run`, that will end up queuing a work item to the thread pool that will immediately complete with the pre-computed value. Instead, use `Task.FromResult`, to create a task wrapping already computed data.

❌ **BAD** This example wastes a thread-pool thread to return a trivially computed value.

```csharp
public class MyLibrary
{
   public Task<int> AddAsync(int a, int b)
   {
       return Task.Run(() => a + b);
   }
}
```

:white_check_mark: **GOOD** This example uses `Task.FromResult` to return the trivially computed value. It does not use any extra threads as a result.

```csharp
public class MyLibrary
{
   public Task<int> AddAsync(int a, int b)
   {
       return Task.FromResult(a + b);
   }
}
```

:bulb:**NOTE: Using `Task.FromResult` will result in a `Task` allocation. Using `ValueTask<T>` can completely remove that allocation.**

:white_check_mark: **GOOD** This example uses a `ValueTask<int>` to return the trivially computed value. It does not use any extra threads as a result. It also does not allocate an object on the managed heap.

```csharp
public class MyLibrary
{
   public ValueTask<int> AddAsync(int a, int b)
   {
       return new ValueTask<int>(a + b);
   }
}
```

**[⬆ back to top](#nashtech-c#-coding-convention)**

#### 7.4 Avoid using `Task.Result` and `Task.Wait`

There are very few ways to use `Task.Result` and `Task.Wait` correctly so the general advice is to completely avoid using them in your code. 

###### :warning: Sync over `async`

Using `Task.Result` or `Task.Wait` to block wait on an asynchronous operation to complete is *MUCH* worse than calling a truly synchronous API to block. This phenomenon is dubbed "Sync over async". Here is what happens at a very high level:

- An asynchronous operation is kicked off.
- The calling thread is blocked waiting for that operation to complete.
- When the asynchronous operation completes, it unblocks the code waiting on that operation. This takes place on another thread.

The result is that we need to use 2 threads instead of 1 to complete synchronous operations. This usually leads to [thread-pool starvation](https://blogs.msdn.microsoft.com/vancem/2018/10/16/diagnosing-net-core-threadpool-starvation-with-perfview-why-my-service-is-not-saturating-all-cores-or-seems-to-stall/) and results in service outages.

###### :warning: Deadlocks

The `SynchronizationContext` is an abstraction that gives application models a chance to control where asynchronous continuations run. ASP.NET (non-core), WPF and Windows Forms each have an implementation that will result in a deadlock if Task.Wait or Task.Result is used on the main thread. This behavior has led to a bunch of "clever" code snippets that show the "right" way to block waiting for a Task. The truth is, there's no good way to block waiting for a Task to complete.

:bulb:**NOTE: ASP.NET Core does not have a `SynchronizationContext` and is not prone to the deadlock problem.**

❌ **BAD** The below are all examples that are, in one way or another, trying to avoid the deadlock situation but still succumb to "sync over async" problems.

```csharp
public string DoOperationBlocking()
{
    // Bad - Blocking the thread that enters.
    // DoAsyncOperation will be scheduled on the default task scheduler, and remove the risk of deadlocking.
    // In the case of an exception, this method will throw an AggregateException wrapping the original exception.
    return Task.Run(() => DoAsyncOperation()).Result;
}

public string DoOperationBlocking2()
{
    // Bad - Blocking the thread that enters.
    // DoAsyncOperation will be scheduled on the default task scheduler, and remove the risk of deadlocking.
    return Task.Run(() => DoAsyncOperation()).GetAwaiter().GetResult();
}

public string DoOperationBlocking3()
{
    // Bad - Blocking the thread that enters, and blocking the theadpool thread inside.
    // In the case of an exception, this method will throw an AggregateException containing another AggregateException, containing the original exception.
    return Task.Run(() => DoAsyncOperation().Result).Result;
}

public string DoOperationBlocking4()
{
    // Bad - Blocking the thread that enters, and blocking the theadpool thread inside.
    return Task.Run(() => DoAsyncOperation().GetAwaiter().GetResult()).GetAwaiter().GetResult();
}

public string DoOperationBlocking5()
{
    // Bad - Blocking the thread that enters.
    // Bad - No effort has been made to prevent a present SynchonizationContext from becoming deadlocked.
    // In the case of an exception, this method will throw an AggregateException wrapping the original exception.
    return DoAsyncOperation().Result;
}

public string DoOperationBlocking6()
{
    // Bad - Blocking the thread that enters.
    // Bad - No effort has been made to prevent a present SynchonizationContext from becoming deadlocked.
    return DoAsyncOperation().GetAwaiter().GetResult();
}

public string DoOperationBlocking7()
{
    // Bad - Blocking the thread that enters.
    // Bad - No effort has been made to prevent a present SynchonizationContext from becoming deadlocked.
    var task = DoAsyncOperation();
    task.Wait();
    return task.GetAwaiter().GetResult();
}
```

**[⬆ back to top](#nashtech-c#-coding-convention)**

#### 7.5 Prefer `await` over `ContinueWith`

`Task` existed before the async/await keywords were introduced and as such provided ways to execute continuations without relying on the language. Although these methods are still valid to use, we generally recommend that you prefer `async`/`await` to using `ContinueWith`. `ContinueWith` also does not capture the `SynchronizationContext` and as a result is actually semantically different to `async`/`await`.

❌ **BAD** The example uses `ContinueWith` instead of `async`

```csharp
public Task<int> DoSomethingAsync()
{
    return CallDependencyAsync().ContinueWith(task =>
    {
        return task.Result + 1;
    });
}
```

:white_check_mark: **GOOD** This example uses the `await` keyword to get the result from `CallDependencyAsync`.

```csharp
public async Task<int> DoSomethingAsync()
{
    var result = await CallDependencyAsync();
    return result + 1;
}
```

**[⬆ back to top](#nashtech-c#-coding-convention)**

#### 7.6 Always dispose `CancellationTokenSource`(s) used for timeouts

`CancellationTokenSource` objects that are used for timeouts (are created with timers or uses the `CancelAfter` method), can put pressure on the timer queue if not disposed.

❌ **BAD** This example does not dispose the `CancellationTokenSource` and as a result the timer stays in the queue for 10 seconds after each request is made.

```csharp
public async Task<Stream> HttpClientAsyncWithCancellationBad()
{
    var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));

    using (var client = _httpClientFactory.CreateClient())
    {
        var response = await client.GetAsync("http://backend/api/1", cts.Token);
        return await response.Content.ReadAsStreamAsync();
    }
}
```

:white_check_mark: **GOOD** This example disposes the `CancellationTokenSource` and properly removes the timer from the queue.

```csharp
public async Task<Stream> HttpClientAsyncWithCancellationGood()
{
    using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10)))
    {
        using (var client = _httpClientFactory.CreateClient())
        {
            var response = await client.GetAsync("http://backend/api/1", cts.Token);
            return await response.Content.ReadAsStreamAsync();
        }
    }
}
```

**[⬆ back to top](#nashtech-c#-coding-convention)**

#### 7.7 Always flow `CancellationToken`(s) to APIs that take a `CancellationToken`

Cancellation is cooperative in .NET. Everything in the call-chain has to be explicitly passed the `CancellationToken` in order for it to work well. This means you need to explicitly pass the token into other APIs that take a token if you want cancellation to be most effective.

❌ **BAD** This example neglects to pass the `CancellationToken` to `Stream.ReadAsync` making the operation effectively not cancellable.

```csharp
public async Task<string> DoAsyncThing(CancellationToken cancellationToken = default)
{
   byte[] buffer = new byte[1024];
   // We forgot to pass flow cancellationToken to ReadAsync
   int read = await _stream.ReadAsync(buffer, 0, buffer.Length);
   return Encoding.UTF8.GetString(buffer, 0, read);
}
```

:white_check_mark: **GOOD** This example passes the `CancellationToken` into `Stream.ReadAsync`.

```csharp
public async Task<string> DoAsyncThing(CancellationToken cancellationToken = default)
{
   byte[] buffer = new byte[1024];
   // This properly flows cancellationToken to ReadAsync
   int read = await _stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);
   return Encoding.UTF8.GetString(buffer, 0, read);
}
```

**[⬆ back to top](#nashtech-c#-coding-convention)**

#### 7.8 Always call `FlushAsync` on `StreamWriter`(s) or `Stream`(s) before calling `Dispose`

When writing to a `Stream` or `StreamWriter`, even if the asynchronous overloads are used for writing, the underlying data might be buffered. When data is buffered, disposing the `Stream` or `StreamWriter` via the `Dispose` method will synchronously write/flush, which results in blocking the thread and could lead to thread-pool starvation. Either use the asynchronous `DisposeAsync` method (for example via `await using`) or call `FlushAsync` before calling `Dispose`.

:bulb:**NOTE: This is only problematic if the underlying subsystem does IO.**

❌ **BAD** This example ends up blocking the request by writing synchronously to the HTTP-response body.

```csharp
app.Run(async context =>
{
    // The implicit Dispose call will synchronously write to the response body
    using (var streamWriter = new StreamWriter(context.Response.Body))
    {
        await streamWriter.WriteAsync("Hello World");
    }
});
```

:white_check_mark: **GOOD** This example asynchronously flushes any buffered data while disposing the `StreamWriter`.

```csharp
app.Run(async context =>
{
    // The implicit AsyncDispose call will flush asynchronously
    await using (var streamWriter = new StreamWriter(context.Response.Body))
    {
        await streamWriter.WriteAsync("Hello World");
    }
});
```

:white_check_mark: **GOOD** This example asynchronously flushes any buffered data before disposing the `StreamWriter`.

```csharp
app.Run(async context =>
{
    using (var streamWriter = new StreamWriter(context.Response.Body))
    {
        await streamWriter.WriteAsync("Hello World");

        // Force an asynchronous flush
        await streamWriter.FlushAsync();
    }
});
```

**[⬆ back to top](#nashtech-c#-coding-convention)**

#### 7.9 Prefer `async`/`await` over directly returning `Task`

There are benefits to using the `async`/`await` keyword instead of directly returning the `Task`:
- Asynchronous and synchronous exceptions are normalized to always be asynchronous.
- The code is easier to modify (consider adding a `using`, for example).
- Diagnostics of asynchronous methods are easier (debugging hangs etc).
- Exceptions thrown will be automatically wrapped in the returned `Task` instead of surprising the caller with an actual exception.

❌ **BAD** This example directly returns the `Task` to the caller.

```csharp
public Task<int> DoSomethingAsync()
{
    return CallDependencyAsync();
}
```

:white_check_mark: **GOOD** This examples uses async/await instead of directly returning the Task.

```csharp
public async Task<int> DoSomethingAsync()
{
    return await CallDependencyAsync();
}
```

:bulb:**NOTE: There are performance considerations when using an async state machine over directly returning the `Task`. It's always faster to directly return the `Task` since it does less work but you end up changing the behavior and potentially losing some of the benefits of the async state machine.**

**[⬆ back to top](#nashtech-c#-coding-convention)**

## 8. Unit tests

The unit tests for the `Microsoft.Fruit` assembly live in the `Microsoft.Fruit.Tests` assembly.
In general there should be exactly one unit test assembly for each product runtime assembly.

Test class names end with Test and live in the same namespace as the class being tested. For example, the unit tests for the `Microsoft.Fruit.Banana` class would be in a `Microsoft.Fruit.BananaTest` class in the test assembly

Unit test method names must be descriptive about what is being tested, under what conditions, and what the expectations are. Pascal casing and underscores can be used to improve readability. The following test names are correct:

```csharp
PublicApiArgumentsShouldHaveNotNullAnnotation
Public_api_arguments_should_have_not_null_annotation
```

**[⬆ back to top](#nashtech-c#-coding-convention)**

## 9. Others

#### 9.1 Disposable object should be defined within using-block

Objects that implement IDisposable such as classes work with IO, Database or Network should be declared within using-block

```csharp
using (SqlConnection conn = new SqlConnection())
{

}
```

**[⬆ back to top](#nashtech-c#-coding-convention)**

## References

- https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/coding-conventions

- https://github.com/dotnet/runtime/blob/master/docs/coding-guidelines/coding-style.md

- https://github.com/dotnet/aspnetcore/wiki/Engineering-guidelines

- https://github.com/davidfowl/AspNetCoreDiagnosticScenarios

- https://github.com/thangchung/clean-code-dotnet

**[⬆ back to top](#nashtech-c#-coding-convention)**
