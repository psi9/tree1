Консольное приложение, которые выводит содержание текущего и вложенных каталогов в виде дерева.

Использование: из командной строки операционной системы запустить исполняемый файл с необходимыми опциями.

Опции при запуске:
* -d или --depth						задать глубину вложенности;
* -s или --size							показывать размер объектов;
* -h или --human-readable				показывать размер объектов в удобном для восприятия виде;
* -r или --reverse						обратное направление сортировки;
* --sort {name,size,creation,change}	тип сортировки элементов дерева: алфавитный (по умолчанию), размер, дата создания, дата изменения;
* -? или --help							справка по использованию

Сборка: 
* в каталоге с файлом проекта выполнить `dotnet publish -o "выходной каталог"`