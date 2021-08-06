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
        IEnumerable<LType> Types { get; }
        IEnumerable<LLink> Links { get; }
        IEnumerable<LLinkInfoBetweenTypes> LinksInfoBetweenTypes { get; }
        IEnumerable<LState> States { get; }
        IEnumerable<LAttribute> Attributes { get; }
        IEnumerable<LProxyUseCase> ProxyUseCases { get; }
        LProxyUseCase GetProxyUseCase(string parentType, string childDocumentType, string extension);
        void Clear();
    }

    internal class LoodsmanMeta : ILoodsmanMeta
    {
        private List<LType> _types;
        private List<LLink> _links;
        private List<LState> _states;
        private List<LAttribute> _attributes;
        private List<LProxyUseCase> _proxyUseCases;
        private List<LLinkInfoBetweenTypes> _linksInfoBetweenTypes;
        private readonly INetPluginCall _iNetPC;

        public IEnumerable<LType> Types
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

        public IEnumerable<LLink> Links
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

        public IEnumerable<LState> States
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

        public IEnumerable<LAttribute> Attributes
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

        public IEnumerable<LProxyUseCase> ProxyUseCases
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
        
        public IEnumerable<LLinkInfoBetweenTypes> LinksInfoBetweenTypes 
        { 
            get 
            {
                if (_linksInfoBetweenTypes is null)
                {
                    _linksInfoBetweenTypes = new List<LLinkInfoBetweenTypes>();
                    var dt = _iNetPC.GetDataTable("GetLinkListEx");
                    FillLinksInfoBetweenTypes(dt, _linksInfoBetweenTypes);
                }
                return _linksInfoBetweenTypes; 
            } 
        }

        public LoodsmanMeta(INetPluginCall iNetPC)
        {
            _iNetPC = iNetPC;
        }

        private static void FillLinksInfoBetweenTypes(DataTable dt, List<LLinkInfoBetweenTypes> linkInfos)
        {
            //Метод GetLinkListEx возвращает данные в которых есть дубликаты
            /*
             Например Если связь горизонтальная
             |_TYPE_ID_1|  _TYPE_NAME_1	  |_TYPE_ID_2| _TYPE_NAME_2    |_ID_LINKTYPE|   _LINKTYPE    |  _INVERSENAME  |_LINKKIND|_DIRECTION|_IS_QUANTITY|
             |    59    |Сборочная единица|    59    |Сборочная единица|	 19     |Взаимозаменяемые|Взаимозаменяемые|    1    |    1     |     0      |
             |    59    |Сборочная единица|    59    |Сборочная единица|	 19     |Взаимозаменяемые|Взаимозаменяемые|    1    |    1     |     0      |
             |    59    |Сборочная единица|    59    |Сборочная единица|	 19     |Взаимозаменяемые|Взаимозаменяемые|    1    |    -1    |     0      |
             |    59    |Сборочная единица|    59    |Сборочная единица|	 19     |Взаимозаменяемые|Взаимозаменяемые|    1    |    -1    |     0      |
             
             Если связь вертикальная и типы могут входить друг в друга
             |_TYPE_ID_1|  _TYPE_NAME_1	  |_TYPE_ID_2| _TYPE_NAME_2    |_ID_LINKTYPE|   _LINKTYPE          |  _INVERSENAME  |_LINKKIND|_DIRECTION|_IS_QUANTITY|
             |    59    |Сборочная единица|    59    |Сборочная единица|     8      |Изготавливается из ...|Для изготовления|    0    |   1      |     0      |
             |    59    |Сборочная единица|    59    |Сборочная единица|     8      |Изготавливается из ...|Для изготовления|    0    |   -1     |     0      |
             
              Для исключения дубликатов, если Id, TypeId1 и TypeId2 такие как у предыдущей добавленной строки, 
              то присваиваем пердыдущей позиции (уже добавленной) Direction = LinkDirection.ForwardAndBackward, не добавляя текущуюю
             */
            var previousLinkInfo = new LLinkInfoBetweenTypes();
            foreach (DataRow dataRow in dt.Rows)
            {
                var currentLinkInfo = new LLinkInfoBetweenTypes(dataRow);
                if (previousLinkInfo.Id == currentLinkInfo.Id && previousLinkInfo.TypeId1 == currentLinkInfo.TypeId1 && previousLinkInfo.TypeId2 == currentLinkInfo.TypeId2)
                {
                    previousLinkInfo.Direction = LinkDirection.ForwardAndBackward;
                }
                else
                {
                    previousLinkInfo = currentLinkInfo;
                    linkInfos.Add(currentLinkInfo);
                }
            }
        }

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
            _links = null;
            _proxyUseCases = null;
            _linksInfoBetweenTypes = null;
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
        public int Id { get; }
        public string Name { get; }

        public Entity(DataRow dataRow, string nameField = "_NAME")
        {
            Id = (int)dataRow["_ID"];
            Name = dataRow[nameField] as string;
        }
    }
    
    public abstract class EntityIcon : Entity
    {
        public Image Icon { get; }
        public ImageSource BitmapSource { get; }
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
                        catch
                        {
                        }
                    }
                }
            }
        }

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);
    }

    public class LType : EntityIcon
    {
        public LAttribute KeyAttr { get; }
        public bool IsDocument { get; }
        public bool Versioned { get; }
        public LState DefaultState { get; }
        public bool CanBeProject { get; }
        public bool CanCreate { get; }

        public LType(DataRow dataRow, IEnumerable<LAttribute> attributes, IEnumerable<LState> states, string nameField = "_NAME") : base(dataRow, nameField)
        {
            KeyAttr = attributes.FirstOrDefault(a => a.Name == dataRow["_ATTRNAME"] as string);
            IsDocument = (int)dataRow["_DOCUMENT"] == 1;
            Versioned = (int)dataRow["_NOVERSIONS"] == 0;
            DefaultState = states.FirstOrDefault(a => a.Name == dataRow["_DEFAULTSTATE"] as string);
            CanBeProject = (int)dataRow["_CANBEPROJECT"] == 1;
            CanCreate = (int)dataRow["_CANCREATE"] == 1;
        }
        public override string ToString()
        {
            return Name;
        }
    }

    public class LLink : EntityIcon
    {
        public string InverseName { get; }
        public bool VerticalLink { get; }

        public LLink(DataRow dataRow, string nameField = "_NAME") : base(dataRow, nameField)
        {
            InverseName = dataRow["_INVERSENAME"] as string;
            VerticalLink = (short)dataRow["_TYPE"] == 0; //Приведение к int вызвало ошибку с short проблем не возникло
            //Order = (int)dataRow["_ORDER"];
            /*
             * 1 	_TYPE_ID_1 		ftInteger
               2 	_TYPE_NAME_1 	ftString
               3 	_TYPE_ID_2 		ftInteger
               4 	_TYPE_NAME_2 	ftString
               5 	_ID_LINKTYPE 	ftInteger
               6 	_LINKTYPE 		ftString
               7 	_INVERSENAME 	ftString
               8 	_LINKKIND 		ftSmallint
               9 	_DIRECTION 		ftInteger
               10 	_IS_QUANTITY 	ftSmallint
             */
        }

    }
    public enum LinkDirection
    {
        Forward = 1,
        Backward = -1,
        ForwardAndBackward = 0
    }

    public class LLinkInfoBetweenTypes
    {
        public int Id { get; }
        public string Name { get; }
        public string InverseName { get; }
        public int TypeId1 { get; }
        public string TypeName1 { get; }
        public int TypeId2 { get; }
        public string TypeName2 { get; }
        public bool IsVertical { get; }
        public LinkDirection Direction { get; internal set; }
        public bool IsQuantity { get; }

        public LLinkInfoBetweenTypes(DataRow dataRow)
        {
            Id = (int)dataRow["_ID_LINKTYPE"];
            Name = dataRow["_LINKTYPE"] as string;
            InverseName = dataRow["_INVERSENAME"] as string;
            TypeId1 = (int)dataRow["_TYPE_ID_1"];
            TypeName1 = dataRow["_TYPE_NAME_1"] as string;
            TypeId2 = (int)dataRow["_TYPE_ID_2"];
            TypeName2 = dataRow["_TYPE_NAME_2"] as string;
            IsVertical = (short)dataRow["_LINKKIND"] == 0;
            Direction = (LinkDirection)dataRow["_DIRECTION"];
            IsQuantity = (short)dataRow["_IS_QUANTITY"] == 1;
        }
        internal LLinkInfoBetweenTypes()
        {

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
        public AttributeType AttrType { get; }
        public string Default { get; }
        public List<string> ListValue { get; }
        public bool OnlyIsItems { get; }
        public bool System { get; set; }
        public LAttribute(DataRow dataRow, string nameField = "_NAME") : base(dataRow, nameField)
        {
            AttrType = (AttributeType)dataRow["_ATTRTYPE"];
            Default = dataRow["_DEFAULT"] as string;
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
            TypeName = dataRow["_PARENTNAME"] as string;
            DocumentType = dataRow["_DOCNAME"] as string;
            Extension = $".{dataRow["_EXTENSION"]}";
        }
    }
}