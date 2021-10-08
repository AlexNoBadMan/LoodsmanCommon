using Ascon.Plm.Loodsman.PluginSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LoodsmanCommon.Demo
{
    [LoodsmanPlugin]
    public class DemoPlugin : LoodsmanWPFPluginBase
    {
        public override void BindMenu(IMenuDefinition menu)
        {
            menu.AddMenuItem("Тест демо#В работу(SelectedObjectCheckOut), удалить, отказ", Command1, CheckCommand);
            menu.AddMenuItem("Тест демо#В работу(Empty CheckOut AddToCheckOut), удалить, отказ", Command2, CheckCommand);
            menu.AddMenuItem("Тест демо#В работу(No empty ChekOut), удалить, отказ", Command3, CheckCommand);
        }

        protected override bool CheckCommand(INetPluginCall iNetPC)
        {
            if (_loodsmanProxy is null && iNetPC != null) //метод OnConnectToDb не срабатывает при первом добавлении команды на панель инструментов.
                PluginInit(iNetPC);

            return base.CheckCommand(iNetPC);
        }

        private void Command1(INetPluginCall iNetPC)
        {
            try
            {
                _loodsmanProxy.InitNetPluginCall(iNetPC);
                _loodsmanProxy.SelectedObjectCheckOut();
                _loodsmanProxy.KillVersion(_loodsmanProxy.SelectedObject.Id);
                _loodsmanProxy.CancelCheckOut();
            }
            catch (Exception ex)
            {
                _loodsmanProxy.CancelCheckOut();
                MessageBox.Show(ex.Message);
            }
        }

        private void Command2(INetPluginCall iNetPC)
        {
            try
            {
                _loodsmanProxy.InitNetPluginCall(iNetPC);
                _loodsmanProxy.CheckOut();
                _loodsmanProxy.ConnectToCheckOut();
                var testObjectId = 1012;
                _loodsmanProxy.AddToCheckOut(testObjectId);
                _loodsmanProxy.KillVersion(testObjectId);
                _loodsmanProxy.CancelCheckOut();
            }
            catch (Exception ex)
            {
                _loodsmanProxy.CancelCheckOut();
                MessageBox.Show(ex.Message);
            }
        }

        private void Command3(INetPluginCall iNetPC)
        {
            try
            {
                _loodsmanProxy.InitNetPluginCall(iNetPC);
                _loodsmanProxy.CheckOut("Сборочная единица", "АГ52.289.047", "2");
                _loodsmanProxy.ConnectToCheckOut();
                var testObjectId = 1012;
                _loodsmanProxy.KillVersion(testObjectId);
                _loodsmanProxy.CancelCheckOut();
            }
            catch (Exception ex)
            {
                _loodsmanProxy.CancelCheckOut();
                MessageBox.Show(ex.Message);
            }
        }
    }
}
