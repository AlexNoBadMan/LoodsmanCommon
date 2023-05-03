using Ascon.Plm.Loodsman.PluginSDK;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Linq;

namespace LoodsmanCommon
{
  /// <summary>
  /// Прокси объект для удобного взаимодействия с ЛОЦМАН:PLM.
  /// </summary>
  public interface ILoodsmanProxy
  {
    /// <summary>
    /// Интерфейс, передаваемый в подключаемые модули ЛОЦМАН:PLM.
    /// </summary>
    INetPluginCall INetPC { get; }

    /// <summary>
    /// Метаданные конфигурации базы Лоцман.
    /// </summary>
    ILoodsmanMeta Meta { get; }

    /// <summary>
    /// Выбранный объект в дереве Лоцман.
    /// </summary>
    ILObject SelectedObject { get; }

    /// <summary>
    /// Выбранные объекты в дереве Лоцман.
    /// </summary>
    IEnumerable<ILObject> SelectedObjects { get; }

    /// <summary>
    /// Название подключенного чекаута.
    /// </summary>
    string CheckOutName { get; }

    /// <summary>
    /// Инициализирует свойство INetPC.
    /// </summary>
    /// <remarks>
    /// Примечание:
    /// ** Небходимо использовать в начале метода обработчика команд!
    /// </remarks>
    void InitNetPluginCall(INetPluginCall iNetPC);

    /// <summary>
    /// Регистрирует в базе данных файл, находящийся на рабочем диске пользователя.
    /// </summary>
    /// <param name="typeName">Название типа создаваемого объекта</param>
    /// <param name="product">Ключевой атрибут создаваемого объекта</param>
    /// <param name="stateName">Состояние вновь создаваемого объекта (если создается объект)</param>
    /// <param name="isProject">Признак того, что объект будет являться проектом</param>
    ILObject NewObject(string typeName, string product, string stateName = null, bool isProject = false);

    /// <summary>
    /// Вставляет новое или существующее изделие в редактируемый объект.
    /// </summary>
    /// <param name="parent">Объект родителя</param>
    /// <param name="child">Объект потомка</param>
    /// <param name="linkType">Название типа связи</param>
    /// <param name="stateName">Состояние вновь создаваемого объекта (если создается объект)</param>
    /// <param name="reuse">Устанавливает возможность повторного применения объекта</param>
    /// <remarks>
    /// Примечание:
    /// <para>** reuse:
    /// Если значение - true, то независимо от существования в базе данных такой пары связанных объектов будет создаваться еще один экземпляр связи.
    /// Если значение - false, то при существовании в базе данных такой пары связанных объектов новый экземпляр связи создаваться не будет, при этом будет вызвано исключение.</para>
    /// </remarks>
    /// <returns>Возвращает идентификатор созданной связи.</returns>
    int InsertObject(ILObject parent, ILObject child, string linkType, string stateName = null, bool reuse = false);

    /// <summary>
    /// Вставляет новое или существующее изделие в редактируемый объект.
    /// </summary>
    /// <param name="parentTypeName">Тип объекта-родителя</param>
    /// <param name="parentProduct">Значение ключевого атрибута объекта-родителя</param>
    /// <param name="parentVersion">Номер версии объекта-родителя</param>
    /// <param name="childTypeName">Тип объекта-потомка</param>
    /// <param name="childProduct">Значение ключевого атрибута объекта-потомка</param>
    /// <param name="childVersion">Номер версии объекта-потомка</param>
    /// <param name="linkType">Название типа связи</param>
    /// <param name="stateName">Состояние вновь создаваемого объекта (если создается объект)</param>
    /// <param name="reuse">Устанавливает возможность повторного применения объекта</param>
    /// <remarks>
    /// Примечание:
    /// <para>** reuse:
    /// Если значение - true, то независимо от существования в базе данных такой пары связанных объектов будет создаваться еще один экземпляр связи.
    /// Если значение - false, то при существовании в базе данных такой пары связанных объектов новый экземпляр связи создаваться не будет, при этом будет вызвано исключение.</para>
    /// </remarks>
    /// <returns>Возвращает идентификатор созданной связи.</returns>
    int InsertObject(string parentTypeName, string parentProduct, string parentVersion, string linkType, string childTypeName, string childProduct, string childVersion = " ", string stateName = null, bool reuse = false);

    /// <summary>
    /// Добавляет связь между объектами.
    /// </summary>
    /// <param name="parent">Объект родителя</param>
    /// <param name="child">Объект потомка</param>
    /// <param name="linkType">Название типа связи</param>
    /// <param name="minQuantity">Нижняя граница количества</param>
    /// <param name="maxQuantity">Верхняя граница количества</param>
    /// <param name="unitId">Уникальный идентификатор единицы измерения</param>
    /// <returns>Возвращает идентификатор созданной связи.</returns>
    int NewLink(ILObject parent, ILObject child, string linkType, double minQuantity = 0, double maxQuantity = 0, string unitId = null);

