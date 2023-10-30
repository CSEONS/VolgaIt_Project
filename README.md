# Инструкция по использованию репозитория

Для запуска проекта необходимо выполнить следующие шаги:

1. Установить dotnet 7 [отсюда](https://dotnet.microsoft.com/en-us/download/dotnet/7.0).

2. Перейти в папку `./VolgaIt`.

3. Выполнить команду `dotnet tool install --global dotnet-ef`, чтобы установить dotnet-ef.

4. Открыть файл `appsettings.json` и изменить данные `ConnectionString` под свои.

5. Выполнить команду `dotnet-ef database drop` для удаления базы данных (если она существует), затем выполнить команду `dotnet-ef database update`.
