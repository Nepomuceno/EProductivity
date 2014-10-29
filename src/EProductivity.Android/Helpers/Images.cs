using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Graphics;
using Android.Widget;

namespace EProductivity.Droid.Helpers
{
    public interface IBitmapHolder
    {
        void SetImageBitmap(Bitmap bmp);
    }

    public static class Images
    {
        public static float ScreenWidth = 320;
        static Dictionary<string, Bitmap> bmpCache = new Dictionary<string, Bitmap>();

        public static async Task SetImageFromUrlAsync(this ImageView imageView, string url)
        {
            var bmp = FromUrl(url);
            if (bmp.IsCompleted)
                imageView.SetImageBitmap(bmp.Result);
            else
                imageView.SetImageBitmap(await bmp);
        }

        public static async Task SetImageFromUrlAsync(IBitmapHolder imageView, string url)
        {
            var bmp = FromUrl(url);
            if (bmp.IsCompleted)
                imageView.SetImageBitmap(bmp.Result);
            else
                imageView.SetImageBitmap(await bmp);
        }

        public static async Task<Bitmap> FromUrl(string url)
        {
            Bitmap bmp;
            if (bmpCache.TryGetValue(url, out bmp))
                return bmp;
            var path = await FileCache.Download(url);
            if (string.IsNullOrEmpty(path))
                return null;
            bmp = await BitmapFactory.DecodeFileAsync(path);
            bmpCache[url] = bmp;
            return bmp;
        }
    }
}