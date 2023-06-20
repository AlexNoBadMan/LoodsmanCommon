//using Ascon.Plm.Loodsman.PluginSDK;
//using System.Collections.Generic;

//namespace LoodsmanCommon
//{
//  public class LEffectivityTypeInfo : Entity
//  {
//    private readonly ILoodsmanMeta _meta;
//    private readonly INetPluginCall _pc;
//    private IEnumerable<LAttributeInfo> _attributes;

//    public LEffectivityTypeInfo(ILoodsmanMeta meta, INetPluginCall pc, int id, string name) : base(id, name)
//    {
//      _meta = meta;
//      _pc = pc;
//    }

//    /// <summary> Список возможных атрибутов типа применияемости. </summary>
//    public IEnumerable<LAttributeInfo> Attributes => _attributes ??= _pc.Native_GetEffTypeAttributes(Id).Select(x => _meta.Attributes[x.ATTR_NAME()]);
//  }
//}