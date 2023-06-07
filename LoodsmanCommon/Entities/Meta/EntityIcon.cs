using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LoodsmanCommon
{
  public abstract class EntityIcon : Entity
  {
    public Image Icon { get; }
    public ImageSource BitmapSource { get; }

    internal EntityIcon(int id, string name, byte[] iconField) : base(id, name)
    {
      try
      {
        using var icon = new MemoryStream(iconField);
        var bitmap = new Bitmap(icon);
        bitmap.MakeTransparent(bitmap.GetPixel(0, bitmap.Height - 1));
        Icon = bitmap;
        IntPtr hBitmap = bitmap.GetHbitmap();
        try
        {
          BitmapSource = Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }
        finally
        {
          DeleteObject(hBitmap);
        }
      }
      catch
      { }
    }

    internal EntityIcon(DataRow dataRow, string nameField = "_NAME") : this(dataRow.ID(), dataRow[nameField] as string, dataRow["_ICON"] as byte[]) { }

    [System.Runtime.InteropServices.DllImport("gdi32.dll")]
    private static extern bool DeleteObject(IntPtr hObject);
  }
}