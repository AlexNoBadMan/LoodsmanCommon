using Ascon.Plm.Loodsman.PluginSDK;
using LoodsmanCommon;
using System;
using System.Data;
using System.Linq;
using System.Windows;

namespace LoodsmanCommon.Demo
{
  [LoodsmanPlugin]
  public class DemoPlugin : LoodsmanWPFNetPluginBase
  {
    public override void BindMenu(IMenuDefinition menu)
    {
      menu.AddMenuItem("Тест демо#В работу(SelectedObjectCheckOut), удалить, отказ", Command1, CheckCommand);
      menu.AddMenuItem("Тест демо#В работу(Empty CheckOut AddToCheckOut), удалить, отказ", Command2, CheckCommand);
      menu.AddMenuItem("Тест демо#В работу(No empty ChekOut), удалить, отказ", Command3, CheckCommand);
      menu.AddMenuItem("Тест демо#Преобразование единиц измерения", Command4, CheckCommand);
      menu.AddMenuItem("Тест демо#Получение информации об организационной структуре", Command5, FreeCheckCommand);
    }

    protected override bool CheckCommand(INetPluginCall iNetPC)
    {
      if (_proxy is null && iNetPC != null) //метод OnConnectToDb не срабатывает при первом добавлении команды на панель инструментов.
        PluginInit(iNetPC);

      return base.CheckCommand(iNetPC);
    }

    private void Command1(INetPluginCall iNetPC)
    {
      try
      {
        _proxy.InitNetPluginCall(iNetPC);
        _proxy.SelectedObjectCheckOut();
        _proxy.KillVersion(_proxy.SelectedObject.Id);
        _proxy.CancelCheckOut();
      }
      catch (Exception ex)
      {
        _proxy.CancelCheckOut();
        MessageBox.Show(ex.Message);
      }
    }

    private void Command2(INetPluginCall iNetPC)
    {
      try
      {
        _proxy.InitNetPluginCall(iNetPC);
        _proxy.CheckOut();
        _proxy.ConnectToCheckOut();
        var testObjectId = 1012;
        _proxy.AddToCheckOut(testObjectId);
        _proxy.KillVersion(testObjectId);
        _proxy.CancelCheckOut();
      }
      catch (Exception ex)
      {
        _proxy.CancelCheckOut();
        MessageBox.Show(ex.Message);
      }
    }

    private void Command3(INetPluginCall iNetPC)
    {
      try
      {
        _proxy.InitNetPluginCall(iNetPC);
        _proxy.CheckOut("Сборочная единица", "АГ52.289.047", "2");
        _proxy.ConnectToCheckOut();
        var testObjectId = 1012;
        _proxy.KillVersion(testObjectId);
        _proxy.CancelCheckOut();
      }
      catch (Exception ex)
      {
        _proxy.CancelCheckOut();
        MessageBox.Show(ex.Message);
      }
    }

    private void Command4(INetPluginCall iNetPC)
    {
      _proxy.InitNetPluginCall(iNetPC);
      var massa = _meta.Measures["Масса"];
      var valueUnit = massa.Units.Values.FirstOrDefault(x => x.IsBase);
      var convertValueUnit = massa.Units["г"];
      var value = 5;
      var convertValue = _proxy.ConverseValue(value, convertValueUnit, valueUnit);
      MessageBox.Show($"Исходное значение: {value} {convertValueUnit.Name} \nПреобразованное значение: {convertValue} {valueUnit.Name}");
    }

    private void Command5(INetPluginCall iNetPC)
    {
      _proxy.InitNetPluginCall(iNetPC);
      var mainDepartaments = _meta.OrganisationUnits.Values.Where(x => x.Kind == OrganisationUnitKind.MainDepartment).Cast<LMainDepartment>().OrderBy(x => x.Id).ToArray();
      var orgInfo = $"Головных орг. единиц: {mainDepartaments.Length}\n\n";
      for (var i = 0; i < mainDepartaments.Length; i++)
      {
        var mainDepartament = mainDepartaments[i];
        var departamentsCount = mainDepartament.Descendants().Count(x => x.Kind == OrganisationUnitKind.Department);
        var positionsCount = mainDepartament.Descendants().Count(x => x.Kind == OrganisationUnitKind.Position);
        orgInfo = $"{orgInfo}Наименование: {mainDepartament.Name}\nПодразделений: {departamentsCount}\nДолжностей: {positionsCount}\n\n";
      }
      MessageBox.Show(orgInfo, "Информация об организационной структуре");
    }

    private bool FreeCheckCommand(INetPluginCall iNetPC)
    {
      if (_proxy is null && iNetPC != null) //метод OnConnectToDb не срабатывает при первом добавлении команды на панель инструментов.
        PluginInit(iNetPC);

      return iNetPC != null;
    }
  }
}
