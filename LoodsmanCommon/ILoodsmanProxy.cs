using Loodsman;
using System.Collections.Generic;
using System.Data;

namespace LoodsmanCommon
{
  /// <summary> Прокси объект для удобного взаимодействия с ЛОЦМАН:PLM. </summary>
  public interface ILoodsmanProxy
  {
    /// <summary> Возвращает метаданные конфигурации базы Лоцман. </summary>
    ILoodsmanMeta Meta { get; }

    /// <summary> Возвращает выбранный объект в дереве Лоцман. </summary>
    ILObject SelectedObject { get; }

    /// <summary> Возвращает выбранные объекты в дереве Лоцман. </summary>
    IEnumerable<ILObject> SelectedObjects { get; }

    /// <summary> Возвращает название подключенного чекаута. </summary>
    string CheckOutName { get; }

    /// <summary> Регистрирует в базе данных файл, находящийся на рабочем диске пользователя. </summary>
    /// <param name="typeName"> Название типа создаваемого объекта. </param>
    /// <param name="product"> Ключевой атрибут создаваемого объекта. </param>
    /// <param name="stateName"> Состояние вновь создаваемого объекта (если создается объект). </param>
    /// <param name="isProject"> Признак того, что объект будет являться проектом. </param>
    ILObject NewObject(string typeName, string product, string stateName = null, bool isProject = false);

    /// <summary> Вставляет новое или существующее изделие в редактируемый объект. </summary>
    /// <param name="parent"> Объект родителя. </param>
    /// <param name="child"> Объект потомка. </param>
    /// <param name="linkType"> Название типа связи. </param>
    /// <param name="stateName"> Состояние вновь создаваемого объекта (если создается объект). </param>
    /// <param name="reuse"> Устанавливает возможность повторного применения объекта. </param>
    /// <remarks> <br/>** reuse:
    /// <br/> Если значение - true, то независимо от существования в базе данных такой пары связанных объектов будет создаваться еще один экземпляр связи.
    /// <br/> Если значение - false, то при существовании в базе данных такой пары связанных объектов новый экземпляр связи создаваться не будет, при этом будет вызвано исключение.
    /// </remarks>
    /// <returns>Возвращает идентификатор созданной связи.</returns>
    int InsertObject(ILObject parent, ILObject child, string linkType, string stateName = null, bool reuse = false);

    /// <summary> Вставляет новое или существующее изделие в редактируемый объект. </summary>
    /// <param name="parentTypeName"> Тип объекта-родителя. </param>
    /// <param name="parentProduct"> Значение ключевого атрибута объекта-родителя. </param>
    /// <param name="parentVersion"> Номер версии объекта-родителя. </param>
    /// <param name="childTypeName"> Тип объекта-потомка. </param>
    /// <param name="childProduct"> Значение ключевого атрибута объекта-потомка. </param>
    /// <param name="childVersion"> Номер версии объекта-потомка. </param>
    /// <inheritdoc cref="InsertObject(ILObject, ILObject, string, string, bool)"/>
    int InsertObject(string parentTypeName, string parentProduct, string parentVersion, string linkType, string childTypeName, string childProduct, string childVersion = " ", string stateName = null, bool reuse = false);

    /// <summary> Добавляет связь между объектами. </summary>
    /// <param name="parent"> Объект родителя. </param>
    /// <param name="child"> Объект потомка. </param>
    /// <param name="linkType"> Название типа связи. </param>
    /// <param name="minQuantity"> Нижняя граница количества. </param>
    /// <param name="maxQuantity"> Верхняя граница количества. </param>
    /// <param name="unitId"> Уникальный идентификатор единицы измерения. </param>
    /// <returns> Возвращает новую связь. </returns>
    LLink NewLink(ILObject parent, ILObject child, string linkType, double minQuantity = 0, double maxQuantity = 0, string unitId = null);

    /// <summary> Добавляет связь между объектами. </summary>
    /// <param name="parentId"> Идентификатор версии объекта-родителя. </param>
    /// <param name="childId"> Идентификатор версии объекта-потомка. </param>
    /// <inheritdoc cref="NewLink(ILObject, ILObject, string, double, double, string)"/>
    int NewLink(int parentId, int childId, string linkType, double minQuantity = 0, double maxQuantity = 0, string unitId = null);

    /// <summary> Добавляет связь между объектами. </summary>
    /// <param name="parentTypeName"> Тип объекта-родителя. </param>
    /// <param name="parentProduct"> Значение ключевого атрибута объекта-родителя. </param>
    /// <param name="parentVersion"> Номер версии объекта-родителя. </param>
    /// <param name="childTypeName"> Тип объекта-потомка. </param>
    /// <param name="childProduct"> Значение ключевого атрибута объекта-потомка. </param>
    /// <param name="childVersion"> Номер версии объекта-потомка. </param>
    /// <inheritdoc cref="NewLink(ILObject, ILObject, string, double, double, string)"/>
    int NewLink(string parentTypeName, string parentProduct, string parentVersion, string childTypeName, string childProduct, string childVersion, string linkType, double minQuantity = 0, double maxQuantity = 0, string unitId = null);