    /// <summary>
    /// Добавляет связь между объектами.
    /// </summary>
    /// <param name="parentId">Идентификатор версии объекта-родителя</param>
    /// <param name="childId">Идентификатор версии объекта-потомка</param>
    /// <param name="linkType">Название типа связи</param>
    /// <param name="minQuantity">Нижняя граница количества</param>
    /// <param name="maxQuantity">Верхняя граница количества</param>
    /// <param name="unitId">Уникальный идентификатор единицы измерения</param>
    /// <returns>Возвращает идентификатор созданной связи.</returns>
    int NewLink(int parentId, int childId, string linkType, double minQuantity = 0, double maxQuantity = 0, string unitId = null);

    /// <summary>
    /// Добавляет связь между объектами.
    /// </summary>
    /// <param name="parentTypeName">Тип объекта-родителя</param>
    /// <param name="parentProduct">Значение ключевого атрибута объекта-родителя</param>
    /// <param name="parentVersion">Номер версии объекта-родителя</param>
    /// <param name="childTypeName">Тип объекта-потомка</param>
    /// <param name="childProduct">Значение ключевого атрибута объекта-потомка</param>
    /// <param name="childVersion">Номер версии объекта-потомка</param>
    /// <param name="linkType">Название типа связи</param>
    /// <param name="minQuantity">Нижняя граница количества</param>
    /// <param name="maxQuantity">Верхняя граница количества</param>
    /// <param name="unitId">Уникальный идентификатор единицы измерения</param>
    /// <returns>Возвращает идентификатор созданной связи.</returns>
    int NewLink(string parentTypeName, string parentProduct, string parentVersion, string childTypeName, string childProduct, string childVersion, string linkType, double minQuantity = 0, double maxQuantity = 0, string unitId = null);

    /// <summary>
    /// Обновляет связь между объектами.
    /// </summary>
    /// <param name="idLink">Идентификатор связи</param>
    /// <param name="minQuantity">Нижняя граница количества</param>
    /// <param name="maxQuantity">Верхняя граница количества</param>
    /// <param name="unitId">Уникальный идентификатор единицы измерения</param>
    void UpLink(int idLink, double minQuantity = 0, double maxQuantity = 0, string unitId = null);

    /// <summary>
    /// Удаляет связь между объектами.
    /// </summary>
    /// <param name="idLink">Идентификатор связи</param>
    void DeleteLink(int idLink);

    /// <summary>
    /// Возвращает список объектов, привязанных соответствующей связью.
    /// </summary>
    /// <param name="objectId">Идентификатор версии объекта</param>
    /// <param name="linkType">Название типа связи</param>
    /// <param name="inverse">Направление (true - обратное, false - прямое)</param>
    List<ILObject> GetLinkedFast(int objectId, string linkType, bool inverse = false);

    /// <summary>
    /// Получение атрибутов объекта, включая служебные.
    /// </summary>
    /// <param name="loodsmanObject">Объект Лоцман</param>
    /// <returns>Возвращает атрибуты объекта, включая служебные.</returns>
    IEnumerable<LAttribute> GetAttributes(ILObject loodsmanObject);

    /// <summary>
    /// Приводит значение к заданной единице измерения.
    /// </summary>
    /// <param name="value">Значение</param>
    /// <param name="sourceMeasureUnit">Исходная единица измерения</param>
    /// <param name="destMeasureUnit">Требуемая единица измерения</param>
    /// <returns>Возвращает преобразованное значение.</returns>
    double ConverseValue(double value, LMeasureUnit sourceMeasureUnit, LMeasureUnit destMeasureUnit);

    /// <summary>
    /// Изменяет состояние объекта.
    /// </summary>
    ///  <param name="objectId">Идентификатор версии объекта</param>
    /// <param name="state">Состояние</param>
    void UpdateState(int objectId, LStateInfo state);

    /// <summary>
    /// Добавляет, удаляет, обновляет значение атрибута объекта.
    /// </summary>
    /// <param name="objectId">Идентификатор версии объекта</param>
    /// <param name="attributeName">Название атрибута</param>
    /// <param name="attributeValue">Значение атрибута. Если null или string.Empty то атрибут будет помечен на удаление</param>
    /// <param name="measureUnit">Единица измерения</param>
    void UpAttrValueById(int objectId, string attributeName, object attributeValue, LMeasureUnit measureUnit = null);

    /// <summary>
    /// Регистрирует в базе данных файл, находящийся на рабочем диске пользователя.
    /// </summary>
    /// <param name="documentId">Идентификатор версии объекта</param>
    /// <param name="fileName">Название файла (должен быть уникальным)</param>
    /// <param name="filePath">Путь к файлу относительно диска из настройки "Буква рабочего диска", доступный серверу приложений</param>  
    /// <remarks>
    /// Примечание:
    /// Если путь указан не на рабочий диск Лоцмана <see cref="Entities.Meta.OrganisationUnit.LUser.WorkDir"/>, то файл будет скопирован на рабочий диск.
    /// </remarks>
    string RegistrationOfFile(int documentId, string fileName, string filePath);

