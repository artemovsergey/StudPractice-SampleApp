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

5. Проанализировать предметную область, исходные данные.
6. Построить необходимые диаграммы

7. Построить архитектуру с разделением на слои, трехзвенная архитектура,
8. на второй бд можно показать подход DatabaseFirst
9. Сначала начать с локального хранилица, потом локальной базы данных, потом паттерн репозиторий, интерфейсы, подключение бд
10. Клиентов делаем после домена и сервиса в презентационном слое


11. Создали проект Razor Page в Presentation Layer

# _Layout.cshtml

```html
<!DOCTYPE html>
<html>
<head>
<title>Home | Ruby on Rails Tutorial Sample App</title>
</head>
<body>
<h1>Sample App</h1>
<p>
This is the home page for the
<a href="http://www.railstutorial.org/">Ruby on Rails
Tutorial</a> sample application.
</p>
</body>
</html>

```

# Подключение Bootstrap

Далее подключим файлы bootstrap для придания дизайна элементам на
страницах. Для этого спросите у преподавателя готовые папки css и js,
которые вы поместите в корень своего приложения. Ниже представлен код
подключения, который вы помещаете в теге ```<head>```.

```html
<link href="css/bootstrap.min.css" rel="stylesheet">
 <link href="css/bootstrap-theme.min.css" rel="stylesheet">
 <link href="css/theme.css" rel="stylesheet">
```

# Header

```html
<header class="navbar navbar-fixed-top navbar-inverse">
 <div class="container">
 <a href=”#” id="logo" >sample app</a>
 <nav>
 <ul class="nav navbar-nav navbar-right">
 <li><a href=”#”>Home</a></li>
 <li><a href=”#”>Help</a></li>
 <li><a href=”#”>About</a></li>
 </ul>
 </nav>
 </div>
</header>
```

## Index

```html
<div class="center jumbotron">
 <h1>Welcome to the Sample App</h1>
 <h2>
 This is the home page for the
 <a href="#"> Tutorial</a> sample application.
 </h2>
 <a href = “#” class = "btn btn-lg btn-primary">Sign up
now!</a>
</div>
```

Создаем файл css в папке css и подключим его под названием
custom.scss (добавляем в него стили ). Вначале добавляем общие стили. 

```css
body {
padding-top: 60px;
}
section {
overflow: auto;
}
textarea {
resize: vertical;
}
.center {
text-align: center;
}
.center h1 {
margin-bottom: 10px;
}

```

Добавим в custom.scss дополнительные правила CSS для улучшения оформления
текста.

```css
h1, h2, h3, h4, h5, h6 {
line-height: 1;
}
h1 {
font-size: 3em;
letter-spacing: -2px;
margin-bottom: 30px;
text-align: center;
}
h2 {
font-size: 1.2em;
letter-spacing: -1px;
margin-bottom: 30px;
text-align: center;
font-weight: normal;
color: #777;
}
p {
font-size: 1.1em;
line-height: 1.7em;
}

```

Делаем в custom.scss правила для оформления логотипа (в результате лого
меняется). 

```css
#logo {
float: left;
margin-right: 10px;
font-size: 1.7em;
color: #fff;
text-transform: uppercase;
letter-spacing: -1px;
padding-top: 9px;
font-weight: bold;
}
#logo:hover {
color: #fff;
text-decoration: none;
}
```

Далее создаем footer. Он будет подключаться перед закрывающимся тегом
```<body> ```


```html
<footer class="footer">
 <nav>
 <ul>
 <li><a href=”#”>About</a></li>
 <li><a href=”#”>Help</a></li>
 </ul>
 </nav>
</footer>
```

Создадим отдельные файлы header и footer. Теперь мы можем вынести
код в эти файлы и подключать их для каждого файла нашего приложения
отдельно. Ниже приводится код подключения. Подключение header.php и
footer.php сделать в каждом файле приложения! 


Дополнительные правила CSS для оформления подвала сайта (footer).

```css
footer {
margin-top: 45px;
padding-top: 5px;
border-top: 1px solid #eaeaea;
color: #777;
}
footer a {
color: #555;
}
footer a:hover {
color: #222;
}
footer small {
float: left;
}
footer ul {
float: right;
list-style: none;
}
footer ul li {
float: left;
margin-left: 15px;
}

```


## Результат, который должен у вас получиться 


# Задания
- Проставить ссылку на главную страницу по лого.
- Подключаем частичные представления для header и footer


**Коммит**: настройка окружения