    /// <summary> Обновляет связь между объектами. </summary>
    /// <param name="idLink"> Идентификатор связи. </param>
    /// <param name="minQuantity"> Нижняя граница количества. </param>
    /// <param name="maxQuantity"> Верхняя граница количества. </param>
    /// <param name="unitId"> Уникальный идентификатор единицы измерения. </param>
    void UpLink(int idLink, double minQuantity = 0, double maxQuantity = 0, string unitId = null);

    /// <summary> Удаляет связь между объектами. </summary>
    /// <param name="idLink"> Идентификатор связи. </param>
    void DeleteLink(int idLink);


    /// <summary> Возвращает список связей, по заданной связи. </summary>
    /// <param name="lObject"> Объект Лоцман. </param>
    /// <param name="linkType"> Название типа связи. </param>
    /// <param name="inverse"> Направление (true - обратное, false - прямое). </param>
    IEnumerable<ILLink> GetLinkedFast(ILObject lObject, string linkType, bool inverse = false);

    /// <summary> Возвращает список связанных объектов для отображения в дереве. </summary>
    /// <param name="lObject"> Объект Лоцман. </param>
    /// <param name="linkTypeNames"> Список связей. </param>
    /// <param name="inverse"> Направление (true - обратное, false - прямое). </param>
    IEnumerable<ILLink> GetTree(ILObject lObject, IEnumerable<string> linkTypeNames, bool inverse = false);

    /// <summary> Получение атрибутов объекта, включая служебные. </summary>
    /// <param name="lObject"> Объект Лоцман. </param>
    /// <returns> Возвращает атрибуты объекта, включая служебные. </returns>
    IEnumerable<ILAttribute> GetAttributes(ILObject lObject);

    /// <summary> Получение атрибутов связи, включая служебные. </summary>
    /// <param name="link"> Связь Лоцман. </param>
    /// <returns> Возвращает атрибуты связи, включая служебные. </returns>
    IEnumerable<ILAttribute> GetLinkAttributes(ILLink link);

    /// <summary> Приводит значение к заданной единице измерения. </summary>
    /// <param name="value"> Значение. </param>
    /// <param name="sourceMeasureUnit"> Исходная единица измерения. </param>
    /// <param name="destMeasureUnit"> Требуемая единица измерения. </param>
    /// <returns>Возвращает преобразованное значение.</returns>
    double ConverseValue(double value, LMeasureUnit sourceMeasureUnit, LMeasureUnit destMeasureUnit);

    /// <summary> Изменяет состояние объекта. </summary>
    /// <param name="objectId"> Идентификатор версии объекта. </param>
    /// <param name="state"> Состояние. </param>
    void UpdateState(int objectId, LStateInfo state);

    /// <summary> Добавляет, удаляет, обновляет значение атрибута объекта. </summary>
    /// <param name="objectId"> Идентификатор версии объекта. </param>
    /// <param name="attributeName"> Название атрибута. </param>
    /// <param name="attributeValue"> Значение атрибута. Если null или string.Empty то атрибут будет помечен на удаление. </param>
    /// <param name="measureUnit"> Единица измерения. </param>
    void UpAttrValueById(int objectId, string attributeName, object attributeValue, LMeasureUnit measureUnit);

    /// <summary> Добавляет, удаляет, обновляет значение атрибута связи. </summary>
    /// <param name="idLink"> Идентификатор связи. </param>
    /// <param name="attributeName"> Название атрибута. </param>
    /// <param name="attributeValue"> Значение атрибута. Если null или string.Empty то атрибут будет помечен на удаление. </param>
    /// <param name="measureUnit"> Единица измерения. </param>
    void UpLinkAttrValueById(int idLink, string attributeName, object attributeValue, LMeasureUnit measureUnit);

    /// <summary> Регистрирует в базе данных файл, находящийся на рабочем диске пользователя. </summary>
    /// <param name="documentId"> Идентификатор версии объекта. </param>
    /// <param name="fileName"> Название файла (должен быть уникальным). </param>
    /// <param name="relativePath"> Путь к файлу относительно диска из настройки "Буква рабочего диска", доступный серверу приложений. </param>  
    /// <param name="filePath"> Путь к исходному файлу. </param>  
    /// <remarks> Если путь указан не на рабочий диск Лоцмана <see cref="LUser.WorkDir"/>, то файл будет скопирован на рабочий диск. </remarks>
    string RegistrationOfFile(int documentId, string fileName, string relativePath, string filePath);

