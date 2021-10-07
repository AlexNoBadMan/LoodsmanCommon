using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace LoodsmanCommon.Entities
{
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
                                BitmapSource = Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
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
}