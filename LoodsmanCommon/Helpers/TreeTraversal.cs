using System;
using System.Collections.Generic;
using System.Linq;

namespace LoodsmanCommon
{
  /// <summary> Статические вспомогательные методы для обхода деревьев. </summary>
  public static class TreeTraversal
  {
    /// <summary> Преобразует древовидную структуру данных в плоский список, обходя ее в предварительном порядке. </summary>
    /// <typeparam name="T"> Тип элемента. </typeparam>
    /// <param name="root"> Корневой элемент. </param>
    /// <param name="recursion"> Функция, которая получает дочерние элементы элемента. </param>
    /// <returns> Итератор, перебирающий древовидную структуру в предварительном порядке. </returns>
    public static IEnumerable<T> PreOrder<T>(T root, Func<T, IEnumerable<T>> recursion)
    {
      return PreOrder(Enumerable.Repeat(root, 1), recursion);
    }

    /// <summary> Преобразует древовидную структуру данных в плоский список, обходя ее в предварительном порядке. </summary>
    /// <typeparam name="T"> Тип элемента. </typeparam>
    /// <param name="input"> Корневые элементы. </param>
    /// <param name="recursion"> Функция, которая получает дочерние элементы элемента. </param>
    /// <returns> Итератор, перебирающий древовидную структуру в предварительном порядке. </returns>
    public static IEnumerable<T> PreOrder<T>(IEnumerable<T> input, Func<T, IEnumerable<T>> recursion)
    {
      var stack = new Stack<IEnumerator<T>>();
      try
      {
        stack.Push(input.GetEnumerator());
        while (stack.Count > 0)
        {
          while (stack.Peek().MoveNext())
          {
            var element = stack.Peek().Current;
            yield return element;
            var children = recursion(element);
            if (children != null)
              stack.Push(children.GetEnumerator());
          }

          stack.Pop().Dispose();
        }
      }
      finally
      {
        while (stack.Count > 0)
        {
          stack.Pop().Dispose();
        }
      }
    }
  }
}