    /// <param name="typeName"> Название типа. </param>
    /// <param name="product"> Ключевой атрибут. </param>
    /// <param name="version"> Версия объекта. </param>
    /// <inheritdoc cref="RegistrationOfFile(int, string, string, string)"/>
    string RegistrationOfFile(string typeName, string product, string version, string fileName, string relativePath, string filePath);

    /// <summary> Сохраняет вторичное представление документа. </summary>
    /// <param name="documentId"> Идентификатор версии объекта. </param>
    /// <param name="filePath"> Путь к файлу вторичного представления, относительно диска из настройки "Буква рабочего диска", доступный серверу приложений. </param>        
    /// <param name="removeAfterSave"> Признак удаления файла с рабочего диска, после сохранения. </param>        
    /// <remarks> Если путь указан не на рабочий диск Лоцмана <see cref="LUser.WorkDir"/>, то файл будет скопирован на рабочий диск. </remarks>
    void SaveSecondaryView(int documentId, string filePath, bool removeAfterSave = true);

    /// <summary> Возвращает признак того что объект с задаными параметрами уже существует. </summary>
    /// <param name="typeName"> Название типа. </param>
    /// <param name="name"> Ключевой атрибут. </param>
    /// <returns> Возвращает признак существования объекта с задаными параметрами. </returns>
    bool CheckUniqueName(string typeName, string name);

    /// <summary> Возвращает признак того что файл и путь с задаными параметрами уже существует. </summary>
    /// <param name="fileName"> Название файла. </param>
    /// <param name="relativePath"> Путь к файлу относительно диска из настройки «Буква рабочего диска». </param>
    /// <returns> Возвращает признак существования файла с задаными параметрами. </returns>
    bool CheckUniqueFileName(string fileName, string relativePath);

    /// <summary> Проверяет уникальность ключевого атриубта для данного типа и возвращает уникальное наименование. </summary>
    /// <param name="typeName"> Название типа. </param>
    /// <param name="name"> Ключевой атрибут. </param>
    /// <returns> Возвращает уникальный ключевой атрибут для заданного типа. </returns>
    string GetUniqueName(string typeName, string name);

    /// <summary> Проверяет уникальность наименования файла относительно пути и возвращает уникальное наименование. </summary>
    /// <param name="fileName"> Название файла. </param>
    /// <param name="relativePath"> Путь к файлу относительно диска из настройки «Буква рабочего диска». </param>
    /// <returns> Возвращает уникальное наименование файла. </returns>
    string GetUniqueFileName(string fileName, string relativePath);

    /// <summary> Возвращает данные для формирования отчета. </summary>
    /// <param name="reportName"> Название хранимой процедуры. </param>
    /// <param name="objectsIds"> Список идентификаторов версий объектов. </param>
    /// <param name="reportParams"> Параметры отчёта формата "параметр1=значение1;параметр2=значение2". </param>        
    /// <remarks> Параметры отчёта должны быть формата "параметр1=значение1;параметр2=значение2"</remarks>
    DataTable GetReport(string reportName, IEnumerable<int> objectsIds = null, string reportParams = null);

    /// <summary> Возвращает объекты с заполнеными свойствами. </summary>
    /// <param name="objectsIds"> Список идентификаторов версий объектов. </param>
    IEnumerable<ILObject> GetPropObjects(IEnumerable<int> objectsIds);

    /// <summary> Проверка на существование бизнес объекта в базе Лоцман. </summary>
    /// <param name="typeName"> Название типа. </param>
    /// <param name="uniqueId"> Ключевой атрибут формата ***BOSimple. </param>
    /// <remarks> Если объект не существует то свойство возвращаемого объекта ILObject.Id будет равен 0.
    /// <br/>** Для создания бизнес объекта необходимо чтобы product был в формате ***BOSimple
    /// </remarks>
    ILObject PreviewBoObject(string typeName, string uniqueId);

    /// <summary> Возвращает список идентификаторов версий объектов, заблокированных в текущем чекауте. </summary>
    IEnumerable<int> GetLockedObjectsIds();

    /// <summary> Помечает объект, находящийся на изменении, как подлежащий удалению при возврате из работы. </summary>
    /// <param name="objectId"> Идентификатор версии объекта. </param>
    void KillVersion(int objectId);

    /// <summary> Помечает объекты, находящиеся на изменении, как подлежащий удалению при возврате из работы. </summary>
    /// <param name="objectsIds"> Список идентификаторов версий объектов. </param>
    void KillVersion(IEnumerable<int> objectsIds);

    /// <summary> Помечает объект, находящийся на изменении, как подлежащий удалению при возврате из работы. </summary>
    /// <param name="typeName"> Название типа. </param>
    /// <param name="product"> Ключевой атрибут. </param>
    /// <param name="version"> Версия объекта. </param>
    void KillVersion(string typeName, string product, string version);

