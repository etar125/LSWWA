**LSWWA** - это [сценарный](https://ru.wikipedia.org/wiki/%D0%A1%D1%86%D0%B5%D0%BD%D0%B0%D1%80%D0%BD%D1%8B%D0%B9_%D1%8F%D0%B7%D1%8B%D0%BA) [эзотерический](https://ru.wikipedia.org/wiki/%D0%AD%D0%B7%D0%BE%D1%82%D0%B5%D1%80%D0%B8%D1%87%D0%B5%D1%81%D0%BA%D0%B8%D0%B9_%D1%8F%D0%B7%D1%8B%D0%BA_%D0%BF%D1%80%D0%BE%D0%B3%D1%80%D0%B0%D0%BC%D0%BC%D0%B8%D1%80%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D1%8F) [язык программирования](https://ru.wikipedia.org/wiki/%D0%AF%D0%B7%D1%8B%D0%BA_%D0%BF%D1%80%D0%BE%D0%B3%D1%80%D0%B0%D0%BC%D0%BC%D0%B8%D1%80%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D1%8F) написанный на [C#](https://ru.wikipedia.org/wiki/C_Sharp).  
### Нумерация
Первое число - _основная версия_. То есть версия, в которой добавлено всё, что планировалось.  
Второе число - _дополнительная версия_. То есть версия, в которой добавлена часть того, что планировалось, или просто среднее/большое исправление.  
Третье число - _версия сборки_. Вот его структура: ``ДЕНЬ-МЕСЯЦ-ГОД-ЧАС-МИНУТА``(можно округлить минуты, без ``-``)  
Пример: ``0.5.126231550``.  
### Библиотеки
#### Создание своей библиотеки
Есть поддержка собственных библиотек.  
Создаём C# проект .NET Framework 4.0 -> Библиотека классов.  
**ОБЯЗАТЕЛЬНО** называем класс _Program_(файл в том числе) и делаем его публичным.  
Дальше создаём такой метод:  
```cs
public static void Do(string func, string arg, string file, List<string> code, Dictionary<string, string> vars, int index, string version) {
    // Тут ваш код
}
```
Всё. В нём пишите проверки и всё такое.  
#### Подключение и использование библиотеки.
Помещаем библиотеку в директорию с LSWWA и вашим файлом.  
В файле с кодом пишем:  
```
begin
   include mydll ; Тут пишите название вашей библиотеки
   mydll.МЕТОД аргументы,еслиесть
   pause
```
Всё. Запускаете и смотрите результат.    
***Библиотеки были проверены. Всё работает.***

***

Написано для версии _0.5.126231551_.
