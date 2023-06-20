using System.Collections.Generic;

namespace LoodsmanCommon
{
  /// <summary> Метаданные базы ЛОЦМАН:PLM. </summary>
  public interface ILoodsmanMeta
  {
    /// <summary> Возвращает список типов. </summary>
    NamedEntityCollection<LTypeInfo> Types { get; }

    /// <summary> Возвращает список связей. </summary>
    NamedEntityCollection<LLinkInfo> Links { get; }

    /// <summary> Возвращает список состояний. </summary>
    NamedEntityCollection<LStateInfo> States { get; }

    /// <summary> Возвращает список атрибутов. </summary>
    NamedEntityCollection<LAttributeInfo> Attributes { get; }

    /// <summary> Возвращает список содержащий случаи использования прокси, типа объекта и типа документа. </summary>
    IReadOnlyList<LProxyUseCase> ProxyUseCases { get; }

    /// <summary> Возвращает список содержащий информацию о связях между типами. </summary>
    IReadOnlyList<LLinkInfoBetweenTypes> LinksInfoBetweenTypes { get; }

    /// <summary> Возвращает список единиц измерения. </summary>
    NamedEntityCollection<LMeasure> Measures { get; }

    /// <summary> Возвращает список пользователей. Ключом является свойство <see cref="INamedEntity.Name">Name</see>. </summary>
    NamedEntityCollection<LUser> Users { get; }

    /// <summary> Возвращает информацию о текущем пользователе. </summary>
    public LUser CurrentUser { get; }

    /// <summary> Возвращает список подразделений, должностей (не включает пользователей). Пользователей можно получить при помощи свойства <see cref="Users"/>. </summary>
    EntityCollection<LOrganisationUnit> OrganisationUnits { get; }

    /// <summary> Возвращает информацию о текущей головной организационной единице (зависит от основной должности текущего пользователя). </summary>
    /// <remarks> Если текущему пользователю не назначена должность, то свойство вернёт первую в списке головную организационную единицу. </remarks>
    LMainDepartment CurrentMainDepartment { get; }

    /// <summary> Возвращает случай использования прокси. </summary>
    /// <param name="parentType"> Наименование типа объекта-родителя. </param>
    /// <param name="childDocumentType"> Наименование типа документа-потомка. </param>
    /// <param name="extension">Расширение файла (наличие точки не обязательно).</param>
    LProxyUseCase GetProxyUseCase(string parentType, string childDocumentType, string extension);

    /// <summary> Возвращает список измеряемых сущностей атрибута. </summary>
    /// <param name="name"> Название атрибута. </param>
    IEnumerable<LAttributeMeasure> GetAttributeMeasures(string name);

    /// <summary> Возвращает список атрибутов типа. </summary>
    /// <param name="name"> Название типа. </param>
    NamedEntityCollection<ILAttributeInfo> GetTypeAttrbiutes(string name);

    /// <summary> Возвращает возможные атрибуты связей для связки типов, включая служебные. </summary>
    /// <param name="parentTypeName"> Название типа родителя. </param>
    /// <param name="childTypeName"> Название типа потомка. </param>
    /// <param name="linkName"> Название типа связи. </param>
    NamedEntityCollection<ILAttributeInfo> GetLinkAttrbiutesForTypes(string parentTypeName, string childTypeName, string linkName);

    /// <summary> Очистить загруженные данные. </summary>
    void Clear();
  }
}