    /// <summary> Берет объект на редактирование. </summary>
    /// <param name="typeName"> Название типа. </param>
    /// <param name="product"> Ключевой атрибут. </param>
    /// <param name="version"> Версия объекта. </param>
    /// <param name="mode"> Режим. </param>
    /// <returns> Возвращает внутреннее название редактируемого объекта (название чекаута). </returns>
    /// <remarks> При пустых значениях typeName, product и version, будет создан пустой чекаут. С ним можно работать, как с обычным. 
    /// Отличие в том, что в списке, возвращаемым методом GetInfoAboutCurrentBase (режим 2) в качестве значений полей[_ID_VERSION], [_TYPE], [_PRODUCT], [_VERSION] будут пустые значения.
    /// <para>** Для дальнейшей работы с объектом необходимо подключиться к чекауту методом ConnectToCheckOut.</para>
    /// </remarks>
    string CheckOut(string typeName = null, string product = null, string version = null, CheckOutMode mode = CheckOutMode.Default);

    /// <summary> Делает объект доступным для изменения только в рамках текущего чекаута (блокирует его). </summary>
    /// <param name="objectId"> Идентификатор версии. </param>
    /// <param name="isRoot"> Признак головного объекта. </param>
    void AddToCheckOut(int objectId, bool isRoot = false);
    
    /// <summary> Берет в работу текущий SelectedObject, если он уже находится в работе просто возращает PluginCall.CheckOut. Будет автоматически подключен к чекауту методом ConnectToCheckOut. </summary>
    /// <returns> Возвращает внутреннее название редактируемого объекта (название чекаута). </returns>
    string SelectedObjectCheckOut(CheckOutMode mode = CheckOutMode.Default);

    /// <summary> Подключается к указанному чекауту. </summary>
    /// <param name="checkOutName"> Название чекаута. </param>
    /// <param name="dBName"> Название базы данных. </param>
    /// <remarks> При пустых значении: <paramref name="checkOutName"/> - используется <see cref="CheckOutName"/>, dBName - используется <see cref="IPluginCall.DBName"/>. </remarks>
    void ConnectToCheckOut(string checkOutName = null, string dBName = null);
    
    /// <summary> Отключается от редактируемого объекта (чекаута). /summary>
    /// <inheritdoc cref="ConnectToCheckOut(string, string)"/>
    void DisconnectToCheckOut(string checkOutName = null, string dBName = null);

    /// <summary> Сохранияет изменения и возвращает чекаут из работы. </summary>
    /// <inheritdoc cref="ConnectToCheckOut(string, string)"/>
    void CheckIn(string checkOutName = null, string dBName = null);

    /// <summary> Сохраняет изменения в базу данных. </summary>
    /// <inheritdoc cref="ConnectToCheckOut(string, string)"/>
    void SaveChanges(string checkOutName = null, string dBName = null);

    /// <summary> Выполняет отказ от изменения объекта. </summary>
    /// <inheritdoc cref="ConnectToCheckOut(string, string)"/>
    void CancelCheckOut(string checkOutName = null, string dBName = null);

    /// <summary> Возвращает ссылку бизнес-объекта в справочнике. </summary>
    /// <param name="objectId"> Идентификатор версии. </param>
    string GetBOLocation(int objectId);

    /// <summary> Возвращает имя владельца и дату создания версии. </summary>
    /// <param name="objectId"> Идентификатор версии. </param>
    CreationInfo GetCreationInfo(int objectId);

    /// <summary> Возвращает список файлов, прикрепленных к документу. </summary>
    /// <param name="lObject"> Объект. </param>
    /// <returns> Возвращает список файлов, прикрепленных к документу, в случе если <paramref name="lObject"/> не является документом то вернёт пустой список без запроса. </returns>
    IEnumerable<LFile> GetFiles(ILObject lObject);

    /// <summary> Выгружает файл из системы ЛОЦМАН:PLM.
    /// <br/>В режиме базы данных копирует файл в зарезервированную временную папку пользователя.
    /// <br/>В режиме редактирования объектов копирует файл на рабочий диск (настройка «Буква рабочего диска») пользователя.
    /// </summary>
    /// <param name="lObject"> Объект. </param>
    /// <param name="fileName"> Название файла (должен быть уникальным). </param>
    /// <param name="relativePath"> Путь к файлу относительно диска из настройки "Буква рабочего диска". </param>  
    /// <returns>
    /// В режиме базы данных возвращает полное имя файла с путем. Например: result='\\Server\DOMEN#USER\Temp\FileName.ext'.
    /// <br/>В режиме редактирования объектов возвращает имя файла на рабочем диске. Например: result='Х:\Folder\SubFolder\FileName.ext'.
    /// </returns>
    string GetFile(ILObject lObject, string fileName, string relativePath);
  }
}
