using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;

namespace LoodsmanCommon
{
  public class LAttributeInfo : Entity
  {
    private readonly ILoodsmanMeta _meta;
    private IEnumerable<LAttributeMeasure> _measures;

    public AttributeType Type { get; }
    public string DefaultValue { get; }
    public IReadOnlyList<string> ListValue { get; }
    public bool OnlyIsItems { get; }
    public bool IsSystem { get; }
    public bool IsMeasured => Measures.Any();
    public IEnumerable<LAttributeMeasure> Measures => _measures ??= _meta.GetAttributeMeasures(Name);

    internal LAttributeInfo(ILoodsmanMeta meta, DataRow dataRow, string nameField = "_NAME") : base(dataRow, nameField)
    {
      _meta = meta;
      Type = (AttributeType)dataRow["_ATTRTYPE"];
      DefaultValue = dataRow["_DEFAULT"] as string;
      ListValue = new ReadOnlyCollection<string>(dataRow["_LIST"].ToString().Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
      OnlyIsItems = (int)dataRow["_ONLYLISTITEMS"] == 1;
      IsSystem = (short)dataRow["_SYSTEM"] == 1;
    }
  }
}