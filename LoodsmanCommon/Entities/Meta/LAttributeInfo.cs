using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;

namespace LoodsmanCommon
{
  public class LAttributeInfo : Entity, ILAttributeInfo
  {
    private readonly ILoodsmanMeta _meta;
    private readonly string _list;
    private IReadOnlyList<string> _listValues;
    private LAttributeMeasure[] _measures;

    internal LAttributeInfo(ILoodsmanMeta meta, DataRow dataRow) : base(dataRow.ID(), dataRow.NAME())
    {
      _meta = meta;
      Type = dataRow.ATTRTYPE();
      DefaultValue = dataRow.DEFAULT();
      _list = dataRow.LIST();
      OnlyIsItems = dataRow.ONLYLISTITEMS();
      IsSystem = dataRow.SYSTEM();
    }

    public AttributeType Type { get; }
    public string DefaultValue { get; }
    public IReadOnlyList<string> ListValues => _listValues ??= new ReadOnlyCollection<string>(_list.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
    public bool OnlyIsItems { get; }
    public bool IsSystem { get; }
    public bool IsObligatory { get; }
    public bool IsMeasured => Measures.Any();
    public IEnumerable<LAttributeMeasure> Measures => _measures ??= _meta.GetAttributeMeasures(Name).OrderBy(x => x.Name).ToArray();
  }
}