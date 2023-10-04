using System;
using System.IO;
using System.Reflection;

namespace LoodsmanCommon
{
  /// <summary> Облегчает поиск библиотек. </summary>
  public class AssemblyResolver
  {
    /// <summary> Метод инициирует активацию стандартного Resolver из библиотеки Ascon.Integration. </summary>
    /// <remarks> Позволяет работать с библиотеками Полином без копирования в папку сборки. </remarks>
    public static void ComplexIntegrationForceResolve()
    {
      const string name = "Ascon.Integration.AuthenticationManager";
      var t = Type.GetTypeFromProgID(name);
      if (t == null)
        throw new Exception($"Класс не найден \"{name}\"");

      var c = Activator.CreateInstance(t);
    }

    /// <summary> Метод инициирует поиск необходимых сборок в папке с приложением, в случае когда разрешение сборки завершается неудачей. </summary>
    public static void CurrentAssemblyForceResolve()
    {
      AppDomain.CurrentDomain.AssemblyResolve -= OnCurrentDomainAssemblyResolve;
      AppDomain.CurrentDomain.AssemblyResolve += OnCurrentDomainAssemblyResolve;
    }

    /// <summary> Вызывается при необходимости найти сборку. </summary>
    /// <param name="sender"> Источник события. </param>
    /// <param name="args"> Параметры события. </param>
    /// <returns> Найденную сборку или <c>null</c>. </returns>
    private static Assembly OnCurrentDomainAssemblyResolve(object sender, ResolveEventArgs args)
    {
      try
      {
        var name = args.Name.Split(',')[0];
        var exts = new string[] { ".dll", ".exe" };
        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        foreach (var ext in exts)
        {
          var fullname = Path.Combine(path, $"{name}{ext}");
          if (File.Exists(fullname))
            return Assembly.LoadFile(fullname);
        }
      }
      catch (Exception ex)
      {
        System.Windows.MessageBox.Show($"{ex.Message}\n{ex.StackTrace}");
      }
      return null;
    }
  }
}