    /// <summary>
    /// Регистрирует в базе данных файл, находящийся на рабочем диске пользователя.
    /// </summary>
    /// <param name="typeName">Название типа</param>
    /// <param name="product">Ключевой атрибут</param>
    /// <param name="version">Версия объекта</param>
    /// <param name="fileName">Название файла (должен быть уникальным)</param>        
    /// <param name="filePath">Путь к файлу относительно диска из настройки "Буква рабочего диска"</param>  
    /// <remarks>
    /// Примечание:
    /// Если путь указан не на рабочий диск Лоцмана <see cref="Entities.Meta.OrganisationUnit.LUser.WorkDir"/>, то файл будет скопирован на рабочий диск.
    /// </remarks>
    string RegistrationOfFile(string typeName, string product, string version, string fileName, string filePath);

    /// <summary>
    /// Сохраняет вторичное представление документа.
    /// </summary>
    /// <param name="documentId">Идентификатор версии объекта</param>
    /// <param name="filePath">Путь к файлу вторичного представления, относительно диска из настройки "Буква рабочего диска", доступный серверу приложений</param>        
    /// <param name="removeAfterSave">Признак удаления файла с рабочего диска, после сохранения</param>        
    /// <remarks>
    /// Примечание:
    /// Если путь указан не на рабочий диск Лоцмана <see cref="Entities.Meta.OrganisationUnit.LUser.WorkDir"/>, то файл будет скопирован на рабочий диск.
    /// </remarks>
    void SaveSecondaryView(int documentId, string filePath, bool removeAfterSave = true);

    /// <summary>
    /// Возвращает все версии заданного объекта или "похожего" объекта, если в базе данных установлена соответствующая настройка.
    /// </summary>
    /// <param name="typeName">Название типа</param>
    /// <param name="product">Ключевой атрибут</param>
    bool CheckUniqueName(string typeName, string product);

    /// <summary>
    /// Возвращает данные для формирования отчета.
    /// </summary>
    /// <param name="reportName">Название хранимой процедуры</param>
    /// <param name="objectsIds">Список идентификаторов версий объектов</param>
    /// <param name="reportParams">Параметры отчёта формата "параметр1=значение1;параметр2=значение2"</param>        
    /// <remarks>
    /// Примечание:
    /// Если отчет не требует идентификаторов, то необходимо передать null. Параметры отчёта должны быть формата "параметр1=значение1;параметр2=значение2"
    /// </remarks>
    DataTable GetReport(string reportName, IEnumerable<int> objectsIds = null, string reportParams = null);

    /// <summary>
    /// Возвращает объекты с заполнеными свойствами.
    /// </summary>
    /// <param name="objectsIds">Список идентификаторов версий объектов</param>
    List<ILObject> GetPropObjects(IEnumerable<int> objectsIds);

    /// <summary>
    /// Проверка на существование бизнес объекта в базе Лоцман.
    /// </summary>
    /// <param name="typeName">Название типа</param>
    /// <param name="uniqueId">Ключевой атрибут формата ***BOSimple</param>
    /// <remarks>
    /// Примечание:
    /// Если объект не существует то свойство возвращаемого объекта ILoodsmanObject.Id будет равен 0.
    /// <para>** Для создания бизнес объекта необходимо чтобы product был в формате ***BOSimple</para>
    /// </remarks>
    ILObject PreviewBoObject(string typeName, string uniqueId);

    /// <summary>
    /// Возвращает список идентификаторов версий объектов, заблокированных в текущем чекауте.
    /// </summary>
    List<int> GetLockedObjectsIds();

    /// <summary>
    /// Помечает объект, находящийся на изменении, как подлежащий удалению при возврате в базу данных.
    /// </summary>
    /// <param name="objectId">Идентификатор версии объекта</param>
    void KillVersion(int objectId);

    /// <summary>
    /// Помечает объекты, находящиеся на изменении, как подлежащий удалению при возврате в базу данных.
    /// </summary>
    /// <param name="objectsIds">Список идентификаторов версий объектов</param>
    void KillVersion(IEnumerable<int> objectsIds);

    /// <summary>
    /// Помечает объект, находящийся на изменении, как подлежащий удалению при возврате в базу данных.
    /// </summary>
    /// <param name="typeName">Название типа</param>
    /// <param name="product">Ключевой атрибут</param>
    /// <param name="version">Версия объекта</param>
    void KillVersion(string typeName, string product, string version);

    /// <summary>
    /// Берет объект на редактирование.
    /// </summary>
    /// <param name="typeName">Название типа</param>
    /// <param name="product">Ключевой атрибут</param>
    /// <param name="version">Версия объекта</param>
    /// <param name="mode">Режим</param>
    /// <returns>Возвращает внутреннее название редактируемого объекта (название чекаута).</returns>
    /// <remarks>
    /// Примечание:
    /// При пустых значениях typeName, product и version, будет создан пустой чекаут. С ним можно работать, как с обычным. 
    /// Отличие в том, что в списке, возвращаемым методом GetInfoAboutCurrentBase (режим 2) в качестве значений полей[_ID_VERSION], [_TYPE], [_PRODUCT], [_VERSION] будут пустые значения.
    /// <para>** Для дальнейшей работы с объектом необходимо подключиться к чекауту методом ConnectToCheckOut.</para>
    /// </remarks>
    string CheckOut(string typeName = null, string product = null, string version = null, CheckOutMode mode = CheckOutMode.Default);

