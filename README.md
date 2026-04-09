# Склад стройматериалов (ASP.NET Core MVC)

![Build Status](https://github.com/gstu/safonenko/actions/workflows/build.yml/badge.svg)

Учебное веб-приложение для учета складских операций в предметной области стройматериалов.

## Реализовано по заданию

- ASP.NET Core MVC приложение с использованием DI.
- EF Core + SQL Server (`WarehouseContext`) и миграция `InitialCreate`.
- Модели БД:
  - `Product` (Артикул, Название, Единица измерения, Вес, Мин. запас)
  - `Category`
  - `StorageLocation`
  - `Supplier`
  - `StockMovement` (дата, тип операции, количество, документ-основание)
- Middleware `DatabaseSeedMiddleware` для инициализации тестовых данных.
- Контроллеры и представления для нескольких таблиц:
  - `ProductsController`
  - `CategoriesController`
  - `StockMovementsController`
- Для таблицы на стороне "многие" (`StockMovement`) отображаются связанные смысловые значения (`Product.Name`, `Supplier.Name`, место хранения), а не только FK.
- Дополнительные функции:
  - поиск товара по артикулу или названию;
  - отчет об остатках;
  - список товаров ниже минимального запаса.
- Кэширование страниц через `ResponseCache` с длительностью **268 секунд** (`2*N+240`, где `N=14`).

## Стек

- .NET 8
- ASP.NET Core MVC
- Entity Framework Core 8
- SQL Server (LocalDB/Express)

## Структура проекта

- `Data/WarehouseContext.cs` - контекст БД
- `Models/*` - сущности предметной области
- `Middleware/DatabaseSeedMiddleware.cs` - заполнение тестовыми данными
- `Controllers/*` - логика выборки и отображения
- `Views/*` - Razor представления
- `Migrations/*` - миграция `InitialCreate`

## Запуск проекта локально

1. Установить .NET SDK 8 и SQL Server LocalDB (или SQL Server Express).
2. Обновить строку подключения в `appsettings.json` при необходимости.
3. Выполнить миграции и запуск:

```bash
dotnet restore
dotnet ef database update
dotnet run
```

4. Открыть приложение по адресу из `launchSettings.json`.

## Проверка кэширования через DevTools

1. Открыть, например, страницу `/Products/Balances`.
2. В Chrome/Firefox открыть **DevTools -> Network**.
3. Выполнить первый запрос и зафиксировать время.
4. Обновить страницу повторно и сравнить время ответа/статус.
5. Проверить заголовки `Cache-Control` и `Age` (при наличии), подтверждающие кэширование `268` секунд.

## GitHub Actions

Workflow `build.yml` запускается при `push` и `pull_request` и собирает проект на двух платформах:

- `windows-latest`
- `ubuntu-latest`

> Если репозиторий опубликован не как `gstu/safonenko`, замените ссылку badge в начале файла на актуальную.
