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
