# MvvmBuilder

[![NuGet version](https://img.shields.io/nuget/v/MvvmBuilder.svg?label=NuGet)](https://www.nuget.org/packages/MvvmBuilder)
[![.NET](https://img.shields.io/badge/.NET-3.1-blue.svg)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/github/license/Lairnan/MvvmBuilder)](LICENSE)

[Русский](./README.ru.md)

---

**MvvmBuilder** is a lightweight and flexible helper for simplifying the implementation of `INotifyPropertyChanged`, designed primarily for WPF applications using the MVVM pattern.  
It can also be used in console or any other C# applications where property change notifications are needed.

---

## Features

- Simplifies `INotifyPropertyChanged` usage with `GetProperty` and `SetProperty`
- Provides **property change subscription** through `SubscribeOnChanges` and `UnsubscribeOnChanges`
- Reduces boilerplate in ViewModels
- Suitable for WPF, console, and other C# applications
- Minimalistic and easy to integrate

---

## Installation

You can install via NuGet:

```bash
dotnet add package MvvmBuilder
```

Or via Package Manager:

```bash
Install-Package MvvmBuilder
```

---

## Usage Examples

### Basic ViewModel

```csharp
using MvvmBuilder;

public class UserViewModel : NotifyBase
{
    public string Name
    {
        get => GetProperty<string>();
        set => SetProperty(value);
    }

    public int Age
    {
        get => GetProperty<int>();
        set => SetProperty(value);
    }
}
```

---

### Subscribing to Property Changes

```csharp
var viewModel = new UserViewModel() { Name = "Ivan" };

NotifyBase.SubscribeOnChanges(
    nameof(UserViewModel.Name),
    viewModel,
    (oldValue, newValue) => 
    {
        Console.WriteLine($"Name changed from {oldValue} to {newValue}");
    }
);

viewModel.Name = "John";
// Console output: Name changed from Ivan to John

NotifyBase.UnsubscribeOnChanges(
    nameof(UserViewModel.Name),
    viewModel
);
```

---

## Compatibility

- ✅ .NET 6.0+
- ✅ WPF applications
- ✅ Console applications
- ✅ Other C# applications

---

## License

This project is licensed under the MIT License.  
See the [LICENSE](./LICENSE) file for details.
