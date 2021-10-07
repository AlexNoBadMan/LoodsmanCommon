namespace LoodsmanCommon
{
    /// <summary>
    /// Режим чекаута
    /// </summary>
    public enum CheckOutMode
    {
        /// <summary>
        /// Блокировать головной объект. По умолчанию.
        /// </summary>
        Default = 0,
        /// <summary>
        /// Блокировать все привязанные объекты с полной разузловкой вниз.
        /// </summary>
        Through = 1
    }

    /// <summary>
    /// Уровни доступа
    /// </summary>
    public enum AccessLevel
    {
        /// <summary>
        /// Нет доступа
        /// </summary>
        No = 0,
        /// <summary>
        /// Чтение
        /// </summary>
        Read = 1,
        /// <summary>
        /// Чтение-запись
        /// </summary>
        Write = 2,
        /// <summary>
        /// Полный доступ
        /// </summary>
        Full = 3
    }

    /// <summary>
    /// Уровни блокировки
    /// </summary>
    public enum LockLevel
    {
        /// <summary>
        /// Не заблокирован
        /// </summary>
        No = 0,
        /// <summary>
        /// Заблокирован текущим пользователем
        /// </summary>
        Self = 1,
        /// <summary>
        /// Заблокирован другим пользователем
        /// </summary>
        Other = 2
    }

    /// <summary>
    /// Типы атрибутов
    /// </summary>
    public enum AttributeType
    {
        /// <summary>
        /// Строка
        /// </summary>
        String = 0,
        /// <summary>
        /// Целое
        /// </summary>
        Int = 1,
        /// <summary>
        /// Вещественное
        /// </summary>
        Double = 2,
        /// <summary>
        /// Дата
        /// </summary>
        DateTime = 3,
        /// <summary>
        /// Текст
        /// </summary>
        Text = 5,
        /// <summary>
        /// Изображение
        /// </summary>
        Image = 6
    }
}
