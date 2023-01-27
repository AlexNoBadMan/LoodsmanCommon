using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;

namespace LoodsmanCommon
{
    /// <summary>
    /// Расширения DataTable для удобного использования. 
    /// </summary>
    public static class DataTableExtensions
    {
        /// <summary>
        /// Проецирует каждый элемент последовательности в новую форму.
        /// </summary>
        /// <typeparam name="T">Тип значения, возвращаемый <paramref name="selector"/>.</typeparam>
        /// <param name="dataTable">Таблица данных.</param>
        /// <param name="selector">Функция преобразования, применяемая к каждому элементу.</param>
        /// <returns><see cref="IEnumerable{T}"/> Элементы которой получены в результате вызова функции преобразования к каждому элементу source.</returns>
        public static IEnumerable<T> Select<T>(this DataTable dataTable, Func<DataRow, T> selector)
        {
            for (int i = 0; i < dataTable.Rows.Count; i++)
                yield return selector(dataTable.Rows[i]);
        }

        /// <summary>
        /// Выполняет фильтрацию последовательности значений на основе заданного предиката.
        /// </summary>
        /// <param name="dataTable">Таблица данных.</param>
        /// <param name="predicate">Функция для проверки каждого элемента исходной последовательности условие.</param>
        /// <returns><see cref="IEnumerable{T}"/> Содержащий элементы из входной последовательности, которые удовлетворяют условию.</returns>
        public static IEnumerable<DataRow> Where(this DataTable dataTable, Func<DataRow, bool> predicate)
        {
            for (int i = 0; i < dataTable.Rows.Count; i++)
                if (predicate(dataTable.Rows[i]))
                    yield return dataTable.Rows[i];
        }

        /// <summary>
        /// Возвращает первый элемент последовательности, удовлетворяющий указанному условию, или значение по умолчанию, если ни одного такого элемента не найдено.
        /// </summary>
        /// <param name="dataTable">Таблица данных.</param>
        /// <param name="predicate">Функция для проверки каждого элемента исходной последовательности условие.</param>
        /// <returns>Возвращает первый <see cref="DataRow"/>, удовлетворяющий указанному условию (<paramref name="predicate"/>) или значение по умолчанию.</returns>
        public static DataRow FirstOrDefault(this DataTable dataTable, Func<DataRow, bool> predicate)
        {
            for (int i = 0; i < dataTable.Rows.Count; i++)
                if (predicate(dataTable.Rows[i]))
                    return dataTable.Rows[i];
            
            return null;
        }

        /// <summary>
        /// Возвращает первый элемент последовательности, удовлетворяющий указанному условию, или значение по умолчанию, если ни одного такого элемента не найдено.
        /// </summary>
        /// <param name="dataTable">Таблица данных.</param>
        /// <param name="predicate">Функция для проверки каждого элемента исходной последовательности условие.</param>
        /// <param name="columnName">Наименование заголовка столбца, значение которого необходимо получить.</param>
        /// <returns>Возвращает значение указанного столбца (<paramref name="columnName"/>), удовлетворяющий указанному условию (<paramref name="predicate"/>) или значение по умолчанию.</returns>
        public static object FirstOrDefault(this DataTable dataTable, Func<DataRow, bool> predicate, string columnName)
        {
            return dataTable.FirstOrDefault(predicate)?[columnName];
        }
    }
}
