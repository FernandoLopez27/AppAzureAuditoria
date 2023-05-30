using System;
using CoreAnimation;
using CoreLocation;
using MapKit;
using System.IO;
using UIKit;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CoreGraphics;
using Foundation;


namespace AppAzureAuditoria
{
	public partial class DetalleController : UIViewController
	{
		public DetalleController () : base ("DetalleController", null)
		{
		}

        public DetalleController(string auditor, string empresa, string resultado, string imagen, string imagenfondo, string latitud,string longitud) : base("DetalleController", null)
        {
            Auditor = auditor;
            Empresa = empresa;
            Resultado = resultado;
            ImagenS = imagen;
            Imagenfondo = imagenfondo;
            Latitud = double.Parse(latitud.ToString());
            Longitud = double.Parse(longitud.ToString());
        }
        public string Auditor { get; set; }
        public string Empresa { get; set; }
        public string Resultado { get; set; }
        public string ImagenS { get; set; }
        public string Imagenfondo { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }

        protected DetalleController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
            CAGradientLayer gradiente = new CAGradientLayer();
            gradiente.Frame = View.Bounds;
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
            this.View.Layer.InsertSublayer(gradiente, 0);

            var folder = System.Environment.GetFolderPath
                         (System.Environment.SpecialFolder.Personal);
            var filename = Auditor.ToString() + ".jpg";
            var filenamefondo = Auditor.ToString() + "Fondo.jpg";
            string pathios = Path.Combine(folder, filename);
            string pathiosfondo = Path.Combine(folder, filenamefondo);
            _Imagen.Image = UIImage.FromFile(pathios);
           _ImagenFondo.Image = UIImage.FromFile(pathiosfondo);
            CALayer RedondeoImagen = _Imagen.Layer;
            RedondeoImagen.CornerRadius = 40;
            RedondeoImagen.MasksToBounds = true;
            _lblAuditor.Text = Auditor.ToString();
            _lblEmpresa.Text = Empresa.ToString();
            _lblResultado.Text = Resultado.ToString();
            _Mapa.MapType = MKMapType.Hybrid;
            var Centrar = new CLLocationCoordinate2D(Latitud, Longitud);
            var Altura = new MKCoordinateSpan(.003, .003);
            var Region = new MKCoordinateRegion(Centrar, Altura);
            _Mapa.SetRegion(Region, true);

        }
	}
}


