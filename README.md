Рефакторинг системы приветствия посетителей для решения нескольких проблем и улучшения качества кода:

1. **Инъекция зависимостей**: Заменил прямое использование DateTime.Now на IDateTimeProvider для улучшения тестируемости и отвязки от внешних данных.
2. **Потокобезопасность**: Использовал ConcurrentDictionary для управления посетителями, обеспечивая потокобезопасные операции.
3. **Разделение логики и вывода**: Разделил бизнес-логику и вывод в консоль, следуя принципу единственной ответственности (SRP).
4. **Интерполяция строк**: Улучшил читаемость, заменив конкатенацию строк на интерполяцию строк.
5. **Очистка кода**: Убрал лишние пробелы и улучшил общую читаемость кода.
6. **Единые соглашения по именованию**: Избегал использования подчёркиваний в названиях переменных для более чистого стиля кода.
7. **Рефакторинг вывода в консоль**: Перенёс логику из тернарных операторов в отдельные методы или переменные для лучшей читаемости.

Хотя в идеале код должен быть организован по отдельным папкам и классам, здесь он оставлен простым для удобства проверки. Кроме того, хотя класс Greeter по-прежнему отвечает за получение текущего времени и форматирование строк, это допустимо для простоты данной программы.

### Внесённые изменения:
- **VisitorService**: Управляет списком посетителей с использованием ConcurrentDictionary.
- **Greeter**: Генерирует приветственные сообщения на основе текущего времени и деталей посетителя, используя IDateTimeProvider для зависимости от времени.
- **IDateTimeProvider и SystemDateTimeProvider**: Предоставляют текущую дату и время, способствуя инъекции зависимостей.
- **Основная программа**: Демонстрирует использование VisitorService и Greeter для генерации и вывода пригласительных сообщений.

Этот рефакторинг соответствует принципам SOLID, улучшает потокобезопасность и повышает читаемость кода.