    /// <summary>
    /// Берет в работу текущий SelectedObject, если он уже находится в работе просто возращает PluginCall.CheckOut. Будет автоматически подключен к чекауту методом ConnectToCheckOut.
    /// </summary>
    /// <returns>Возвращает внутреннее название редактируемого объекта (название чекаута).</returns>
    string SelectedObjectCheckOut(CheckOutMode mode = CheckOutMode.Default);

    /// <summary>
    /// Подключается к указанному чекауту.
    /// </summary>
    /// <param name="checkOutName">Название чекаута</param>
    /// <param name="dBName">Название базы данных</param>
    /// <remarks>
    /// Примечание:
    /// При пустых значении: checkOutName - используется ILoodsmanProxy.CheckOutName, dBName - используется PluginCall.DBName. 
    /// </remarks>
    void ConnectToCheckOut(string checkOutName = null, string dBName = null);

    /// <summary>
    /// Делает объект доступным для изменения только в рамках текущего чекаута (блокирует его).
    /// </summary>
    /// <param name="objectId">Идентификатор версии</param>
    /// <param name="isRoot">Признак головного объекта</param>
    void AddToCheckOut(int objectId, bool isRoot = false);

    /// <summary>
    /// Возвращает измененный объект в базу данных.
    /// </summary>
    /// <param name="checkOutName">Название чекаута</param>
    /// <param name="dBName">Название базы данных</param>
    /// <remarks>
    /// Примечание:
    /// При пустых значении: checkOutName - используется PluginCall.CheckOut, dBName - используется PluginCall.DBName. 
    /// </remarks>
    void CheckIn(string checkOutName = null, string dBName = null);

    /// <summary>
    /// Возвращает измененный объект в базу данных.
    /// </summary>
    /// <param name="checkOutName">Название чекаута</param>
    /// <param name="dBName">Название базы данных</param>
    /// <remarks>
    /// Примечание:
    /// При пустых значении: checkOutName - используется PluginCall.CheckOut, dBName - используется PluginCall.DBName. 
    /// </remarks>
    void SaveChanges(string checkOutName = null, string dBName = null);

    /// <summary>
    /// Выполняет отказ от изменения объекта и вызывает метод DisconnectCheckOut
    /// </summary>
    /// <param name="checkOutName">Название чекаута</param>
    /// <param name="dBName">Название базы данных</param>
    /// <remarks>
    /// Примечание:
    /// При пустых значении: checkOutName - используется PluginCall.CheckOut, dBName - используется PluginCall.DBName. 
    /// </remarks>
    void CancelCheckOut(string checkOutName = null, string dBName = null);
  }

  internal class LoodsmanProxy : ILoodsmanProxy
  {
    private readonly ILoodsmanMeta _meta;
    private string _checkOutName;
    private ILObject _selectedObject;
    private List<ILObject> _selectedObjects = new List<ILObject>();

    public LoodsmanProxy(INetPluginCall iNetPC, ILoodsmanMeta loodsmanMeta)
    {
      INetPC = iNetPC;

      var culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
      culture.NumberFormat.NumberDecimalSeparator = ".";
      culture.DateTimeFormat.DateSeparator = ".";
      culture.DateTimeFormat.ShortDatePattern = "dd.MM.yyyy";
      culture.DateTimeFormat.ShortTimePattern = "HH:mm";
      culture.DateTimeFormat.LongDatePattern = "HH:mm:ss";
      Thread.CurrentThread.CurrentCulture = culture;
      Thread.CurrentThread.CurrentUICulture = culture;
      /*
      var userInfo = (DataSet)pluginCall.GetDataSet("GetInfoAboutCurrentUser", new object[] { });
      //CurrentUser = userInfo["_FULLNAME"] as string;
      var userFileDir = userInfo.FieldValue["_FILEDIR"] as string;
      var reportName = "Ведомость покупных.fp3";
      var reports = pluginCall.GetDataSet("GetReportsAndFolders", new object[] { -1 });//"rep_VEDOMOST_SPECIFIKACIY"




      var report = pluginCall.GetDataSet("GetReport", new object[] { "rep_VEDOMOST_SPECIFIKACIY", pluginCall.IdVersion, null });//"rep_VEDOMOST_MATERIALOV"
      var guid = typeof(ILoodsmanApplication).GUID;
      var hr = Marshal.QueryInterface(pc, ref guid, out var pI); 
      var application = (ILoodsmanApplication)Marshal.GetTypedObjectForIUnknown(pI, typeof(ILoodsmanApplication));
      var fRDesigner = new FRDesigner();
      fRDesigner.ReportParams = null;// "Изделие (заказ №)=123;Программа=2";
      fRDesigner.ParentHWND = application.AppHandle;
      fRDesigner.Connection = application.DataBase.Connection;
      fRDesigner.Context = pluginCall.Selected;
      fRDesigner.FileName = @"C:\Program Files (x86)\Common Files\ASCON Shared\COD\ReportTemplates\Конструкторские\Ведомость спецификаций ГОСТ 2.106-96.fr3";// "C:\Program Files (x86)\Common Files\ASCON Shared\COD\ReportTemplates\ПМЗ\tem_VEDOMOST_MATERIALOV.fr3";
      fRDesigner.ExternalDataset = report;
      var thread = new Thread(new ThreadStart(() =>
      {
          while (Thread.CurrentThread.ThreadState == ThreadState.Running)
              if (User32.FindWindowsRaw(string.Empty, "TfrmReportsShow").FirstOrDefault() is IntPtr wnd)
                  User32.SendMessage(wnd, (uint)CMD.WM_CLOSE, 0, 0);
      }));
      thread.Start();
      fRDesigner.Open(frMode.mOpenReportExtData);
      thread.Abort();
      File.Copy(fRDesigner.FileName, $"{userFileDir}\\{reportName}", true);
       */
      _meta = loodsmanMeta;
    }

