using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LoodsmanCommon
{
  public abstract class EntityIcon : Entity
  {
    private readonly byte[]? _iconField;
    private Image _icon;
    private ImageSource _bitmapSource;

    public Image Icon => _icon ??= GetIcon(_iconField);
    public ImageSource BitmapSource => _bitmapSource ??= GetIconSource(Icon);
    internal EntityIcon(int id, string name, byte[]? iconField) : base(id, name)
    {
      _iconField = iconField;
    }

    internal EntityIcon(DataRow dataRow, string nameField = "_NAME") : this(dataRow.ID(), dataRow[nameField] as string, dataRow["_ICON"] as byte[]) { }


    private static Image GetIcon(byte[]? iconField)
    {
      if (iconField is null)
        return new Bitmap(16, 16);

      using var icon = new MemoryStream(iconField);
      var bitmap = new Bitmap(icon);
      bitmap.MakeTransparent(bitmap.GetPixel(0, bitmap.Height - 1));
      if (bitmap.Width > 16)
        bitmap = new Bitmap(bitmap, 16, 16);

      return bitmap;
    }

    public static BitmapImage GetIconSource(Image image)
    {
      var bitmapImage = new BitmapImage();
      using (var ms = new MemoryStream())
      {
        image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        ms.Position = 0;
        bitmapImage.BeginInit();
        bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
        bitmapImage.UriSource = null;
        bitmapImage.StreamSource = ms;
        bitmapImage.EndInit();
      }
      bitmapImage.Freeze();
      return bitmapImage;
    }
  }
}