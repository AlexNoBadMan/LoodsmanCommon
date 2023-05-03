﻿namespace LoodsmanCommon
{
  /// <summary>
  /// Режим возврата структурного списока подразделений, должностей и пользователей. 
  /// </summary>
  public enum GetRoleTreeMode
  {
    Mode1 = 1,
    Mode2 = 2,
    Mode3 = 3,
    Mode4 = 4,
    Mode5 = 5,
  }
  /// <summary>
  /// Режим возврата информации об экземпляре связи. 
  /// </summary>
  public enum GetInfoAboutLinkMode
  {
    /// <summary>
    /// Возвращает общую информацию об экземпляре связи.
    /// <br/>
    /// <br/>Возвращает набор данных с полями:
    /// <br/>[_ID] int - уникальный идентификатор экземпляра связи;
    /// <br/>[_NAME] string - тип связи;
    /// <br/>[_MIN_QUANTITY] double - нижняя граница количества;
    /// <br/>[_MAX_QUANTITY] double - верхняя граница количества;
    /// <br/>[_ID_UNIT] string - идентификатор единицы измерения, в которой отображается значение количества;
    /// <br/>[_UNIT] string - название единицы измерения, в которой отображается значение количества;
    /// <br/>[_ID_MEASURE] string - идентификатор сущности, которая измерена количеством;
    /// <br/>[_MEASURE] string - название сущности, которая измерена количеством.
    /// </summary>
    Mode1 = 1,
    /// <summary>
    /// Возвращает объекты, которые связаны данным экземпляром связи.
    /// <br/>
    /// <br/>Возвращает набор данных с полями:
    /// <br/>[_NAME] string - тип связи;
    /// <br/>[_ID_PARENT] int - уникальный идентификатор объекта-родителя;
    /// <br/>[_PARENT_TYPE] string - название типа объекта-родителя;
    /// <br/>[_PARENT_PRODUCT] string - ключевой атрибут объекта-родителя;
    /// <br/>[_PARENT_VERSION] string - версия объекта объекта-родителя;
    /// <br/>[_ID_CHILD] int - уникальный идентификатор объекта-потомка;
    /// <br/>[_CHILD_TYPE] string - название типа объекта-потомка;
    /// <br/>[_CHILD_PRODUCT] string - ключевой атрибут объекта-потомка;
    /// <br/>[_CHILD_VERSION] string - версия объекта объекта-потомка.
    /// </summary>
    Mode2 = 2,
  }

  /// <summary>
  /// Режим возврата информации о версии объекта.
  /// </summary>
  public enum GetInfoAboutVersionMode
  {
    /// <summary>
    /// Возвращает атрибуты объекта.
    /// <br/>
    /// <br/>Возвращает набор данных с полями:
    /// <br/>[_ID] int - уникальный идентификатор значения атрибута;
    /// <br/>[_NAME] string - название атрибута;
    /// <br/>[_VALUE] string - значение атрибута;
    /// <br/>[_ATTRTYPE] int - тип атрибута;
    /// <br/>[_ACCESSLEVEL] int - уровень доступа (1 - Только чтение, 2 - Чтение/запись). Начиная с версии ЛОЦМАН:PLM 2017 поле устарело и его значение всегда равно 2;
    /// <br/>[_ID_UNIT] string - идентификатор единицы измерения, в которой возвращается значение атрибута;
    /// <br/>[_UNIT] string - название единицы измерения, в которой возвращается значение атрибута;
    /// <br/>[_ID_MEASURE] string - идентификатор сущности, которую измерил данный атрибут;
    /// <br/>[_MEASURE] string - название сущности, которую измерил данный атрибут.
    /// </summary>
    Mode2 = 2,
    /// <summary>
    /// <br/>Возвращает атрибуты объекта, включая служебные. (поддерживается в версии ЛОЦМАН:PLM 2012 и более поздних)
    /// <br/>
    /// <br/>Возвращает набор данных с полями:
    /// <br/>[_ID] int - уникальный идентификатор значения атрибута;
    /// <br/>[_NAME] string - название атрибута;
    /// <br/>[_VALUE] string - значение атрибута;
    /// <br/>[_ATTRTYPE] int - тип атрибута;
    /// <br/>[_ACCESSLEVEL] int - уровень доступа (1 - Только чтение, 2 - Чтение/запись). Начиная с версии ЛОЦМАН:PLM 2017 поле устарело и его значение всегда равно 2;
    /// <br/>[_ID_UNIT] string - идентификатор единицы измерения, в которой возвращается значение атрибута;
    /// <br/>[_UNIT] string - название единицы измерения, в которой возвращается значение атрибута;
    /// <br/>[_ID_MEASURE] string - идентификатор сущности, которую измерил данный атрибут;
    /// <br/>[_MEASURE] string - название сущности, которую измерил данный атрибут.
    /// <br/>[_SYSTEM] int - признак того, что атрибут является служебным (0 - Обычный, 1 - Служебный).
    /// </summary>
    Mode3 = 3,
    /// <summary>
    /// Возвращает список файлов, прикрепленных к документу.
    /// <br/>
    /// <br/>Возвращает набор данных с полями:
    /// <br/>[_ID_FILE] int - уникальный идентификатор файла в системе;
    /// <br/>[_NAME] string - имя файла;
    /// <br/>[_LOCALNAME] string - путь к файлу относительно диска из настройки «Буква рабочего диска»;
    /// <br/>[_DATEOFCREATE] datetime - дата и время создания файла;
    /// <br/>[_SIZE] int - размер файла (если метод вызывается в контексте рабочего проекта, в котором файл выгружался на рабочий диск, то возвращается размер файла на рабочем диске, в противном случае - размер файла на момент последнего сохранения в рабочем проекте);
    /// <br/>[_CRC] long - контрольная сумма содержимого файла на момент последнего сохранения в рабочем проекте;
    /// <br/>[_MODIFIED] datetime - дата последнего изменения файла ( если метод вызывается в контексте рабочего проекта, в котором файл выгружался на рабочий диск, то возвращается дата последнего изменения файла на рабочем диске, в противном случае - дата последнего изменения файла на момент последнего сохранения в рабочем проекте);
    /// <br/>[_READONLY] int - зарезервировано.
    /// </summary>
    Mode7 = 7,
    /// <summary>
    /// ** Этот режим существует лишь для совместимости с клиентскими приложениями ранних версий. Новые приложения должны вызывать <see cref="Mode22">режим 22</see>.
    /// <br/>Возвращает список пользователей, которым назначены директивные права на объект.
    /// <br/>
    /// <br/>Возвращает набор данных с полями:
    /// <br/>[_NAME] string - имя пользователя;
    /// <br/>[_FULLNAME] string - полное имя пользователя;
    /// <br/>[_ACCESSLEVEL] int - уровень доступа к объекту (0 - Нет доступа, 1 - Только чтение, 2 - Чтение/запись, 3 - Полный доступ);
    /// <br/>[_FINISHTIME] datetime - дата окончания действия прав доступа. Начиная с версии ЛОЦМАН:PLM 2017 поле устарело и его значение всегда равно NULL;
    /// <br/>[_OWNER] int - является ли пользователь владельцем объекта (0 - нет, 1 - да);
    /// <br/>[_IS_GROUP] int - является ли ролью (1 - является ролью, 0 - является пользователем). Начиная с версии ЛОЦМАН:PLM 2017 поле устарело и его значение всегда равно 0.
    /// </summary>
    Mode8 = 8,
    /// <summary>
    /// Возвращает список состояний, в которые может перейти данный объект.
    /// <br/>
    /// <br/>Возвращает набор данных с полями:
    /// <br/>[_NAME] string - название состояния.
    /// </summary>
    Mode9 = 9,
    /// <summary>
    /// Возвращает кратчайший путь к версии в дереве.
    /// <br/>
    /// <br/>Возвращает набор данных с полями:
    /// <br/>[_ID_VERSION] int - уникальный идентификатор объекта;
    /// <br/>[_LINKTYPE] string - название типа связи;
    /// <br/>[_LEVEL] int - глубина.
    /// </summary>
    Mode10 = 10,
    /// <summary>
    /// ** Этот режим существует лишь для совместимости с клиентскими приложениями ранних версий. Новые приложения должны вызывать <see cref="Mode23">режим 23</see>.
    /// <br/>Возвращает права на объект всех пользователей базы данных, имеющих ролевые права на тип и состояние объекта.
    /// <br/>
    /// <br/>Возвращает набор данных с полями:
    /// <br/>[_NAME] string - имя пользователя;
    /// <br/>[_FULLNAME] string - полное имя пользователя;
    /// <br/>[_ACCESSLEVEL] int - уровень доступа к объекту (0 - Нет доступа, 1 - Только чтение, 2 - Чтение/запись, 3 - Полный доступ);
    /// <br/>[_MAXACCESSLEVEL] int - максимально возможные права на объект;
    /// <br/>[_IS_GROUP] int - является ли ролью (1 - роль, 0 - пользователь). Начиная с версии ЛОЦМАН:PLM 2017 поле устарело и его значение всегда равно 0.
    /// </summary>
    Mode11 = 11,
    /// <summary>
    /// Возвращает список пользователей, у которых объект находится на изменении.
    /// <br/>
    /// <br/>Возвращает набор данных с полями:
    /// <br/>[_NAME] string - имя пользователя;
    /// <br/>[_FULLNAME] string - полное имя пользователя;
    /// <br/>[_DATE] datetime - дата и время взятия на изменение;
    /// <br/>[_COMMENTS] string - комментарий;
    /// <br/>[_LOCKED] int - уровень блокировки объекта (0 - Чтение, 1 - Изменение);
    /// <br/>[_CHECKOUTNAME] string - название чекаута, в котором блокирован объект.
    /// </summary>
    Mode12 = 12,
    /// <summary>
    /// Возвращает имя владельца и дату создания версии.
    /// <br/>
    /// <br/>Возвращает набор данных с полями:
    /// <br/>[_NAME] string - имя пользователя;
    /// <br/>[_FULLNAME] string - полное имя пользователя;
    /// <br/>[_DATEOFCREATE] datetime - дата и время создания версии.
    /// </summary>
    Mode13 = 13,
    /// <summary>
    /// Возвращает информацию об объекте.
    /// <br/>Смотрите также описание метода <seealso cref="NetPluginCallExtensions.Native_GetPropObjects(Ascon.Plm.Loodsman.PluginSDK.INetPluginCall, System.Collections.Generic.IEnumerable{int})">GetPropObjects</seealso>.
    /// <br/>
    /// <br/>Возвращает набор данных с полями:
    /// <br/>[_ID_VERSION] int - уникальный идентификатор объекта;
    /// <br/>[_TYPE] string - название типа;
    /// <br/>[_PRODUCT] string - ключевой атрибут;
    /// <br/>[_VERSION] string - версия объекта;
    /// <br/>[_STATE] string - состояние объекта;
    /// <br/>[_ACCESSLEVEL] int - уровень доступа к объекту (1 - Только чтение, 2 - Чтение/запись, 3 - Полный доступ);
    /// <br/>[_LOCKED] int - уровень блокировки объекта (0 - не блокирован, 1 - блокирован текущим пользователем, 2 - блокирован другим пользователем);
    /// <br/>[_DOCUMENT] int - является ли документом (1 - является, 0 - не является).
    /// </summary>
    Mode15 = 15,
    /// <summary>
    /// Возвращает параметр Location и класс справочника для внешнего объекта. Если объект не является внешним, то набор данных возвращается пустым.
    /// <br/>
    /// <br/>Возвращает набор данных с полями:
    /// <br/>[_NAME] string - параметр Location;
    /// <br/>[_CLASS_ID] int - идентификатор класса справочника;
    /// <br/>[_CLASS] string - имя класса справочника.
    /// </summary>
    Mode16 = 16,
    /// <summary>
    /// Возвращает тело цифровой подписи.
    /// <br/>Смотрите также описание метода **TODO: добавить реализацию метода "SignObject".
    /// <br/>
    /// <br/>Возвращает набор данных с полями:
    /// <br/>[_SIGN] image - тело подписи;
    /// <br/>[_SIGN_TYPE] int - тип подписи: 0 - усиленная, 1 - простая; 
    /// <br/>[_SIGN_VERSION] int - версия подписи: 
    /// <br/>0 - устаревшая версия (подпись поставлена в ЛОЦМАН:PLM 2013 или в предыдущих версиях системы), 
    /// <br/>1 - новая версия (подпись поставлена в ЛОЦМАН:PLM 2014 или в более поздних версиях системы).
    /// </summary>
    Mode19 = 19,
    /// <summary>
    /// Возвращает варианты замены.
    /// <br/>
    /// <br/>Возвращает набор данных с полями:
    /// <br/>[_ID_GROUPCHANGE] int - идентификатор группы замены;
    /// <br/>[_GROUPCHANGE_NAME] string - имя группы замены;
    /// <br/>[_GROUP_IN_CURRENCONF] int - признак того, что группа замены входит в текущую конфигурацию;
    /// <br/>[_ID_VARIANT] int - идентификатор варианта замены;
    /// <br/>[_VARIANT_NAME] string - имя варианта замены;
    /// <br/>[_BASIC] int - признак основного варианта (1 - является основным, 0 - не является основным);
    /// <br/>[_VARIANT_IN_CURRENCONF] int - признак того, что вариант замены входит в текущую конфигурацию;
    /// <br/>[_ID_LINK] int - идентификатор экземпляра связи;
    /// <br/>[_LINKTYPE] string - тип связи;
    /// <br/>[_ID_PARENT] int - идентификатор объекта-родителя;
    /// <br/>[_ID_VERSION] int - идентификатор версии;
    /// <br/>[_ID_TYPE] int - идентификатор типа объекта;
    /// <br/>[_ID_STATE] int - идентификатор состояния объекта;
    /// <br/>[_PRODUCT] string - ключевой атрибут;
    /// <br/>[_VERSION] string - версия объекта;
    /// <br/>[_ID_LOCK] int - идентификатор чекаута, в котором блокирован объект (если объект не блокирован, то вернется null);
    /// <br/>[_ACCESSLEVEL] int - уровень доступа к объекту (1 - Только чтение, 2 - Чтение/запись, 3 - Полный доступ).
    /// </summary>
    Mode20 = 20,
    /// <summary>
    /// Возвращает список пользователей, должностей и организационных единиц, которым назначены директивные права на заданный объект. (поддерживается в версии ЛОЦМАН:PLM 2017 и более поздних)
    /// <br/>
    /// <br/>Возвращает набор данных с полями:
    /// <br/>[_ID] int - идентификатор субъекта;
    /// <br/>[_NAME] string - имя субъекта;
    /// <br/>[_FULLNAME] string - полное имя субъекта;
    /// <br/>[_ACCESSLEVEL] int - действительный уровень доступа субъекта к объекту (0 - Нет доступа, 1 - Только чтение, 2 - Чтение/запись, 3 - Полный доступ);
    /// <br/>[_APPOINTEDACCESSLEVEL] int - назначенный уровень доступа к объекту (0 - Нет доступа, 1 - Только чтение, 2 - Чтение/запись, 3 - Полный доступ);
    /// <br/>[_OWNER] int - является ли субъект владельцем объекта (0 - нет, 1 - да);
    /// <br/>[_TYPE] int - тип субъекта:
    /// <br/>0 - пользователь;
    /// <br/>1 - должность;
    /// <br/>2 - организационная единица.
    /// </summary>
    Mode22 = 22,
    /// <summary>
    /// Если текущий пользователь имеет права Полный доступ на заданный объект, то выдаются пользователи и должности, имеющие ролевые права на тип и состояние объекта, а также должности, которым не назначена ни одна роль, и все организационные единицы; если текущий пользователь имеет права Чтение/запись и ниже на заданный объект, то выдается пустой выходной набор. (поддерживается в версии ЛОЦМАН:PLM 2017 и более поздних)
    /// <br/>
    /// <br/>Возвращает набор данных с полями:
    /// <br/>[_ID] int - идентификатор субъекта;
    /// <br/>[_NAME] string - имя субъекта;
    /// <br/>[_FULLNAME] string - полное имя субъекта;
    /// <br/>[_ACCESSLEVEL] int - уровень доступа субъекта к объекту (0 - Нет доступа, 1 - Только чтение, 2 - Чтение/запись, 3 - Полный доступ);
    /// <br/>[_DEFACCESSLEVEL] int - права доступа по умолчанию субъекта к объекту;
    /// <br/>[_MAXACCESSLEVEL] int - максимальные права субъекта к объекту;
    /// <br/>[_APPOINTEDACCESSLEVEL] int - уровень назначенного директивного доступа субъекта к объекту (0 - Нет доступа, 1 - Только чтение, 2 - Чтение/запись, 3 - Полный доступ);
    /// <br/>[_TYPE] int - тип субъекта:
    /// <br/>0 - пользователь;
    /// <br/>1 - должность;
    /// <br/>2 - организационная единица.
    /// </summary>
    Mode23 = 23,
  }

  /// <summary>
  /// Режим возврата информации о типе.
  /// </summary>
  public enum GetInfoAboutTypeMode
  {
    /// <summary>
    /// Возвращает список возможных атрибутов типа.
    /// <br/>Возвращает набор данных с полями:
    /// <br/>[_ID] int - уникальный идентификатор значения атрибута;
    /// <br/>[_NAME] string - название атрибута;
    /// <br/>[_ATTRTYPE] int - тип атрибута;
    /// <br/>[_DEFAULT] string - значение по умолчанию;
    /// <br/>[_LIST] text - список возможных значений;
    /// <br/>[_ACCESSLEVEL] int -  уровень прав доступа (1 - Только чтение, 2 - Чтение/запись). Начиная с версии ЛОЦМАН:PLM 2017 поле устарело и его значение всегда равно 2;
    /// <br/>[_ONLYLISTITEMS] int - атрибут может принимать значения только из списка (0 - Любое, 1 - Из списка);
    /// <br/>[_SYSTEM] int - признак того, что атрибут является служебным, всегда равен 0;
    /// <br/>[_OBLIGATORY] int - признак «Обязательный» (0 - необязательный, 1 - обязательный).
    /// </summary>
    Mode1 = 1,
    /// <summary>
    /// Возвращает список возможных состояний типа без учета доступа к типу и состоянию.
    /// <br/>Возвращает набор данных с полями:
    /// <br/>[_NAME] string - название состояния;
    /// <br/>[_VERSIONING] int - зарезервировано.
    /// </summary>
    Mode2 = 2,
    /// <summary>
    /// Возвращает список возможных состояний типа с учетом доступа к типу и его состояниям.
    /// <br/>Возвращает набор данных с полями:
    /// <br/>[_NAME] string - название состояния.
    /// </summary>
    Mode3 = 3,
    /// <summary>
    /// Возвращает все типы, с которыми может быть связан объект заданного типа любыми видами связей и в любом направлении.
    /// <br/>Возвращает набор данных с полями:
    /// <br/>[_NAME] string - название типа;
    /// <br/>[_ID_LINKTYPE] int - идентификатор типа связи;
    /// <br/>[_LINKTYPE] string - тип связи;
    /// <br/>[_INVERSENAME] string - обратное название типа связи;
    /// <br/>[_LINKICON] image - картинка для типа связи;
    /// <br/>[_LINKORDER] int - порядок следования;
    /// <br/>[_LINKKIND] int - вид связи (1 - горизонтальная, 0 - вертикальная);
    /// <br/>[_DIRECTION] int - направление, может быть связан c типом:
    /// <br/>-1 - только в обратном направлении;
    /// <br/>1 - только в прямом направлении;
    /// <br/>0 - в прямом и обратном направлении;
    /// <br/>[_CANCREATE] int - признак существования привилегии для создания объекта:
    /// <br/>1 - создание объекта возможно;
    /// <br/>0 - создание объекта запрещено;
    /// <br/>[_IS_QUANTITY] int - признак количественной связи:
    /// <br/>0 - в любом направлении связь неколичественная;
    /// <br/>1 - если возвращенный тип выступает в роли вышестоящего для заданного типа;
    /// <br/>2 - если возвращенный тип выступает в роли нижестоящего для заданного типа;
    /// <br/>3 - в любом направлении связь количественная.
    /// </summary>
    Mode4 = 4,
    /// <summary>
    /// Возвращает название инструмента, в котором редактируется документ данного типа. Если инструмент не сопоставлен, то возвратится пустой набор данных.
    /// <br/>Возвращает набор данных с полями:
    /// <br/>[_TOOLNAME] string - название инструмента;
    /// <br/>[_EXTENSION] string - расширение файла, который редактируется в соответствующем инструменте.
    /// </summary>
    Mode6 = 6,
    /// <summary>
    /// Возвращает список карточек для типа в соответствии с привязкой карточки к ролям, назначенным данному пользователю.
    /// <br/>Если на вход подано stTypeName - заведомо несуществующее, то на выходе придут все карточки для пользователя.
    /// <br/>Если карточка не привязана ни к одной роли, то на выходе ее не будет.
    /// <br/>Возвращает набор данных с полями:
    /// <br/>[_ID] int - идентификатор карточки;
    /// <br/>[_NAME] string - название карточки;
    /// <br/>[_TYPE] string - название типа, которому сопоставлена карточка.
    /// </summary>
    Mode7 = 7,
    /// <summary>
    /// Возвращает список типов, с которыми объект указанного типа может быть связан вертикальными связями.
    /// <br/>Возвращает набор данных с полями:
    /// <br/>[_ID] int - уникальный идентификатор типа;
    /// <br/>[_NAME] string - название типа;
    /// <br/>[_ICON] image - значок типа;
    /// <br/>[_ATTRNAME] string - название ключевого атрибута типа;
    /// <br/>[_LINKTYPE] string - название типа связи.
    /// </summary>
    Mode8 = 8,
    /// <summary>
    /// Возвращает список используемых классов типа.
    /// <br/>Возвращает набор данных с полями:
    /// <br/>[_NAME] string - класс справочника;
    /// <br/>[_PRODUCTSOURCE] string - правило формирования ключевого атрибута для объектов класса;
    /// <br/>[_STATESOURCE] string - условие назначения состояния для объектов класса;
    /// <br/>[_CLASS_ID] int - идентификатор класса справочника;
    /// <br/>[_CAPTION] string - название класса справочника;
    /// <br/>[_NODE] string - идентификатор узла справочника класса.
    /// </summary>
    Mode9 = 9,
    /// <summary>
    /// Возвращает внешние атрибуты типа.
    /// <br/>Возвращает набор данных с полями:
    /// <br/>[_NAME] string - название атрибута;
    /// <br/>[_OBJECTATTRIBUTENAME] string - Начиная с версии ЛОЦМАН:PLM 2018.1 поле устарело и его значение всегда равно 'Deprecated'.
    /// </summary>
    Mode10 = 10,
    /// <summary>
    /// Возвращает список связей, в которых может состоять объект данного типа.
    /// <br/>Если на вход подано stTypeName, заведомо несуществующее, на выходе будут данные для всех типов системы.
    /// <br/>Возвращает набор данных с полями:
    /// <br/>[_TYPE] string - название типа (повторяет то, что пришло на входе);
    /// <br/>[_NAME] string - название типа связи;
    /// <br/>[_DIRECTION] int - направление, может быть связан c типом: (-1 - только в обратном направлении, 1 - только в прямом направлении, 0 - в прямом и обратном направлении);
    /// <br/>[_ID_LINKTYPE] int - идентификатор типа связи;
    /// <br/>[_INVERSENAME] string - обратное название типа связи;
    /// <br/>[_LINKICON] image - значок типа связи;
    /// <br/>[_LINKORDER] int - порядок следования;
    /// <br/>[_LINKKIND] int - вид связи (1 - горизонтальная, 0 - вертикальная).
    /// </summary>
    Mode11 = 11,
    /// <summary>
    /// Возвращает список возможных атрибутов типа, включая служебные. (поддерживается в версии ЛОЦМАН:PLM 2012 и более поздних)
    /// <br/>Возвращает набор данных с полями:
    /// <br/>[_ID] int - уникальный идентификатор значения атрибута;
    /// <br/>[_NAME] string - название атрибута;
    /// <br/>[_ATTRTYPE] int - тип атрибута;
    /// <br/>[_DEFAULT] string - значение по умолчанию;
    /// <br/>[_LIST] text - список возможных значений;
    /// <br/>[_ACCESSLEVEL] int -  уровень прав доступа (1 - Только чтение, 2 - Чтение/запись). Начиная с версии ЛОЦМАН:PLM 2017 поле устарело и его значение всегда равно 2;
    /// <br/>[_ONLYLISTITEMS] int - атрибут может принимать значения только из списка (0 - Любое, 1 - Из списка);
    /// <br/>[_OBLIGATORY] int - признак «Обязательный» (0 - необязательный, 1 - обязательный);
    /// <br/>[_SYSTEM] int - признак того, что атрибут является служебным (0 - Обычный, 1 - Служебный).
    /// </summary>
    Mode12 = 12,
  }

  /// <summary>
  /// Режим возврата списка атрибутов.
  /// </summary>
  public enum GetAttributesMode
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
  /// Направление связи.
  /// </summary>
  public enum LinkDirection
  {
    /// <summary>
    /// Прямая связь.
    /// </summary>
    Forward = 1,
    /// <summary>
    /// Обратная связь.
    /// </summary>
    Backward = -1,
    /// <summary>
    /// Прямая и обратная связь.
    /// </summary>
    ForwardAndBackward = 0
  }

  /// <summary>
  /// Вид связи.
  /// </summary>
  public enum LinkKind
  {
    /// <summary>
    /// Вертикальная.
    /// </summary>
    Vertical = 0,
    /// <summary>
    /// Горизонтальная.
    /// </summary>
    Horizontal = 1,
  }

  /// <summary>
  /// Тип количественной связи по отношению к типу.
  /// </summary>
  public enum LinkQuantityType
  {
    /// <summary>
    /// в любом направлении связь неколичественная.
    /// </summary>
    NonQuantitative = 0,
    /// <summary>
    /// Если возвращенный тип выступает в роли вышестоящего для заданного типа.
    /// </summary>
    OnlyAsParent = 1,
    /// <summary>
    /// Если возвращенный тип выступает в роли нижестоящего для заданного типа.
    /// </summary>
    OnlyAsChild = 2,
    /// <summary>
    /// В любом направлении связь количественная.
    /// </summary>
    Quantitative = 3,
  }

  /// <summary>
  /// Тип субъекта.
  /// </summary>
  public enum SubjectType
  {
    /// <summary>
    /// Пользователь.
    /// </summary>
    User = 0,
    /// <summary>
    /// Должность.
    /// </summary>
    Position = 1,
    /// <summary>
    /// Организационная единица.
    /// </summary>
    OrgUnit = 2,
  }

  /// <summary>
  /// Отражение записи [_OBJECT_TYPE] метода <see cref="NetPluginCallExtensions.Native_GetReportsAndFolders(Ascon.Plm.Loodsman.PluginSDK.INetPluginCall, int)">GetReportsAndFolders</see>.
  /// </summary>
  public enum ReportRecordType
  {
    /// <summary>
    /// Папка.
    /// </summary>
    Folder = 0,
    /// <summary>
    /// Отчет SQL.
    /// </summary>
    SQL = 1,
    /// <summary>
    /// отчет FastReport.
    /// </summary>
    FastReport = 2,
  }

  /// <summary>
  /// Область применения отчета.
  /// </summary>
  public enum ReportTargetType
  {
    /// <summary>
    /// Объекты.
    /// </summary>
    Objects = 0,
    /// <summary>
    /// Бизнес-процессы.
    /// </summary>
    BusinessProcesses = 1,
    /// <summary>
    /// Задания WorkFlow.
    /// </summary>
    TasksWorkFlow = 2,
    /// <summary>
    /// Не определено.
    /// </summary>
    Undefined = 3,
    /// <summary>
    /// Задания СПиУПП.
    /// </summary>
    TasksSPiUPP = 4,
  }

  /// <summary>
  /// Режим чекаута.
  /// </summary>
  public enum CheckOutMode
  {
    /// <summary>
    /// Блокировать головной объект.
    /// </summary>
    Default = 0,
    /// <summary>
    /// Блокировать все привязанные объекты с полной разузловкой вниз.
    /// </summary>
    Through = 1
  }

  /// <summary>
  /// Типы атрибутов.
  /// </summary>
  public enum AttributeType : short
  {
    /// <summary>
    /// Строка.
    /// </summary>
    String = 0,
    /// <summary>
    /// Целое.
    /// </summary>
    Int = 1,
    /// <summary>
    /// Вещественное.
    /// </summary>
    Double = 2,
    /// <summary>
    /// Дата.
    /// </summary>
    DateTime = 3,
    /// <summary>
    /// Текст.
    /// </summary>
    Text = 5,
    /// <summary>
    /// Изображение.
    /// </summary>
    Image = 6
  }

  /// <summary>
  /// Состояния пользователей.
  /// </summary>
  public enum UserState : short
  {
    /// <summary>
    /// Доступен.
    /// </summary>
    Available = 0,
    /// <summary>
    /// Недоступен.
    /// </summary>
    NotAvailable = 1,
    /// <summary>
    /// Уволен.
    /// </summary>
    Fired = 2
  }

  /// <summary>
  /// Типы элементов организационной структуры.
  /// </summary>
  public enum OrganisationUnitKind
  {
    /// <summary>
    /// Пользователь организационной структуры.
    /// </summary>
    User = 0,
    /// <summary>
    /// Должность организационной структуры.
    /// </summary>
    Position = 1,
    /// <summary>
    /// Подразделение организационной структуры.
    /// </summary>
    Department = 2,
    /// <summary>
    /// Головное подразделение организационной структуры.
    /// </summary>
    MainDepartment = 3
  }

}
