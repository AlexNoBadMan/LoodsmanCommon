namespace LoodsmanCommon
{
    /// <summary>
    /// Режим возврата списока атрибутов 
    /// </summary>
    public enum GetAttributeListMode
    {
        /// <summary>
        /// Возвращать все атрибуты (обычные и служебные).
        /// </summary>
        All = 0,
        /// <summary>
        /// Возвращать только обычные атрибуты.
        /// </summary>
        OnlyRegular = 1,
        /// <summary>
        /// Возвращать только служебные атрибуты.
        /// </summary>
        OnlyService = 2
    }
    /// <summary>
    /// Направление связи
    /// </summary>
    public enum LinkDirection
    {
        /// <summary>
        /// Прямая связь
        /// </summary>
        Forward = 1,
        /// <summary>
        /// Обратная связь
        /// </summary>
        Backward = -1,
        /// <summary>
        /// Прямая и обратная связь
        /// </summary>
        ForwardAndBackward = 0
    }


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
    public enum AttributeType : short
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
