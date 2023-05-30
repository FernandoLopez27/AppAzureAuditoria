using System;
using Foundation;
using UIKit;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CoreAnimation;
using CoreGraphics;

namespace AppAzureAuditoria
{
	public partial class CeldaController : UITableViewCell
	{
        string Imagenruta, Imagenfondo, Nombre;
        public static readonly NSString Key = new NSString ("CeldaController");
		public static readonly UINib Nib;

		static CeldaController ()
		{
			Nib = UINib.FromName ("CeldaController", NSBundle.MainBundle);
		}

		protected CeldaController (IntPtr handle) : base (handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

        public async void ActualizarCelda(Datos datos)
        {
            CAGradientLayer gradiente = new CAGradientLayer();
            gradiente.Frame = this.Bounds;
            gradiente.Colors = new CGColor[]
            {
                UIColor.Red.CGColor,
                UIColor.Blue.CGColor
            };
            gradiente.Locations = new NSNumber[]
            {
                new NSNumber(0.0),
                new NSNumber(1.0)
            };
            this.Layer.InsertSublayer(gradiente, 0);
            Nombre = datos.Auditor;
            Imagenruta = datos.Imagen;
            Imagenfondo = datos.ImagenFondo;
            var rutaArchivo = await DescargarImagen();
            var rutaArchivoFondo = await DescargarImagenFondo();
            _Imagen.Image = UIImage.FromFile(rutaArchivo);
            _ImagenFondo.Image = UIImage.FromFile(rutaArchivoFondo);
            CALayer RedondeoImagen = _Imagen.Layer;
            RedondeoImagen.CornerRadius = 40;
            RedondeoImagen.MasksToBounds = true;
            _lblAuditor.Text = datos.Auditor;
            _lblEmpresa.Text = datos.Empresa;
            this.Layer.InsertSublayer(gradiente, 0);

        }
        public async Task<string> DescargarImagen()
        {
            string pathios = null;
            try
            {
                var client = new HttpClient();
                byte[] imgurl = await client.GetByteArrayAsync
                    (Imagenruta.ToString());
                var folder = System.Environment.GetFolderPath
                    (System.Environment.SpecialFolder.Personal);
                var filename = Nombre.ToString() + ".jpg";
                pathios = Path.Combine(folder, filename);
                File.WriteAllBytes(pathios, imgurl);
                return pathios;
            }
            catch (Exception ex)
            {
                return pathios;
            }
        }
        public async Task<string> DescargarImagenFondo()
        {
            string pathios = null;
            try
            {
                var client = new HttpClient();
                byte[] imgurl = await client.GetByteArrayAsync
                    (Imagenfondo.ToString());
                var folder = System.Environment.GetFolderPath
                    (System.Environment.SpecialFolder.Personal);
                var filename = Nombre.ToString() + "Fondo.jpg";
                pathios = Path.Combine(folder, filename);
                File.WriteAllBytes(pathios, imgurl);
                return pathios;
            }
            catch (Exception ex)
            {
                return pathios;
            }
        }
    }
}
