# SampleApp

1.Изначально дан шаблон с хорошей структурой разделения и
bootstrap из коробки. То есть фреймворк дает каркас.

2. Начинаем с построения архитектуры приложения и
анализа предметной области, построения UML-диаграмм

2. Commit: create clear arheaticture

3. Создаем Page для регистрации и авторизации

4. Подключение кастомного custom.scss. Тут надо подключиться пакет AspCore.Sass.Compiler

Program.cs:
```Csharp
#if DEBUG
builder.Services.AddSassCompiler();
#endif
```
appsettings.json:

```json
  "SassCompiler": {
    "SourceFolder": "wwwroot/scss",
    "TargetFolder": "wwwroot/css",
    "Arguments": "--style=compressed",
    "GenerateScopedCss": true,
    "ScopedCssFolders": [ "Views", "Pages", "Shared", "Components" ],
    "IncludePaths": []
  }
```

4. Создаем новую ветку и переходим в нее.



1. Подход CodeFirst более предпочтителен
2. Надо хорошо уметь верстать, работать с html, css, js
3. Проанализировать предметную область, исходные данные,  построить необходимые диаграммы
4. Работа с ветками по git-flow
5. Построить архитектуру с разделением на слои, трехзвенная архитектура, на второй бд можно показать подход DatabaseFirst
6. Сначала начать с локального хранилица, потом локальной базы данных, потом паттерн репозиторий, интерфейсы, подключение бд
7. Клиентов делаем после домена и сервиса в презентационном слое