    public INetPluginCall INetPC { get; private set; }

    public ILoodsmanMeta Meta => _meta;

    public ILObject SelectedObject => GetSelectedObject();

    public IEnumerable<ILObject> SelectedObjects => GetSelectedObjects();

    public string CheckOutName => _checkOutName;

    public void InitNetPluginCall(INetPluginCall iNetPC)
    {
      INetPC = iNetPC;
    }

    private ILObject GetSelectedObject()
    {
      return _selectedObject?.Id == INetPC.PluginCall.IdVersion ? _selectedObject : _selectedObject = new LObject(INetPC.PluginCall, this);
    }

    private IEnumerable<ILObject> GetSelectedObjects()
    {
      var ids = INetPC.Native_CGetTreeSelectedIDs().Split(new[] { Constants.ID_SEPARATOR }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
      if (!_selectedObjects.Select(x => x.Id).OrderBy(x => x).SequenceEqual(ids.OrderBy(x => x)))
        _selectedObjects = GetPropObjects(ids);
      return _selectedObjects;
    }

    #region NewObject

    public ILObject NewObject(string typeName, string product, string stateName = null, bool isProject = false)
    {
      var type = _meta.Types[typeName];
      var state = string.IsNullOrEmpty(stateName) ? type.DefaultState : _meta.States[stateName];
      var loodsmanObject = new LObject(this, type, state)
      {
        Id = INetPC.Native_NewObject(type.Name, state.Name, product, isProject),
        Product = product,
      };
      return loodsmanObject;
    }

    private string StateIfNullGetDefault(string typeName, string stateName = null)
    {
      if (string.IsNullOrEmpty(typeName))
        throw new ArgumentException($"{nameof(typeName)} - тип не может быть пустым", nameof(typeName));

      if (string.IsNullOrEmpty(stateName))
        stateName = _meta.Types[typeName].DefaultState.Name;
      return stateName;
    }
    #endregion

    #region Link - Insert/New/Update/Remove
    public int InsertObject(ILObject parent, ILObject child, string linkType, string stateName = null, bool reuse = false)
    {
      CheckLoodsmanObjectsForError(parent, child);
      CheckInsertedObject(parent);
      CheckInsertedObject(child);
      return InsertObject(parent.Type.Name, parent.Product, parent.Version, linkType, child.Type.Name, child.Product, child.Version, stateName, reuse);
    }

    private void CheckInsertedObject(ILObject loodsmanObject)
    {
      if (loodsmanObject.Id <= 0 && string.IsNullOrEmpty(loodsmanObject.Version))
        loodsmanObject.Version = Constants.DEFAULT_INSERT_NEW_VERSION;
    }

    public int InsertObject(string parentTypeName, string parentProduct, string parentVersion, string linkType, string childTypeName, string childProduct, string childVersion = Constants.DEFAULT_INSERT_NEW_VERSION, string stateName = null, bool reuse = false)
    {
      CheckKeyAttributesForErrors(parentTypeName, parentProduct, childTypeName, childProduct);
      var linkInfo = GetLinkInfo(parentTypeName, childTypeName, linkType);
      if (linkInfo.Direction == LinkDirection.Backward)
        Swap(ref parentTypeName, ref parentProduct, ref parentVersion, ref childTypeName, ref childProduct, ref childVersion);

      if (string.IsNullOrEmpty(stateName))
        stateName = StateIfNullGetDefault(parentVersion == Constants.DEFAULT_INSERT_NEW_VERSION ? parentTypeName : childTypeName);
      return INetPC.Native_InsertObject(parentTypeName, parentProduct, parentVersion, linkType, childTypeName, childProduct, childVersion, stateName, reuse);
    }

    public int NewLink(ILObject parent, ILObject child, string linkType, double minQuantity = 0, double maxQuantity = 0, string unitId = null)
    {
      CheckLoodsmanObjectsForError(parent, child);
      if (parent.Id <= 0 && child.Id <= 0)
      {
        if (string.IsNullOrEmpty(parent.Product) && string.IsNullOrEmpty(child.Product))
          throw new InvalidOperationException("Не заданы ключевые атрибуты объектов для формирования связи");
      }
      else
      {
        if (parent.Id <= 0)
          NewObject(parent.Type.Name, parent.Product);

        if (child.Id <= 0)
          NewObject(child.Type.Name, child.Product);
      }
      return NewLink(parent.Id, parent.Type.Name, parent.Product, parent.Version, child.Id, child.Type.Name, child.Product, child.Version, linkType, minQuantity, maxQuantity, unitId);
    }


    public int NewLink(string parentTypeName, string parentProduct, string parentVersion, string childTypeName, string childProduct, string childVersion, string linkType, double minQuantity = 0, double maxQuantity = 0, string unitId = null)
    {
      CheckKeyAttributesForErrors(parentTypeName, parentProduct, childTypeName, childProduct);
      return NewLink(0, parentTypeName, parentProduct, parentVersion, 0, childTypeName, childProduct, childVersion, linkType, minQuantity, maxQuantity, unitId);
    }

    public int NewLink(int parentId, int childId, string linkType, double minQuantity = 0, double maxQuantity = 0, string unitId = null)
    {
      if (parentId <= 0)
        throw new ArgumentException($"{nameof(parentId)} - отсутствует или неверно задан идентификатор объекта", nameof(parentId));

      if (childId <= 0)
        throw new ArgumentException($"{nameof(childId)} - отсутствует или неверно задан идентификатор объекта", nameof(childId));

      return INetPC.Native_NewLink(parentId, string.Empty, string.Empty, string.Empty, childId, string.Empty, string.Empty, string.Empty, minQuantity, maxQuantity, unitId, linkType);
    }

    public void UpLink(int idLink, double minQuantity = 0, double maxQuantity = 0, string unitId = null)
    {
      INetPC.Native_UpLink(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, idLink, minQuantity, maxQuantity, unitId, false, string.Empty);
    }

    public void DeleteLink(int idLink)
    {
      INetPC.Native_UpLink(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, idLink, 0, 0, string.Empty, true, string.Empty);
    }

    private int NewLink(int parentId, string parentTypeName, string parentProduct, string parentVersion, int childId, string childTypeName, string childProduct, string childVersion, string linkType, double minQuantity, double maxQuantity, string unitId)
    {
      //if (string.IsNullOrEmpty(linkType))
      //linkInfo = _loodsmanMeta.LinksInfoBetweenTypes.SingleOrDefault(x => (x.TypeName1 == parentTypeName && x.TypeName2 == childTypeName) || (x.TypeName1 == childTypeName && x.TypeName2 == parentTypeName));
      //Способ автоматически найти подходящий тип связи, но он будет не стабильным если пользователь произведёт изменение конфигурации бд
      if (string.IsNullOrEmpty(linkType))
        throw new ArgumentException($"{nameof(linkType)} не может быть пустым, не указан тип связи", nameof(linkType));

      var linkInfo = GetLinkInfo(parentTypeName, childTypeName, linkType);
      if (linkInfo.Direction == LinkDirection.Backward)
        Swap(ref parentId, ref parentTypeName, ref parentProduct, ref parentVersion, ref childId, ref childTypeName, ref childProduct, ref childVersion);

      if (linkInfo.IsQuantity && minQuantity <= 0 && maxQuantity <= 0)
      {
        minQuantity = 1;
        maxQuantity = 1;
      }
      return INetPC.Native_NewLink(parentId, parentTypeName, parentProduct, parentVersion, childId, childTypeName, childProduct, childVersion, minQuantity, maxQuantity, unitId, linkType);
    }

    private static void CheckKeyAttributesForErrors(string parentTypeName, string parentProduct, string childTypeName, string childProduct)
    {
      if (string.IsNullOrEmpty(parentTypeName))
        throw new ArgumentException($"{nameof(parentTypeName)} не может быть пустым или иметь значение null", nameof(parentTypeName));

      if (string.IsNullOrEmpty(parentProduct))
        throw new ArgumentException($"{nameof(parentProduct)} не может быть пустым или иметь значение null", nameof(parentProduct));

      if (string.IsNullOrEmpty(childTypeName))
        throw new ArgumentException($"{nameof(childTypeName)} не может быть пустым или иметь значение null", nameof(childTypeName));

      if (string.IsNullOrEmpty(childProduct))
        throw new ArgumentException($"{nameof(childProduct)} не может быть пустым или иметь значение null", nameof(childProduct));
    }

    private static void CheckLoodsmanObjectsForError(ILObject parent, ILObject child)
    {
      if (parent is null)
        throw new ArgumentNullException($"{nameof(parent)} не задан родитель для создания связи");

      if (child is null)
        throw new ArgumentNullException($"{nameof(child)} не задан потомок для создания связи");
    }

    private LLinkInfoBetweenTypes GetLinkInfo(string parentTypeName, string childTypeName, string linkType)
    {
      var linkInfo = _meta.LinksInfoBetweenTypes.FirstOrDefault(x => x.TypeName1 == parentTypeName && x.TypeName2 == childTypeName && x.Name == linkType);
      if (linkInfo is null)
        throw new InvalidOperationException($"Не удалось найти информацию о связи типов {nameof(parentTypeName)}: \"{parentTypeName}\" - {nameof(childTypeName)}: \"{childTypeName}\", по связи: \"{linkType}\"");
      return linkInfo;
    }

    private static void Swap(ref int parentId, ref string parentTypeName, ref string parentProduct, ref string parentVersion, ref int childId, ref string childTypeName, ref string childProduct, ref string childVersion)
    {
      var tId = parentId;
      parentId = childId;
      childId = tId;
      Swap(ref parentTypeName, ref parentProduct, ref parentVersion, ref childTypeName, ref childProduct, ref childVersion);
    }

    private static void Swap(ref string parentTypeName, ref string parentProduct, ref string parentVersion, ref string childTypeName, ref string childProduct, ref string childVersion)
    {
      var tTypeName = parentTypeName;
      var tProduct = parentProduct;
      var tVersion = parentVersion;
      parentTypeName = childTypeName;
      parentProduct = childProduct;
      parentVersion = childVersion;
      childTypeName = tTypeName;
      childProduct = tProduct;
      childVersion = tVersion;
    }

    public List<ILObject> GetLinkedFast(int objectId, string linkType, bool inverse = false)
    {
      return new List<ILObject>(INetPC.Native_GetLinkedFast(objectId, linkType, inverse).Select(x => new LObject(x, this)));
    }
    #endregion

    public IEnumerable<LAttribute> GetAttributes(ILObject loodsmanObject)
    {
      var attributesInfo = INetPC.Native_GetInfoAboutVersion(loodsmanObject.Id, GetInfoAboutVersionMode.Mode3).Select(x => x);
      foreach (var lTypeAttribute in loodsmanObject.Type.Attributes)
      {
        var attribute = attributesInfo.FirstOrDefault(x => x["_NAME"] as string == lTypeAttribute.Name);
        var measureId = string.Empty;
        var unitId = string.Empty;
        var value = attribute?["_VALUE"];
        if (lTypeAttribute.IsMeasured && !(value is null))
        {
          measureId = attribute["_ID_MEASURE"] as string;
          unitId = attribute["_ID_UNIT"] as string;
        }

        yield return new LAttribute(this, loodsmanObject, lTypeAttribute, value, measureId, unitId);
      }
    }

    public double ConverseValue(double value, LMeasureUnit sourceMeasureUnit, LMeasureUnit destMeasureUnit)
    {
      if (sourceMeasureUnit is null || destMeasureUnit is null || sourceMeasureUnit == destMeasureUnit)
        return value;

      if (sourceMeasureUnit.ParentMeasure != destMeasureUnit.ParentMeasure)
        throw new ArgumentException($"Невозможно преобразование единиц измерения из \"{sourceMeasureUnit.ParentMeasure.Name}\" в \"{destMeasureUnit.ParentMeasure.Name}\"");

      return INetPC.Native_ConverseValue(value, sourceMeasureUnit.Guid, destMeasureUnit.Guid);
    }

    public void UpdateState(int objectId, LStateInfo state)
    {
      if (state is null)
        throw new Exception("Состоянием не может быть пустым");

      INetPC.Native_UpdateStateOnObject(objectId, state.Name);
    }

    public void UpAttrValueById(int objectId, string attributeName, object attributeValue, LMeasureUnit measureUnit = null)
    {
      INetPC.Native_UpAttrValueById(objectId, attributeName, attributeValue, measureUnit?.Guid, IsNullOrDefault(attributeValue));
    }

    public static bool IsNullOrDefault<T>(T value)
    {
      return value == null || (value is string strValue && strValue == string.Empty);
    }

    public string RegistrationOfFile(int documentId, string fileName, string filePath)
    {
      try
      {
        //var floderPath = string.Join("\\", Ancestors().OrderBy(x => x.Level).Cast<LoodsmanObjectVM>().Where(x => x.TypeName == _settingImport.FolderTypeName).Select(x => x.Product));
        //var workDirPath = $"{proxy.UserFileDir}\\{floderPath}";
        //if (!Directory.Exists(workDirPath))
        //    Directory.CreateDirectory(workDirPath);
        //var newPath = $"{workDirPath}\\{Product} - {Name} ({TypeName}){FileNode.Info.Extension}";
        //Лоцман не чистит за собой папки, поэтому пока без структуры папок и ложим всё в корень W:\

        filePath = CopyIfNeddedOnWorkDir(filePath);
        INetPC.Native_RegistrationOfFile(documentId, fileName, filePath);
        return filePath;
      }
      catch// (Exception ex)
      {
        return string.Empty;
        //var test = ex;
        //logger?
      }
    }
    public string RegistrationOfFile(string typeName, string product, string version, string fileName, string filePath)
    {
      try
      {
        filePath = CopyIfNeddedOnWorkDir(filePath);
        INetPC.Native_RegistrationOfFile(typeName, product, version, fileName, filePath);
        return filePath;
      }
      catch
      {
        return string.Empty;
        //logger?
      }
    }

    public void SaveSecondaryView(int docId, string filePath, bool removeAfterSave = true)
    {
      filePath = CopyIfNeddedOnWorkDir(filePath);
      INetPC.Native_SaveSecondaryView(docId, filePath);

      if (removeAfterSave)
        try { File.Delete(filePath); } catch { }
    }

    private string CopyIfNeddedOnWorkDir(string filePath)
    {
      if (!filePath.Contains(_meta.CurrentUser.WorkDir))
      {
        var newPath = $@"{_meta.CurrentUser.WorkDir}\{Path.GetFileName(filePath)}";
        File.Copy(filePath, newPath, true);
        filePath = newPath;
      }

      return filePath;
    }

    public bool CheckUniqueName(string typeName, string product)
    {
      return INetPC.Native_CheckUniqueName(typeName, product).Rows.Count != 0;
    }

    public DataTable GetReport(string reportName, IEnumerable<int> objectsIds = null, string reportParams = null)
    {
      return INetPC.Native_GetReport(reportName, objectsIds, reportParams);
    }

    public List<ILObject> GetPropObjects(IEnumerable<int> objectsIds)
    {
      return new List<ILObject>(INetPC.Native_GetPropObjects(objectsIds).Select(x => new LObject(x, this)));
    }

    public ILObject PreviewBoObject(string typeName, string uniqueId)
    {
      var xmlString = INetPC.Native_PreviewBoObject(typeName, uniqueId);
      var xDocument = XDocument.Parse(xmlString);
      var elements = xDocument.Descendants("PreviewBoObjectResult").Elements();
      var type = _meta.Types[typeName];
      var stateName = elements.FirstOrDefault(x => x.Name == "State")?.Value;
      var state = string.IsNullOrEmpty(stateName) ? type.DefaultState : _meta.States[stateName];
      var loodsmanObject = new LObject(this, type, state)
      {
        Id = int.TryParse(elements.FirstOrDefault(x => x.Name == "VersionId")?.Value, out var id) ? id : 0,
        Product = elements.FirstOrDefault(x => x.Name == "Product").Value,
        //Version = elements.FirstOrDefault(x => x.Name == "Version")?.Value
      };
      return loodsmanObject;
    }

    public List<int> GetLockedObjectsIds()
    {
      return INetPC.Native_GetLockedObjects().Select(x => (int)x[0]).ToList();
    }


    #region KillVersion
    public void KillVersion(int id)
    {
      INetPC.Native_KillVersion(id);
    }

    public void KillVersion(IEnumerable<int> objectsIds)
    {
      INetPC.Native_KillVersions(objectsIds);
    }

    public void KillVersion(string typeName, string product, string version)
    {
      INetPC.Native_KillVersion(typeName, product, version);
    }
    #endregion

    #region CheckOut
    public string CheckOut(string typeName, string product, string version, CheckOutMode mode = CheckOutMode.Default)
    {
      return _checkOutName = INetPC.Native_CheckOut(typeName, product, version, mode);
    }

    public string SelectedObjectCheckOut(CheckOutMode mode = CheckOutMode.Default)
    {
      var pc = INetPC.PluginCall;
      var wasCheckout = pc.CheckOut != 0;
      _checkOutName = wasCheckout ? pc.CheckOut.ToString() : CheckOut(pc.stType, pc.stProduct, pc.stVersion, mode);
      if (!wasCheckout)
        ConnectToCheckOut(_checkOutName, pc.DBName);

      return _checkOutName;
    }

    public void ConnectToCheckOut(string checkOutName = null, string dBName = null)
    {
      var localCheckOutName = checkOutName ?? _checkOutName;
      if (string.IsNullOrEmpty(localCheckOutName))
        return;

      INetPC.Native_ConnectToCheckOut(localCheckOutName, dBName ?? INetPC.PluginCall.DBName);
    }

    public void AddToCheckOut(int objectId, bool isRoot = false)
    {
      INetPC.Native_AddToCheckOut(objectId, isRoot);
    }

    public void CheckIn(string checkOutName = null, string dBName = null)
    {
      var localCheckOutName = checkOutName ?? _checkOutName;
      var localDBName = dBName ?? INetPC.PluginCall.DBName;
      //INetPC.Native_DisconnectCheckOut(localCheckOutName, localDBName);
      INetPC.Native_CheckIn(localCheckOutName, localDBName);
      _checkOutName = string.Empty;
    }

    public void SaveChanges(string checkOutName = null, string dBName = null)
    {
      var localCheckOutName = checkOutName ?? _checkOutName;
      var localDBName = dBName ?? INetPC.PluginCall.DBName;
      INetPC.Native_SaveChanges(localCheckOutName, localDBName);
    }

    public void CancelCheckOut(string checkOutName = null, string dBName = null)
    {
      var localCheckOutName = checkOutName ?? _checkOutName;
      if (string.IsNullOrEmpty(localCheckOutName))
        return;

      var localDBName = dBName ?? INetPC.PluginCall.DBName;
      //INetPC.Native_DisconnectCheckOut(localCheckOutName, localDBName);
      INetPC.Native_CancelCheckOut(localCheckOutName, localDBName);
      _checkOutName = string.Empty;
    }
    #endregion
  }
}
