# MvvmBuilder

[![NuGet version](https://img.shields.io/nuget/v/MvvmBuilder.svg?label=NuGet)](https://www.nuget.org/packages/MvvmBuilder)
[![.NET](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/)

**MvvmBuilder** is a lightweight, zero-bloat C# library that provides essential MVVM building blocks (`NotifyBase`, `RelayCommand`, automatic property storage, and change callbacks) for WPF, desktop, and console applications without pulling in heavy UI frameworks.

---

## Features
- **`NotifyBase`** — clean base class implementing `INotifyPropertyChanged` with built-in backing dictionary (`GetProperty<T>()` / `SetProperty<T>()`) as well as ref-based property updates.
- **`RelayCommand` & `RelayCommand<T>`** — lightweight `ICommand` implementations with `CanExecute` support.
- Minimal footprint and zero external UI dependencies.

## Installation

```bash
dotnet add package MvvmBuilder
```

## Quick Start

1. ViewModel with NotifyBase and RelayCommand
code C#

```csharp
using System.Windows.Input;
using MvvmBuilder.Commands;
using MvvmBuilder.Notifies;

public class LoginViewModel : NotifyBase
{
    private ICommand? _loginCommand;

    public string Username
    {
        get => GetProperty<string>();
        set => SetProperty(value);
    }

    public string StatusMessage
    {
        get => GetProperty<string>();
        set => SetProperty(value, startValue: "Ready");
    }

    public ICommand LoginCommand => _loginCommand ??= new RelayCommand(
        execute: () => StatusMessage = $"Welcome, {Username}!",
        canExecute: () => !string.IsNullOrWhiteSpace(Username)
    );
}
```

2. Property Change Callbacks
code C#

```csharp
public string SelectedLanguage
{
    get => GetProperty<string>();
    set => SetProperty(value, onChanged: lang => ApplyCulture(lang), startValue: "en-US");
}
```
