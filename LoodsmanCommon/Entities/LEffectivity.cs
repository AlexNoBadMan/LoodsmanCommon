//using System;

//namespace LoodsmanCommon
//{
//  public class LEffectivity : LAttributeOwner
//  {
//    public LEffectivity(int id, string name, LEffectivityTypeInfo effTypeInfo) : base(id, name)
//    {
//      EffTypeInfo = effTypeInfo;
//    }

//    public LEffectivityTypeInfo EffTypeInfo { get; }


//    public override void UpdateAttribute(string name, object value, LMeasureUnit unit)
//    {
//            Ascon.Plm.Loodsman.PluginSDK.INetPluginCall call = null;
//      call.Native_SetVersionEffAttrValue(Id, name, value);
//    }

//    protected override NamedEntityCollection<LAttribute> GetAttributes()
//    {
//      foreach (var item in EffTypeInfo.Attributes)
//      {

//      }
//      throw new NotImplementedException();
//    }
//  }
//}
