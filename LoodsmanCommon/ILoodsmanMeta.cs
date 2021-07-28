using Ascon.Plm.Loodsman.PluginSDK;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace LoodsmanCommon
{
    public interface ILoodsmanMeta
    {
        List<LType> Types { get; }
        List<LLink> Links { get; }
        List<LState> States { get; }
        List<LAttribute> Attributes { get; }
        List<LProxyUseCase> ProxyUseCases { get; }
        LProxyUseCase GetProxyUseCase(string parentType, string childDocumentType, string extension);
        //IEnumerable<Types> GetChildTypesForType(string typeName); 
        //IEnumerable<Types> GetChildTypesForType(Types type);
        void Clear();
    }

    internal class LoodsmanMeta : ILoodsmanMeta
    {
        private List<LType> _types;
        private List<LLink> _links;
        private List<LState> _states;
        private List<LAttribute> _attributes;
        private List<LProxyUseCase> _proxyUseCases;
        private readonly INetPluginCall _iNetPC;

        public List<LType> Types
        {
            get
            {
                if (_types is null)
                {
                    _types = new List<LType>();
                    var dt = _iNetPC.GetDataTable("GetTypeListEx");
                    _types.AddRange(dt.Select().Select(x => new LType(x, Attributes, States, "_TYPENAME")));
                }
                return _types;
            }
        }
        
        public List<LLink> Links
        {
            get
            {
                if (_links is null)
                {
                    _links = new List<LLink>();
                    var dt = _iNetPC.GetDataTable("GetLinkList");
                    _links.AddRange(dt.Select().Select(x => new LLink(x)));
                }
                return _links;
            }
        }

        public List<LState> States
        {
            get
            {
                if (_states is null)
                {
                    _states = new List<LState>();
                    var dt = _iNetPC.GetDataTable("GetStateList");
                    _states.AddRange(dt.Select().Select(x => new LState(x)));
                }
                return _states;
            }
        }

        public List<LAttribute> Attributes
        {
            get
            {
                if (_attributes is null)
                {
                    _attributes = new List<LAttribute>();
                    var dt = _iNetPC.GetDataTable("GetAttributeList2", 0);
                    _attributes.AddRange(dt.Select().Select(x => new LAttribute(x)));
                }
                return _attributes;
            }
        }

        public List<LProxyUseCase> ProxyUseCases
        {
            get
            {
                if (_proxyUseCases is null)
                {
                    _proxyUseCases = new List<LProxyUseCase>();
                    var dt = _iNetPC.GetDataTable("GetProxyUseCases", 0, 0, 0);
                    _proxyUseCases.AddRange(dt.Select().Select(x => new LProxyUseCase(x)));
                }
                return _proxyUseCases;
            }
        }

        public LoodsmanMeta(INetPluginCall iNetPC)
        {
            _iNetPC = iNetPC;
        }
        //public IEnumerable<int> IdsTypesDSE { get; private set; } = GetTypesByNames(new string[] { "Деталь", "Сборочная единица", "Папка" });
        //public IEnumerable<int> IdsTypes3D { get; private set; } = GetTypesByNames(new string[] { "3D-модель детали", "3D-модель детали SW", "3D-модель сборки", "3D-модель сборки SW", "3D-сборка технологическая" });
        //public IEnumerable<int> IdsTypesTP { get; private set; } = GetTypesByNames(new string[] { "Гальваника", "Литье", "Механообработка", "Нанесение покрытия", "Плановый ТП", "Сборка", "Сварка", "Сквозной ТП", "Термообработка", "Штамповка" });
        //public IEnumerable<int> IdsTypesDocument { get; private set; } = Types.Where(type => type.IsDocument).Select(type => type.Id);

        //private static IEnumerable<int> GetTypesByNames(string[] typeNames)
        //{
        //    return Types.Where(type => typeNames.Contains(type.Name)).Select(type => type.Id);
        //}

        //public IEnumerable<Types> GetChildTypesForType(string typeName)
        //{
        //    var typesForType = GetTypes(typeName).Select(x => x["_NAME"] as string).ToList();
        //    return Types.Where(x => typesForType.Contains(x.Name));
        //}

        //public IEnumerable<Types> GetChildTypesForType(Types type)
        //{
        //    var typesForType = GetTypes(type.Name).Select(x => (int)x["_ID"]).ToList();
        //    return Types.Where(x => typesForType.Contains(x.Id));
        //}

        //private DataRow[] GetTypes(string typeName)
        //{
        //    return _iNetPC.GetDataTable("GetInfoAboutType", typeName, 8).Select();
        //}

        public LProxyUseCase GetProxyUseCase(string parentType, string childDocumentType, string extension)
        {
            return ProxyUseCases.FirstOrDefault(x => x.TypeName == parentType &&
                                                     x.DocumentType == childDocumentType &&
                                                     x.Extension.IndexOf(extension, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        public void Clear()
        {
            _attributes = null;
            _types = null;
            _states = null;
        }
    }

    public enum AttributeType : short
    {
        LString = 0,
        LInt = 1,
        LDouble = 2,
        LDateTime = 3,
        LRTFText = 5,
        LImage = 6
    }
    
    public abstract class Entity
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public Entity(DataRow dataRow, string nameField = "_NAME")
        {
            Id = (int)dataRow["_ID"];
            Name = dataRow[nameField].ToString();
        }
    }
    
    public abstract class EntityIcon : Entity
    {
        public Image Icon { get; private set; }
        public ImageSource BitmapSource { get; private set; }
        public EntityIcon(DataRow dataRow, string nameField = "_NAME") : base(dataRow, nameField)
        {
            var iconField = dataRow["_ICON"];

            if (!Convert.IsDBNull(iconField))
            {
                using (var icon = new MemoryStream((byte[])iconField))
                {
                    if (icon.Length > 0)
                    {
                        try
                        {
                            FillIcon(icon);
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }

        private void FillIcon(MemoryStream icon)
        {
            var bitmap = new Bitmap(icon);
            bitmap.MakeTransparent(bitmap.GetPixel(0, bitmap.Height - 1));
            Icon = bitmap;
            IntPtr hBitmap = bitmap.GetHbitmap();
            try
            {
                BitmapSource = Imaging.CreateBitmapSourceFromHBitmap(
                             hBitmap,
                             IntPtr.Zero,
                             Int32Rect.Empty,
                             BitmapSizeOptions.FromEmptyOptions());
            }
            finally
            {
                DeleteObject(hBitmap);
            }
        }
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);
    }

    public class LType : EntityIcon
    {
        public LAttribute KeyAttr { get; private set; }
        public bool IsDocument { get; private set; }
        public LState DefaultState { get; private set; }

        public LType(DataRow dataRow, List<LAttribute> attributes, List<LState> states, string nameField = "_NAME") : base(dataRow, nameField)
        {
            KeyAttr = attributes.FirstOrDefault(a => a.Name == dataRow["_ATTRNAME"].ToString());
            IsDocument = (int)dataRow["_DOCUMENT"] == 1;
            DefaultState = states.FirstOrDefault(a => a.Name == dataRow["_DEFAULTSTATE"].ToString());
        }
        public override string ToString()
        {
            return Name;
        }
    }

    public class LLink : EntityIcon
    {
        public string InverseName { get; }
        //public bool VerticalLink { get; }

        public LLink(DataRow dataRow, string nameField = "_NAME") : base(dataRow, nameField)
        {
            InverseName = dataRow["_INVERSENAME"].ToString();
            //VerticalLink = (System.Int16)dataRow["_TYPE"] == 0; Приведение к Int вызвало ошибку с Int16 проблем не возникло
            //Order = (int)dataRow["_ORDER"];
        }

    }
    
    public class LState : EntityIcon
    {
        public LState(DataRow dataRow, string nameField = "_NAME") : base(dataRow, nameField)
        {

        }
    }

    public class LAttribute : Entity
    {
        public AttributeType AttrType { get; private set; }
        public string Default { get; private set; }
        public List<string> ListValue { get; private set; }
        public bool OnlyIsItems { get; private set; }
        public bool System { get; set; }
        public LAttribute(DataRow dataRow, string nameField = "_NAME") : base(dataRow, nameField)
        {
            AttrType = (AttributeType)dataRow["_ATTRTYPE"];
            Default = dataRow["_DEFAULT"].ToString();
            ListValue = dataRow["_LIST"].ToString().Split(new char[2] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            //Encoding.Default.GetString(dataRow["_LIST"] as byte[])
            //.Split(Eol_sep, StringSplitOptions.RemoveEmptyEntries).ToList();
            OnlyIsItems = (int)dataRow["_ONLYLISTITEMS"] == 1;
            System = (short)dataRow["_SYSTEM"] == 1;
        }
    }

    public class LProxyUseCase : Entity
    {
        public string TypeName { get; }
        public string DocumentType { get; }
        public string Extension { get; }
        public LProxyUseCase(DataRow dataRow, string nameField = "_PROXYNAME") : base(dataRow, nameField)
        {
            TypeName = dataRow["_PARENTNAME"].ToString();
            DocumentType = dataRow["_DOCNAME"].ToString();
            Extension = $".{dataRow["_EXTENSION"]}";
        }
    }
}