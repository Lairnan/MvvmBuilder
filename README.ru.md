# MvvmBuilder

[![NuGet version](https://img.shields.io/nuget/v/MvvmBuilder.svg?label=NuGet)](https://www.nuget.org/packages/MvvmBuilder)
[![.NET](https://img.shields.io/badge/.NET-3.1-blue.svg)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/github/license/Lairnan/MvvmBuilder)](LICENSE)

[English](./README.md)

---

**MvvmBuilder** — это лёгкий и удобный помощник для упрощения реализации `INotifyPropertyChanged`, изначально предназначенный для WPF-приложений с использованием паттерна MVVM.  
Также может применяться в консольных и других C# приложениях для уведомлений об изменении свойств.

---

## Возможности

- Упрощает работу с `INotifyPropertyChanged` через `GetProperty` и `SetProperty`
- Поддерживает **подписку на изменения свойств** с помощью `SubscribeOnChanges` и `UnsubscribeOnChanges`
- Снижает количество шаблонного кода в ViewModel
- Подходит для WPF, консольных и других C# приложений
- Минималистичный и легко интегрируемый

---

## Установка

Можно установить через NuGet:

```bash
dotnet add package MvvmBuilder
```

Или через менеджер пакетов:

```bash
Install-Package MvvmBuilder
```

---

## Примеры использования

### Базовая ViewModel

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

### Подписка на изменения свойства

```csharp
var viewModel = new UserViewModel() { Name = "Иван" };

NotifyBase.SubscribeOnChanges(
    nameof(UserViewModel.Name),
    viewModel,
    (oldValue, newValue) => 
    {
        Console.WriteLine($"Имя изменилось с {oldValue} на {newValue}");
    }
);

viewModel.Name = "Джон";
// Вывод: Имя изменилось с Иван на Джон

NotifyBase.UnsubscribeOnChanges(
    nameof(UserViewModel.Name),
    viewModel
);
```

---

## Совместимость

- ✅ .NET 3.1+
- ✅ WPF приложения
- ✅ Консольные приложения
- ✅ Другие C# проекты

---

## Лицензия

Проект лицензирован под лицензией MIT.  
Подробности в файле [LICENSE](./LICENSE).
